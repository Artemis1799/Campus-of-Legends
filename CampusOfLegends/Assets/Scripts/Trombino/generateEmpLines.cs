using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// La classe <c>EmployeeTableManager</c> est responsable de l'affichage et de la mise à jour du tableau des employés.
/// Elle génère dynamiquement des lignes pour chaque employé et permet de rafraîchir le tableau si nécessaire.
/// </summary>
public class EmployeeTableManager : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform contentParent;

    private List<EmployeeData> employees;
    public static EmployeeManager Instance { get; private set; }


    /// <summary>
    /// Méthode appelée au démarrage du jeu. Elle récupère tous les employés et génère le tableau.
    /// </summary>
    private void Start()
    {
        employees = EmployeeManager.Instance.GetAllEmployees();

        GenerateTable();
    }

    /// <summary>
    /// Méthode pour générer le tableau des employés. Pour chaque employé, une ligne est instanciée et ajoutée à l'interface.
    /// </summary>
    private void GenerateTable()
    {
        foreach (var employee in employees)
        {
            GameObject newRow = Instantiate(rowPrefab, contentParent);
            newRow.SetActive(true);
            TextMeshProUGUI[] texts = newRow.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = employee.GetLastName();
            if (employee.GetJob() == "Entretient")
            {
                texts[1].text = "Entretien";
            }
            else { 
            texts[1].text = employee.GetJob();
            }
            texts[2].text = employee.GetFirstName();

            empLine emp = newRow.GetComponent<empLine>();
            if (emp != null)
            {
                emp.setEmpData(employee);
            }
        }
    }

    /// <summary>
    /// Méthode pour rafraîchir le tableau en supprimant les anciennes lignes et en générant un nouveau tableau.
    /// </summary>
    public void RefreshTable()
    {
        foreach(Transform child in contentParent)
        {
            Destroy(child.gameObject);
            Debug.Log("Destroying child");
        }
        employees = EmployeeManager.Instance.GetAllEmployees();

        GenerateTable();
    }

    //public void Update()
    //{
    //    if (Input.GetMouseButtonDown(0)) // 0 is for left mouse button
    //    {
    //        RefreshTable();
    //    }
    //}

}
