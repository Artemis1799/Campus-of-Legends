using System;
using UnityEngine;
/// <summary>
/// Classe sérialisable représentant les données d'un employé.
/// Contient des informations sur l'identité, le poste, les performances et l'état de l'employé.
/// </summary>
[Serializable]
public class EmployeeData
{
    public string firstName;
    public string lastName;
    public string job;
    public int salary;
    public int age;
    public float workEfficiencyGrade;
    public float workTeamGrade;
    public float fidelityGrade;
    public char sexe;
    public GameObject prefab;
    public bool onStrike;


    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="EmployeeData"/> avec toutes les propriétés.
    /// </summary>
    /// <param name="firstName">Prénom de l'employé.</param>
    /// <param name="lastName">Nom de l'employé.</param>
    /// <param name="job">Poste occupé par l'employé.</param>
    /// <param name="salary">Salaire de l'employé.</param>
    /// <param name="age">Âge de l'employé.</param>
    /// <param name="workEfficiencyGrade">Note d'efficacité au travail.</param>
    /// <param name="workTeamGrade">Note de travail en équipe.</param>
    /// <param name="sexe">Sexe de l'employé.</param>
    /// <param name="prefab">Prefab associé.</param>
    /// <param name="fidelityGrade">Note de fidélité.</param>
    /// <param name="onStrike">Statut de grève.</param>
    public EmployeeData(string firstName, string lastName, string job, int salary, int age, float workEfficiencyGrade, float workTeamGrade, char sexe, GameObject prefab, float fidelityGrade, bool onStrike)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.job = job;
        this.salary = salary;
        this.age = age;
        this.workEfficiencyGrade = workEfficiencyGrade;
        this.workTeamGrade = workTeamGrade;
        this.sexe = sexe;
        this.prefab = prefab;
        this.fidelityGrade = fidelityGrade;
        this.onStrike = onStrike;
    }

    public string GetFirstName()
    {
        return this.firstName;
    }

    public string GetLastName()
    {
        return this.lastName;
    }

    public string GetJob()
    {
        return this.job;
    }

    public int GetSalary()
    {
        return this.salary;
    }

    public float GetEfficency()
    {
        return this.workEfficiencyGrade;
    }

    public float GetFidelity()
    {
        return this.fidelityGrade;
    }

    public float GetTeam()
    {
        return this.workTeamGrade;
    }

    public GameObject GetPrefab()
    {
        return this.prefab;
    }

    public bool GetOnStrike()
    {
        return this.onStrike;
    }
}
