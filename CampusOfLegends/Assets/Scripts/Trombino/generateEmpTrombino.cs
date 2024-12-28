using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEditor.Rendering;


/// <summary>
/// La classe <c>EmployeeTrombinoManager</c> gère l'affichage des informations des employés dans un tableau interactif.
/// Elle associe chaque employé à une ligne du tableau contenant des informations détaillées telles que le nom, l'image et l'état de grève.
/// Elle permet également d'afficher des modèles 3D d'employés et d'interagir avec des boutons pour afficher des informations supplémentaires sur chaque employé.
/// </summary>
public class EmployeeTrombinoManager : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform contentParent;
    public Dynamic3DSpriteDisplay modelDisplay; // Référence au script Dynamic3DSpriteDisplay
    public Transform ViewportLul;
    public EmployeeForm employeeForm;
    public TextMeshProUGUI BuildingTitle;

    private List<EmployeeData> names;


    /// <summary>
    /// Méthode appelée au démarrage du jeu. Elle initialise le titre du bâtiment et génère le tableau des employés.
    /// </summary>
    private void Start()
    {
        // Exemple de noms fictifs
        if (TrombinoParam.caller == "Entretient")
        {
            BuildingTitle.text = "Employés du batiment Entretien";

        }
        else
        {
            BuildingTitle.text = "Employés du batiment " + TrombinoParam.caller;
        }
        names = TrombinoParam.Employees;
        GenerateTable();
    }

    /// <summary>
    /// Génère le tableau des employés en instanciant des lignes et en affichant les informations correspondantes.
    /// </summary>
    private void GenerateTable()
    {
        foreach (var name in names)
        {
            // Instancier une nouvelle ligne de tableau
            GameObject newRow = Instantiate(rowPrefab, contentParent);
            TextMeshProUGUI[] texts = newRow.GetComponentsInChildren<TextMeshProUGUI>();
            string empName = name.GetFirstName() + " "+ name.GetLastName();
            // Mettre à jour le texte avec le nom de l'employé
            if (texts.Length > 1) // Assurez-vous que le tableau de TextMeshProUGUI contient les éléments attendus
            {
                texts[1].text = empName;
            }


            // Créer une RawImage si elle n'existe pas déjà dans le prefab
            RawImage newDisplayImage = newRow.GetComponentInChildren<RawImage>();
            if (newDisplayImage == null)
            {
                newDisplayImage = new GameObject("EmployeeImage").AddComponent<RawImage>();
                newDisplayImage.transform.SetParent(newRow.transform);
                newDisplayImage.rectTransform.sizeDelta = new Vector2(256, 256); // Ajustez la taille selon vos besoins
            }

            // Définir l'état actif de strikeStatus en fonction de onStrike
            Transform strikeStatus = newRow.transform.Find("StrikeStatus");
            if (name.GetOnStrike())
            {
                strikeStatus.gameObject.SetActive(name.onStrike);
                GameObject seeMoreButton = newRow.transform.Find("SeeMoreButton").gameObject;
                seeMoreButton.GetComponent<Image>().color = new Color32(128, 128, 128, 255);
                seeMoreButton.GetComponent<Button>().interactable = false;
            }

            // Associer un modèle spécifique à cet employé et l'afficher avec le script Dynamic3DSpriteDisplay
            ShowModel(name, newDisplayImage);
            AssignButtonSlide(newRow, name);

        }
    }

    /// <summary>
    /// Associe le bouton "Voir plus" à une ligne d'employé et lui attribue les informations nécessaires pour l'affichage.
    /// </summary>
    private void AssignButtonSlide(GameObject prefabInstance, EmployeeData emp)
    {
        // Rechercher les composants nécessaires dans le prefab
        Transform defaultPicTransform = prefabInstance.transform.Find("SeeMoreButton");
        Transform seeMoreButtonTransform = prefabInstance.transform.Find("SeeMoreButton");
        Transform AnimatorContainerTransform = ViewportLul;
        if (defaultPicTransform != null && seeMoreButtonTransform != null && AnimatorContainerTransform!=null)
        {
            GameObject defaultTrombinoPic = defaultPicTransform.gameObject;
            Button seeMoreButton = seeMoreButtonTransform.GetComponent<Button>();
            Animator mainPanelAnimator = AnimatorContainerTransform.GetComponent<Animator>();

            if (seeMoreButton != null && mainPanelAnimator != null)
            {
                Debug.Log("Assigning ButtonOpenForm.");
                ButtonOpenForm buttonSlide = prefabInstance.AddComponent<ButtonOpenForm>();
                buttonSlide.AssignReferences(seeMoreButton, mainPanelAnimator);
                buttonSlide.employeeName = emp.GetFirstName() + " "+ emp.GetLastName();
                buttonSlide.efficiency = (float)System.Math.Round(emp.GetEfficency(), 1);
                buttonSlide.team = (float)System.Math.Round(emp.GetTeam(),1);
                buttonSlide.salary = emp.GetSalary();
                buttonSlide.fidelity = (float)System.Math.Round(emp.GetFidelity(), 1);
                buttonSlide.employeeForm=employeeForm;
            }
            else
            {
                Debug.LogError("Missing Button or Animator in prefab instance.");
            }        
        }
        else
        {
            Debug.LogError("One or more required components are missing in the prefab.");
        }
    }

    /// <summary>
    /// Affiche le modèle 3D de l'employé dans la RawImage en utilisant le script Dynamic3DSpriteDisplay.
    /// </summary>
    private void ShowModel(EmployeeData employeeName, RawImage displayImage)
    {
        GameObject modelPrefab = employeeName.GetPrefab();
        if (modelPrefab != null)
        {
            // Appeler le script Dynamic3DSpriteDisplay pour afficher le modèle et obtenir une RenderTexture
            RenderTexture renderTexture = modelDisplay.ShowModel(modelPrefab);

            // Assigner la RenderTexture à la RawImage
            displayImage.texture = renderTexture;
        }
    }

    /// <summary>
    /// Retourne le modèle 3D associé à un employé donné, en fonction de son nom.
    /// Cette méthode peut être étendue pour ajouter plus de modèles si nécessaire.
    /// </summary>
    private GameObject GetModelForEmployee(string employeeName)
    {
        // Exemple simple de logique pour associer un modèle à un employé
        // (À remplacer par une logique plus complexe si nécessaire)
        switch (employeeName)
        {
            case "Alice Dupont": return Resources.Load<GameObject>("Character_Female_1");
            case "Thomas Lefèvre": return Resources.Load<GameObject>("Character_Female_Nun");
            default: return Resources.Load<GameObject>("Character_Male_Soldier");
        }
    }
}
