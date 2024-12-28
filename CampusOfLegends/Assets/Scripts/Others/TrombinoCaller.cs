using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Cette classe est responsable de l'ouverture de scènes spécifiques, comme la scène du trombinoscope,
/// en préparant les données nécessaires avant de charger la scène.
/// Elle interagit avec le gestionnaire d'employés pour récupérer une liste d'employés pour la scène.
/// </summary>
public class SceneCaller : MonoBehaviour
{
    public string sceneName;
    public static EmployeeManager Instance { get; private set; }



    /// <summary>
    /// Ouvre la scène du trombinoscope et charge les données nécessaires.
    /// Cette méthode récupère la liste des employés en fonction de la scène et prépare les paramètres pour le trombinoscope.
    /// Ensuite, elle charge la scène du trombinoscope.
    /// </summary>
    public void OpenTrombinoscopeScene()
    {
        // Préparer la liste des employés pour le trombinoscope
        TrombinoParam.Employees = EmployeeManager.Instance.GetEmployeeList(sceneName);
        TrombinoParam.caller = sceneName;
        // Charger la scène du trombinoscope
        SceneManager.LoadScene("Bat_emp_part2");
    }
}
