using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Cette classe gère la génération et l'affichage des alertes pour différentes crises dans l'université,
/// telles que les bâtiments en feu, les bâtiments hackés, les grèves et les démissions d'employés.
/// </summary>
public class GenerateEventCrisis : MonoBehaviour
{
    // Instance statique de la classe pour garantir l'accès unique
    public static GenerateEventCrisis Instance { get; private set; }

    // Prefab pour générer des lignes d'alertes dans le tableau
    public GameObject rowPrefab;

    // Parent du contenu dans lequel les alertes seront générées
    public Transform contentParent;

    // Listes pour stocker les informations des bâtiments en feu, hackés et en grève
    List<int> BuildingOnFire;
    List<int> BuildingHacked;
    Dictionary<int, int> BuildingOnStrike;

    // Listes pour stocker les noms de bâtiments en feu et hackés (en format String)
    List<string> BuildingOnFireStr = new List<string>();
    List<string> BuildingHackedStr = new List<string>();

    // Dictionnaire des employés démissionnaires par bâtiment
    Dictionary<string, List<EmployeeData>> resignedEmployees;

    // Formulaire d'alerte à ouvrir en cas de crise
    public CrisisForm formToOpen;

    // Références aux instances des managers nécessaires pour récupérer les données
    public static CrisisManager Instance1 { get; private set; }
    public static EmployeeManager Instance2 { get; private set; }

    
    void Start()
    {
        LoadData();         // Charge les données des crises depuis les managers
        GenerateTable();    // Génére le tableau d'alertes
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("GenerateEventCrisis instance already exists. Destroying duplicate.");
            Destroy(this);
        }
    }

    /// <summary>
    /// Charge les données concernant les crises depuis les managers (incendies, hacks, grèves, démissions).
    /// </summary>
    private void LoadData()
    {
        // Récupère les données concernant les bâtiments en feu, hackés et en grève
        BuildingOnFire = CrisisManager.Instance.GetEffectiveBuildingsOnFire();
        BuildingHacked = CrisisManager.Instance.GetEffectiveBuildingsHacked();
        BuildingOnStrike = CrisisManager.Instance.GetBuildingsOnStrike();
        resignedEmployees = EmployeeManager.Instance.GetResignedEmployees();

        // Convertit les IDs des bâtiments en noms de bâtiments pour les alertes
        BuildingOnFireStr.Clear();
        foreach (var buildingsint in BuildingOnFire)
        {
            BuildingOnFireStr.Add(BuildingName(buildingsint));
        }

        BuildingHackedStr.Clear();
        foreach (var buildingsint in BuildingHacked)
        {
            BuildingHackedStr.Add(BuildingName(buildingsint));
        }
    }

    /// <summary>
    /// Génère le tableau d'alertes en fonction des crises existantes (incendies, hacks, grèves, démissions).
    /// </summary>
    private void GenerateTable()
    {
        // Supprime toutes les anciennes lignes du tableau
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject); // Supprime toutes les anciennes lignes
        }

        int index = 0;
        formToOpen.gameObject.SetActive(false);

        // Génère les alertes pour les bâtiments en feu
        foreach (var buildings in BuildingOnFireStr)
        {
            // Crée une nouvelle ligne pour chaque alerte
            GameObject newRow = Instantiate(rowPrefab, contentParent);
            newRow.SetActive(true);
            TextMeshProUGUI[] texts = newRow.GetComponentsInChildren<TextMeshProUGUI>();
            texts[1].text = "Alerte, bâtiment " + buildings + " en feu !!!";
            string alerteMsg = "ALERTE GÉNÉRALE !!!\nLe bâtiment " + buildings + " est en feu ! Réparez-le au plus vite, sinon il restera inaccessible pour vous et vos employés.\nLes réparations vous coûteront 15000 € et ne commenceront qu’après paiement.\nUne fois les travaux lancés, le chantier sera terminé d’ici la fin du semestre.\n";
            OpenFormCrises crisLin = newRow.GetComponent<OpenFormCrises>();
            if (crisLin != null)
            {
                crisLin.setData(texts[1].text, alerteMsg, true, formToOpen, BuildingOnFire[index], true);
            }
            index++;
        }

        index = 0;

        // Génère les alertes pour les bâtiments hackés
        foreach (var buildings in BuildingHackedStr)
        {
            GameObject newRow = Instantiate(rowPrefab, contentParent);
            newRow.SetActive(true);
            TextMeshProUGUI[] texts = newRow.GetComponentsInChildren<TextMeshProUGUI>();
            texts[1].text = "Alerte, bâtiment " + buildings + " se fait hacker !!!";
            string alerteMsg = "ALERTE GÉNÉRALE !!!\nLe bâtiment " + buildings + " est en train de se faire hacker ! Protégez-le au plus vite, sinon il restera accessible, mais inutile pour vous et vos employés.\nLa mise en place de la protection vous coûtera 10000 € et ne commencera qu’après paiement.\nUne fois les travaux lancés, la protection sera opérationnelle d’ici la fin du semestre.\n";
            OpenFormCrises crisLin = newRow.GetComponent<OpenFormCrises>();
            if (crisLin != null)
            {
                crisLin.setData(texts[1].text, alerteMsg, true, formToOpen, BuildingHacked[index], false);
            }
            index++;
        }

        // Génère les alertes pour les bâtiments en grève
        foreach (KeyValuePair<int, int> buildings in BuildingOnStrike)
        {
            string BatName = BuildingName(buildings.Key);
            GameObject newRow = Instantiate(rowPrefab, contentParent);
            newRow.SetActive(true);
            TextMeshProUGUI[] texts = newRow.GetComponentsInChildren<TextMeshProUGUI>();
            texts[1].text = "Une nouvelle grève au bâtiment " + BatName + ".";
            string alerteMsg = "Attention !\nUne grève a été lancée ! " + buildings.Value + " employés refusent de travailler dans le bâtiment " + BatName + ". Ils ne reprendront leurs activités qu’au semestre prochain.\nCependant, le campus continue de tourner et les autres opérations se poursuivent normalement.\nBonne chance pour gérer cette situation.";
            OpenFormCrises crisLin = newRow.GetComponent<OpenFormCrises>();
            if (crisLin != null)
            {
                crisLin.setData(texts[1].text, alerteMsg, false, formToOpen, 5, false);
            }
        }

        // Génère les alertes pour les employés démissionnaires
        foreach (KeyValuePair<string, List<EmployeeData>> emps in resignedEmployees)
        {
            foreach (var emp in emps.Value)
            {
                GameObject newRow = Instantiate(rowPrefab, contentParent);
                newRow.SetActive(true);
                TextMeshProUGUI[] texts = newRow.GetComponentsInChildren<TextMeshProUGUI>();
                texts[1].text = "Un employé de l'" + emps.Key + " nous a quitté.";
                string alerteMsg = "Malheureusement, un employé nous a quitté.\n" + emp.GetFirstName() + " " + emp.GetLastName() + " du bâtiment " + emps.Key + " a choisi de s'en aller vers d'autres horizons.\n Il semblerait que " + emp.GetFirstName() + " " + emp.GetLastName() + " ne se plaisait plus au sein de votre université.\n Il faudrait peut être penser à mettre quelqu'un à son poste.";
                OpenFormCrises crisLin = newRow.GetComponent<OpenFormCrises>();
                if (crisLin != null)
                {
                    crisLin.setData(texts[1].text, alerteMsg, false, formToOpen, 5, false);
                }
            }
        }
    }

    /// <summary>
    /// Rafraîchit les alertes en rechargeant les données et en générant à nouveau le tableau.
    /// </summary>
    public void RefreshCanvas()
    {
        LoadData();  // Recharger les données depuis les managers
        GenerateTable();  // Regénérer le tableau
    }

    /// <summary>
    /// Retourne le nom du bâtiment en fonction de son ID.
    /// </summary>
    /// <param name="buildingID">L'ID du bâtiment.</param>
    /// <returns>Le nom du bâtiment correspondant à l'ID.</returns>
    private string BuildingName(int buildingID)
    {
        switch (buildingID)
        {
            case 1: return "informatique";
            case 2: return "enseignant";
            case 4: return "administration";
            case 3: return "entretien";
            default: return "inconnu";
        }
    }
}
