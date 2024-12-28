using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Classe représentant l'efficacité d'un bâtiment dans le jeu.
/// Elle contient des informations sur l'efficacité au travail, l'efficacité de l'équipe et le nombre d'employés dans un bâtiment donné.
/// </summary>
[System.Serializable]
public class BuildingEfficiency
{
    public string buildingName;
    public float workEfficiencyCoefficient;
    public float teamEfficiencyCoefficient;
    public int nbEmployeeMeta;
}