using System;
using UnityEngine;

/// <summary>
/// Classe représentant un employé dans le jeu, avec ses caractéristiques personnelles et professionnelles.
/// </summary>
[Serializable]
public class Employee : MonoBehaviour
{
    private string firstName;
    private string lastName;
    private string job;
    private int salary;
    private int age;
    private float workEfficiencyGrade;
    private float workTeamGrade;
    private float fidelityGrade;
    private char sexe;
    private GameObject prefab;
    private bool onStrike;

    /// <summary>
    /// Définit les informations de l'employé.
    /// </summary>
    /// <param name="firstName">Prénom de l'employé.</param>
    /// <param name="lastName">Nom de famille de l'employé.</param>
    /// <param name="job">Poste occupé par l'employé.</param>
    /// <param name="salary">Salaire mensuel de l'employé.</param>
    /// <param name="age">Âge de l'employé.</param>
    /// <param name="workEfficiencyGrade">Note d'efficacité au travail (de 0 à 10).</param>
    /// <param name="workTeamGrade">Note de travail en équipe (de 0 à 10).</param>
    /// <param name="sexe">Sexe de l'employé ('M', 'F', etc.).</param>
    /// <param name="fidelityGrade">Note de fidélité (de 0 à 10).</param>
    /// <param name="onStrike">Indique si l'employé est en grève.</param>
    public void SetEmployee(string firstName, string lastName, string job, int salary, int age, float workEfficiencyGrade, float workTeamGrade, char sexe, float fidelityGrade, bool onStrike)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.job = job;
        this.salary = salary;
        this.age = age;
        this.workEfficiencyGrade = workEfficiencyGrade;
        this.workTeamGrade = workTeamGrade;
        this.sexe = sexe;
        this.fidelityGrade = fidelityGrade;
        this.onStrike = onStrike;
    }
    /// <summary>
    /// Définit les informations de l'employé avec un prefab associé.
    /// </summary>
    /// <param name="firstName">Prénom de l'employé.</param>
    /// <param name="lastName">Nom de famille de l'employé.</param>
    /// <param name="job">Poste occupé par l'employé.</param>
    /// <param name="salary">Salaire mensuel de l'employé.</param>
    /// <param name="age">Âge de l'employé.</param>
    /// <param name="workEfficiencyGrade">Note d'efficacité au travail (de 0 à 10).</param>
    /// <param name="workTeamGrade">Note de travail en équipe (de 0 à 10).</param>
    /// <param name="sexe">Sexe de l'employé ('M', 'F', etc.).</param>
    /// <param name="prefabAj">Prefab associé à l'employé.</param>
    /// <param name="fidelityGrade">Note de fidélité (de 0 à 10).</param>
    /// <param name="onStrike">Indique si l'employé est en grève.</param>
    public void SetEmployee(string firstName, string lastName, string job, int salary, int age, float workEfficiencyGrade, float workTeamGrade, char sexe, GameObject prefabAj, float fidelityGrade, bool onStrike)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.job = job;
        this.salary = salary;
        this.age = age;
        this.workEfficiencyGrade = workEfficiencyGrade;
        this.workTeamGrade = workTeamGrade;
        this.sexe = sexe;
        this.prefab = prefabAj;
        this.fidelityGrade = fidelityGrade;
        this.onStrike = onStrike;
    }

    public string GetFirstname()
    {
        return firstName;
    }

    public string GetLastname()
    {
        return lastName;
    }

    public int GetSalary()
    {
        return salary;
    }

    public int GetAge()
    {
        return age;
    }

    public float GetWorkEfficiencyGrade()
    {
        return workEfficiencyGrade;
    }

    public float GetWorkTeamGrade()
    {
        return workTeamGrade;
    }

    public float GetFidelityGrade()
    {
        return fidelityGrade;
    }

    public bool GetOnStrike()
    {
        return onStrike;
    }
    /// <summary>
    /// Convertit les informations de l'employé en un objet `EmployeeData` sérialisable.
    /// </summary>
    /// <returns>Un objet <see cref="EmployeeData"/> contenant les données de l'employé.</returns>
    public EmployeeData ToEmployeeData()
    {
        return new EmployeeData(firstName, lastName, job, salary, age, workEfficiencyGrade, workTeamGrade, sexe, prefab, fidelityGrade, onStrike);
    }
    /// <summary>
    /// Remplit les informations de l'employé à partir d'un objet `EmployeeData`.
    /// </summary>
    /// <param name="data">Les données de l'employé.</param>
    public void FromEmployeeData(EmployeeData data)
    {
        SetEmployee(data.firstName, data.lastName, data.job, data.salary, data.age, data.workEfficiencyGrade, data.workTeamGrade, data.sexe, data.prefab, data.fidelityGrade, data.onStrike);
    }
}
