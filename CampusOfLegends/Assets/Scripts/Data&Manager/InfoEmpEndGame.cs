using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Cette classe est responsable de l'affichage des informations de fin de jeu concernant les ressources du joueur, 
/// y compris l'argent, l'attractivité, le nombre d'étudiants et le nombre d'employés.
/// </summary>
public class InfoEmpEndGame : MonoBehaviour
{
    private float infoMoney;
    private float infoAttractiveness;
    private float infoNBEtu;
    private float infoNBEmp;

    public TextMeshProUGUI textMoney;
    public TextMeshProUGUI textAttractiveness;
    public TextMeshProUGUI textEfficacity;
    public TextMeshProUGUI textNBEmp;

    void Start()
    {
        infoMoney = ResourceManager.Instance.Money; 
        infoAttractiveness = ResourceManager.Instance.Attractiveness;
        infoNBEtu = ResourceManager.Instance.getNumberOfStudents();
        NbEmp();
        DisplayInfo();
    }

    /// <summary>
    /// Calcule le nombre total d'employés en récupérant la liste des employés depuis EmployeeManager.
    /// </summary>
    private void NbEmp()
    {
        List<EmployeeData> employees = new List<EmployeeData>();
        employees = EmployeeManager.Instance.GetAllEmployees();
        infoNBEmp = employees.Count;
    }

    /// <summary>
    /// Met à jour les éléments TextMeshPro avec les informations actuelles du jeu.
    /// </summary>
    private void DisplayInfo()
    {
        textMoney.text = "Argent: " + infoMoney.ToString("F0") + "€";
        textAttractiveness.text = "Attractivité: " + infoAttractiveness.ToString("F0") + "/100";
        textEfficacity.text = "Nombre d'étudiants: " + infoNBEtu;
        textNBEmp.text = "Nombre d'employés: " + infoNBEmp;
    }
}