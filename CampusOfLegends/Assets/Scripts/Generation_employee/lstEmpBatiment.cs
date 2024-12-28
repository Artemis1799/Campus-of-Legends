using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Polybrush;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe responsable de la gestion du recrutement d'employés dans les bâtiments spécifiques, 
/// y compris l'affichage des informations des employés, l'instanciation de préfabriqués, 
/// et l'interaction avec les boutons de recrutement dans l'interface utilisateur.
/// </summary>
public class lstEmpBatiment : MonoBehaviour
{
    private List<GameObject> recruitmentButtons = new List<GameObject>();
    public GenerationEmployee generationEmployee;
    private string job;
    private List<GameObject> instantiatedPrefabs = new List<GameObject>();
    public GameObject ConfirmationMenu;
    public TextMeshProUGUI numberOfEmp;
    
    private void defineJob()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        string sceneName = activeScene.name;

        if (sceneName == "Bat_Administration")
        {
            job = "Administratif";
        }
        else if (sceneName == "Bat_Enseignants")
        {
            job = "Enseignant";
        }
        else if (sceneName == "Bat_Entretien")
        {
            job = "Entretient";
        }
        else if (sceneName == "Bat_Informatique")
        {
            job = "Informatique";
        }
        else
        {
            Debug.LogError("Le nom de la sc�ne n'est pas reconnu: " + sceneName);
        }
    }

    private void FindRecruitmentButtons()
    {
        GameObject boutonRecrutement = GameObject.Find("BoutonRecrutement");
        if (boutonRecrutement != null)
        {
            for (int i = 1; i <= 3; i++)
            {
                GameObject buttonObj = boutonRecrutement.transform.Find("buttonRecruter" + i)?.gameObject;
                if (buttonObj != null)
                {
                    recruitmentButtons.Add(buttonObj);
                }
            }
        }
    }

    public void definitionDesText()
    {
        List<EmployeeData> recruitmentList = EmployeeManager.Instance.GetRecruitmentList(job);
        for (int i = 0; i < recruitmentList.Count; i++)
        {
            EmployeeData empData = recruitmentList[i];
            GameObject nameObj = GameObject.Find("name" + (i + 1));
            GameObject statsObj = GameObject.Find("textStatEmp" + (i + 1));

            if (nameObj == null)
            {
                continue;
            }

            if (statsObj == null)
            {
                continue;
            }

            TextMeshProUGUI nameText = nameObj.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI statsText = statsObj.GetComponent<TextMeshProUGUI>();

            if (nameText == null)
            {
                continue;
            }

            if (statsText == null)
            {
                continue;
            }

            nameText.text = empData.firstName + " " + empData.lastName;
            statsText.fontStyle = FontStyles.Bold;

            float efficiencyGrade = empData.workEfficiencyGrade;
            float teamGrade = empData.workTeamGrade;

            // Changer la couleur du texte en fonction de la note d'efficacit�
            Color efficiencyColor = GetColorForGrade(efficiencyGrade);
            Color teamColor = GetColorForGrade(teamGrade);

            // Construire le texte avec les couleurs
            statsText.color = Color.black;
            statsText.enableAutoSizing = false;
            statsText.fontSize = 25;
            statsText.autoSizeTextContainer = false;
            statsText.lineSpacing = -10;
            string efficiencyText = $"<color=#{ColorUtility.ToHtmlStringRGBA(efficiencyColor)}>{efficiencyGrade}</color>";
            string teamText = $"<color=#{ColorUtility.ToHtmlStringRGBA(teamColor)}>{teamGrade}</color>";

            statsText.text = $"Age: {empData.age}\nSalaire: {empData.salary}\nEfficacité: {efficiencyText}\nCohésion: {teamText}";

            GameObject spriteObj = GameObject.Find("Cube" + (i + 1));
            if (spriteObj == null)
            {
                continue;
            }

            Vector3 spritePosition = spriteObj.transform.position;
            Vector3 prefabPosition = new Vector3(spritePosition.x, 390, spritePosition.z);
            if (empData.prefab != null)
            {
                GameObject newInstance = Instantiate(empData.prefab, prefabPosition, Quaternion.identity);
                newInstance.name = "Cube" + (i + 1);
                newInstance.transform.localScale = new Vector3(200, 200, 200);
                newInstance.transform.rotation = Quaternion.Euler(0, 180, 0);
                rotateSprite rotateComponent = newInstance.AddComponent<rotateSprite>();
                rotateComponent.rotationSpeed = 50f;
                rotateComponent.rotationAxis = new Vector3(0, 1, 0);
                instantiatedPrefabs.Add(newInstance);
            }

            Destroy(spriteObj);
        }
    }

    /// <summary>
    /// Retourne la couleur associée à un grade donné.
    /// </summary>
    /// <param name="grade">La note de l'employé.</param>
    /// <returns>La couleur correspondante au grade.</returns>
    Color GetColorForGrade(float grade)
    {
        if (grade <= 1.5f)
        {
            return new Color(0.8f, 0, 0); // Rouge fonc�
        }
        else if (grade <= 2.5f)
        {
            return Color.Lerp(new Color(0.8f, 0, 0), new Color(0.8f, 0.8f, 0), (grade - 1.5f) / 1.0f); // D�grad� de rouge fonc� � jaune fonc�
        }
        else if (grade <= 4.0f)
        {
            return Color.Lerp(new Color(0.8f, 0.8f, 0), new Color(0, 0.8f, 0), (grade - 2.5f) / 1.5f); // D�grad� de jaune fonc� � vert fonc�
        }
        else
        {
            return new Color(0, 0.8f, 0); // Vert fonc�
        }
    }

    /// <summary>
    /// Initialisation du processus de recrutement en fonction de la scene.
    /// </summary>
    void Start()
    {
        defineJob();
        List<EmployeeData> recruitmentList = EmployeeManager.Instance.GetRecruitmentList(job);

        // V�rifier si la liste des employ�s pouvant �tre recrut�s est vide
        if (recruitmentList.Count == 0)
        {
            List<EmployeeData> newEmployees = generationEmployee.GenerateThreeEmployees(job).ConvertAll(emp => emp.ToEmployeeData());
            recruitmentList.AddRange(newEmployees);
        }

        FindRecruitmentButtons();
        definitionDesText();
        numberOfEmp.text = EmployeeManager.Instance.GetEmployeeList(job).Count.ToString();
    }

    /// <summary>
    /// Gestion du clic sur un bouton de recrutement pour un employé spécifique.
    /// </summary>
    /// <param name="indice">L'indice du bouton et de l'employé dans la liste de recrutement.</param>
    public void ClickResult(int indice)
    {
        List<EmployeeData> recruitmentList = EmployeeManager.Instance.GetRecruitmentList(job);
        if (indice >= 0 && indice < recruitmentButtons.Count && indice < recruitmentList.Count)
        {
            EmployeeData employeeData = recruitmentList[indice];
            EmployeeManager.Instance.HireEmployee(job, employeeData);

            if (indice < instantiatedPrefabs.Count)
            {
                GameObject oldPrefab = instantiatedPrefabs[indice];
                if (oldPrefab != null)
                {
                    Destroy(oldPrefab);
                }
            }

            ConfirmationMenu.SetActive(true);

            // Reg�n�rer compl�tement la liste des employ�s pouvant �tre recrut�s
            recruitmentList.Clear();
            List<EmployeeData> newEmployees = generationEmployee.GenerateThreeEmployees(job).ConvertAll(emp => emp.ToEmployeeData());
            recruitmentList.AddRange(newEmployees);

            // Mettre � jour le texte des boutons
            definitionDesText();
        }
        numberOfEmp.text = EmployeeManager.Instance.GetEmployeeList(job).Count.ToString();
    }
}
