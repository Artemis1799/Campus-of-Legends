using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

/// <summary>
/// <c>EmployeeManager</c> est responsable de la gestion de toutes les données des employés,
/// de l'embauche, du licenciement et du calcul des efficacités. Il garde une trace des rôles des
/// employés, des listes de recrutement et gère la performance des employés, y compris l'efficacité
/// au travail et l'efficacité de l'équipe. Il interagit également avec d'autres systèmes comme le
/// <c>CrisisManager</c> pour la gestion des grèves et le suivi des démissions des employés.
/// </summary>
public class EmployeeManager : MonoBehaviour
{
    public static EmployeeManager Instance { get; private set; }

    // listes des employés qu'on a recrutés
    public List<EmployeeData> adminEmployees = new List<EmployeeData>();
    public List<EmployeeData> teacherEmployees = new List<EmployeeData>();
    public List<EmployeeData> maintenanceEmployees = new List<EmployeeData>();
    public List<EmployeeData> itEmployees = new List<EmployeeData>();

    // listes des employés proposés dans les batiments
    public List<EmployeeData> adminRecruitmentList = new List<EmployeeData>();
    public List<EmployeeData> teacherRecruitmentList = new List<EmployeeData>();
    public List<EmployeeData> maintenanceRecruitmentList = new List<EmployeeData>();
    public List<EmployeeData> itRecruitmentList = new List<EmployeeData>();

    private static float maxGrade = 5;

    // <summary>
    // Coefficients d'efficacité pour chaque bâtiment
    // </summary>
    [SerializeField]
    private List<BuildingEfficiency> buildingEfficiencies = new List<BuildingEfficiency>
    {
        new BuildingEfficiency { buildingName = "Administratif", workEfficiencyCoefficient = 0.7f, teamEfficiencyCoefficient = 0.8f, nbEmployeeMeta = 4 },
        new BuildingEfficiency { buildingName = "Enseignant", workEfficiencyCoefficient = 0.7f, teamEfficiencyCoefficient = 0.7f, nbEmployeeMeta = 6 },
        new BuildingEfficiency { buildingName = "Entretient", workEfficiencyCoefficient = 0.4f, teamEfficiencyCoefficient = 0.6f, nbEmployeeMeta = 3},
        new BuildingEfficiency { buildingName = "Informatique", workEfficiencyCoefficient = 0.7f, teamEfficiencyCoefficient = 0.4f, nbEmployeeMeta = 4 }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Retourne la liste des employés proposés pour recrutement en fonction du poste demandé.
    /// </summary>
    /// <param name="job">Le rôle pour lequel obtenir la liste de recrutement (par exemple, "Administratif", "Enseignant").</param>
    /// <returns>Une liste d'objets <c>EmployeeData</c> correspondant aux employés proposés pour recrutement pour ce rôle.</returns>
    public List<EmployeeData> GetRecruitmentList(string job)
    {
        switch (job)
        {
            case "Administratif":
                return adminRecruitmentList;
            case "Enseignant":
                return teacherRecruitmentList;
            case "Entretient":
                return maintenanceRecruitmentList;
            case "Informatique":
                return itRecruitmentList;
            default:
                return new List<EmployeeData>();
        }
    }

    /// <summary>
    /// Retourne la liste des employés actuellement employés pour un poste spécifique.
    /// </summary>
    /// <param name="job">Le rôle pour lequel obtenir la liste des employés actuellement en poste (par exemple, "Administratif", "Enseignant").</param>
    /// <returns>Une liste d'objets <c>EmployeeData</c> correspondant aux employés actuellement en poste pour ce rôle.</returns>
    public List<EmployeeData> GetEmployeeList(string job)
    {
        switch (job)
        {
            case "Administratif":
                return adminEmployees;
            case "Enseignant":
                return teacherEmployees;
            case "Entretient":
                return maintenanceEmployees;
            case "Informatique":
                return itEmployees;
            default:
                return new List<EmployeeData>();
        }
    }

    /// <summary>
    /// Embauche un employé pour un poste spécifique.
    /// </summary>
    /// <param name="job">Le rôle pour lequel embaucher l'employé (par exemple, "Administratif", "Enseignant").</param>
    /// <param name="employeeData">Les données de l'employé à embaucher.</param>
    public void HireEmployee(string job, EmployeeData employeeData)
    {
        List<EmployeeData> recruitmentList = GetRecruitmentList(job);
        List<EmployeeData> employeeList = GetEmployeeList(job);

        if (recruitmentList.Contains(employeeData))
        {
            recruitmentList.Remove(employeeData);
            employeeList.Add(employeeData);
        }
    }

    /// <summary>
    /// Retourne la liste de tous les employés, peu importe leur poste.
    /// </summary>
    /// <returns>Une liste de tous les employés enregistrés.</returns>
    public List<EmployeeData> GetAllEmployees()
    {
        return adminEmployees
            .Concat(teacherEmployees)
            .Concat(maintenanceEmployees)
            .Concat(itEmployees)
            .ToList();
    }

    /// <summary>
    /// Licencie un employé d'un poste spécifique.
    /// </summary>
    /// <param name="job">Le rôle de l'employé à licencier (par exemple, "Administratif", "Enseignant").</param>
    /// <param name="employeeData">Les données de l'employé à licencier.</param>
    public void FireEmployee(string job, EmployeeData employeeData)
    {
        if (job == "Administratif")
        {
            adminEmployees.Remove(employeeData);
        }
        else if (job == "Enseignant")
        {
            teacherEmployees.Remove(employeeData);
        }
        else if (job == "Entretient")
        {
            maintenanceEmployees.Remove(employeeData);
        }
        else if (job == "Informatique")
        {
            itEmployees.Remove(employeeData);
        }
        if (CrisisManager.Instance != null)
        {
            CrisisManager.Instance.RemoveFromConflicts(employeeData);
        }
    }

    /// <summary>
    /// Augmente l'efficacité au travail d'un employé spécifique pour son rôle.
    /// </summary>
    /// <param name="augm">L'augmentation de l'efficacité au travail de l'employé (peut être négatif ou positif).</param>
    /// <param name="empModif">Les données de l'employé à modifier (l'employé dont l'efficacité au travail est augmentée).</param>
    public void moreWorkEfficiency(float augm, EmployeeData empModif)
    {
        switch (empModif.job)
        {
            case "Administratif":
                foreach (EmployeeData emp in adminEmployees)
                {
                    if (emp == empModif)
                    {
                        emp.workEfficiencyGrade = empModif.workEfficiencyGrade + augm > maxGrade ? maxGrade : empModif.workEfficiencyGrade + augm;
                        break;
                    }
                }
                break;
            case "Enseignant":
                foreach (EmployeeData emp in teacherEmployees)
                {
                    if (emp == empModif)
                    {
                        emp.workEfficiencyGrade = empModif.workEfficiencyGrade + augm > maxGrade ? maxGrade : empModif.workEfficiencyGrade + augm;
                        break;
                    }
                }
                break;
            case "Entretient":
                foreach (EmployeeData emp in maintenanceEmployees)
                {
                    if (emp == empModif)
                    {
                        emp.workEfficiencyGrade = empModif.workEfficiencyGrade + augm > maxGrade ? maxGrade : empModif.workEfficiencyGrade + augm;
                        break;
                    }
                }
                break;
            case "Informatique":
                foreach (EmployeeData emp in itEmployees)
                {
                    if (emp == empModif)
                    {
                        emp.workEfficiencyGrade = empModif.workEfficiencyGrade + augm > maxGrade ? maxGrade : empModif.workEfficiencyGrade + augm;
                        break;
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Augmente la cohésion au travail d'un employé spécifique pour son rôle.
    /// </summary>
    /// <param name="augm">L'augmentation de l'efficacité au travail de l'employé (peut être négatif ou positif).</param>
    /// <param name="empModif">Les données de l'employé à modifier.</param>
    public void moreTeamEfficiency(float augm, EmployeeData empModif)
    {
        switch (empModif.job)
        {
            case "Administratif":
                foreach (EmployeeData emp in adminEmployees)
                {
                    if (emp == empModif)
                    {
                        emp.workTeamGrade = empModif.workTeamGrade + augm > maxGrade ? maxGrade : empModif.workTeamGrade + augm;
                        break;
                    }
                }
                break;
            case "Enseignant":
                foreach (EmployeeData emp in teacherEmployees)
                {
                    if (emp == empModif)
                    {
                        emp.workTeamGrade = empModif.workTeamGrade + augm > maxGrade ? maxGrade : empModif.workTeamGrade + augm;
                        break;
                    }
                }
                break;
            case "Entretient":
                foreach (EmployeeData emp in maintenanceEmployees)
                {
                    if (emp == empModif)
                    {
                        emp.workTeamGrade = empModif.workTeamGrade + augm > maxGrade ? maxGrade : empModif.workTeamGrade + augm;
                        break;
                    }
                }
                break;
            case "Informatique":
                foreach (EmployeeData emp in itEmployees)
                {
                    if (emp == empModif)
                    {
                        emp.workTeamGrade = empModif.workTeamGrade + augm > maxGrade ? maxGrade : empModif.workTeamGrade + augm;
                        break;
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Augmente la fidélité d'un employé spécifique.
    /// </summary>
    /// <param name="prime">L'augmentation d'argent que l'employé va recevoir).</param>
    /// <param name="empModif">Les données de l'employé à modifier.</param>
    public void addPrime(float prime, EmployeeData empModif)
    {
        switch (empModif.job)
        {
            case "Administratif":
                foreach (EmployeeData emp in adminEmployees)
                {
                    if (emp == empModif)
                    {
                        emp.fidelityGrade = empModif.fidelityGrade + prime > maxGrade ? maxGrade : empModif.fidelityGrade + prime;
                        break;
                    }
                }
                break;
            case "Enseignant":
                foreach (EmployeeData emp in teacherEmployees)
                {
                    if (emp == empModif)
                    {
                        emp.fidelityGrade = empModif.fidelityGrade + prime > maxGrade ? maxGrade : empModif.fidelityGrade + prime;
                        break;
                    }
                }
                break;
            case "Entretient":
                foreach (EmployeeData emp in maintenanceEmployees)
                {
                    if (emp == empModif)
                    {
                        emp.fidelityGrade = empModif.fidelityGrade + prime > maxGrade ? maxGrade : empModif.fidelityGrade + prime;
                        break;
                    }
                }
                break;
            case "Informatique":
                foreach (EmployeeData emp in itEmployees)
                {
                    if (emp == empModif)
                    {
                        emp.fidelityGrade = empModif.fidelityGrade + prime > maxGrade ? maxGrade : empModif.fidelityGrade + prime;
                        break;
                    }
                }
                break;
        }
        if (CrisisManager.Instance != null)
        {
            CrisisManager.Instance.RemoveFromConflicts(empModif);
        }
    }

    /// <summary>
    /// Calcule l'efficacité d'un bâtiment spécifique en fonction des performances des employés et des facteurs externes.
    /// </summary>
    /// <param name="buildingName">Le nom du bâtiment pour lequel calculer l'efficacité.</param>
    /// <returns>L'efficacité calculée du bâtiment spécifié.</returns>
    public float CalculateBuildingEfficiency(string buildingName)
    {
        List<EmployeeData> employees = GetEmployeeList(buildingName);
        BuildingEfficiency buildingEfficiency = buildingEfficiencies.Find(b => b.buildingName == buildingName);

        if (buildingEfficiency == null)
        {
            return 0;
        }

        int buildingIndex = GetBuildingIndexByName(buildingName);
        if (CrisisManager.Instance != null && (CrisisManager.Instance.GetBuildingState(buildingIndex) || CrisisManager.Instance.GetBuildingHackedState(buildingIndex)))
        {
            return -8;
        }

        if (employees.Count == 0)
        {
            return -25;
        }

        float totalEfficiency = 0;

        foreach (EmployeeData employee in employees)
        {
            if (!employee.onStrike)
            {
                totalEfficiency += (employee.workEfficiencyGrade * buildingEfficiency.workEfficiencyCoefficient) +
                                    (employee.workTeamGrade * buildingEfficiency.teamEfficiencyCoefficient);
            }
        }
        totalEfficiency = (totalEfficiency - 8)-ResourceManager.Instance.CurrentTurn*1.3f;
        if (totalEfficiency <= 0)
        {
            return totalEfficiency;
        }
        else
        {
            return (totalEfficiency / buildingEfficiency.nbEmployeeMeta) * 2;
        }
    }

    /// <summary>
    /// Calcule le salaire total de tous les employés en multipliant leur salaire mensuel par 6 (pour obtenir un salaire sur 6 mois).
    /// </summary>
    /// <returns>Le salaire total de tous les employés pour 6 mois.</returns>
    public float CalculateTotalSalaries()
    {
        float totalSalaries = 0;

        foreach (EmployeeData employee in GetAllEmployees())
        {
            totalSalaries += employee.salary * 6;
        }

        return totalSalaries;
    }

    /// <summary>
    /// Démarre une grève dans un bâtiment spécifique en affectant un certain nombre d'employés à la grève.
    /// </summary>
    /// <param name="buildingIndex">L'indice du bâtiment où la grève doit commencer. L'indice détermine la liste des employés concernés.</param>
    /// <param name="numberOfStrikers">Le nombre d'employés qui doivent participer à la grève.</param>
    public void StartStrike(int buildingIndex, int numberOfStrikers)
    {
        List<EmployeeData> employees = GetEmployeeListByBuildingIndex(buildingIndex);
        if (employees != null && employees.Count > 0)
        {
            List<int> randomIndices = GenerateRandomIndices(employees.Count, numberOfStrikers);

            foreach (int index in randomIndices)
            {
                employees[index].onStrike = true;
            }
            Debug.Log("Started strike in building " + buildingIndex + " with " + numberOfStrikers + " strikers.");
        }
    }

    /// <summary>
    /// Termine la grève dans un bâtiment spécifique en réinitialisant le statut de grève de tous les employés.
    /// </summary>
    /// <param name="buildingIndex">L'indice du bâtiment où la grève doit être terminée. Cela détermine les employés à réinitialiser.</param>
    public void EndStrike(int buildingIndex)
    {
        List<EmployeeData> employees = GetEmployeeListByBuildingIndex(buildingIndex);
        if (employees != null)
        {
            foreach (EmployeeData employee in employees)
            {
                employee.onStrike = false;
            }
            Debug.Log("Ended strike in building " + buildingIndex);
        }
    }

    /// <summary>
    /// Obtient la liste des employés affectés à un bâtiment en fonction de son indice.
    /// </summary>
    /// <param name="buildingIndex">L'indice du bâtiment dont on veut obtenir les employés.</param>
    /// <returns>Une liste d'employés affectés au bâtiment spécifié.</returns>
    public List<EmployeeData> GetEmployeeListByBuildingIndex(int buildingIndex)
    {
        switch (buildingIndex)
        {
            case 1: return itEmployees;
            case 2: return teacherEmployees;
            case 3: return adminEmployees;
            case 4: return maintenanceEmployees;
            default: return new List<EmployeeData>();
        }
    }

    /// <summary>
    /// Génère une liste d'indices aléatoires sans répétition, utilisée pour sélectionner des employés pour une grève.
    /// </summary>
    /// <param name="count">Le nombre total d'employés dans la liste.</param>
    /// <param name="numberOfIndices">Le nombre d'indices à générer (c'est-à-dire, le nombre de grévistes).</param>
    /// <returns>Une liste d'indices uniques représentant les employés qui seront affectés à la grève.</returns>
    private List<int> GenerateRandomIndices(int count, int numberOfIndices)
    {
        List<int> indices = new List<int>();
        HashSet<int> usedIndices = new HashSet<int>();

        while (indices.Count < numberOfIndices)
        {
            int randomIndex = UnityEngine.Random.Range(0, count);
            if (!usedIndices.Contains(randomIndex))
            {
                indices.Add(randomIndex);
                usedIndices.Add(randomIndex);
            }
        }

        return indices;
    }

    /// <summary>
    /// Obtient la liste des employés en grève dans un bâtiment spécifique.
    /// </summary>
    /// <param name="buildingIndex">L'indice du bâtiment pour lequel on veut obtenir les employés en grève.</param>
    /// <returns>Une liste des employés en grève dans le bâtiment spécifié.</returns>
    public List<EmployeeData> GetStrikingEmployees(int buildingIndex)
    {
        List<EmployeeData> employees = GetEmployeeListByBuildingIndex(buildingIndex);

        return employees.Where(employee => employee.onStrike).ToList();
    }

    /// <summary>
    /// Diminue la fidélité des employés et renvoie ceux dont la fidélité est inférieure à un seuil pour les renvoyer.
    /// </summary>
    public void DecreaseFidelity()
    {
        foreach (var kvp in resignedEmployees)
        {
            kvp.Value.Clear();
        }

        List<EmployeeData> tempAdminEmployees = new List<EmployeeData>(adminEmployees);
        List<EmployeeData> tempTeacherEmployees = new List<EmployeeData>(teacherEmployees);
        List<EmployeeData> tempMaintenanceEmployees = new List<EmployeeData>(maintenanceEmployees);
        List<EmployeeData> tempItEmployees = new List<EmployeeData>(itEmployees);

        foreach (var employee in tempAdminEmployees)
        {
            employee.fidelityGrade = Mathf.Clamp(employee.fidelityGrade - UnityEngine.Random.Range(0f, 1.2f), 0, maxGrade);
            if (employee.fidelityGrade < 1)
            {
                FireEmployee("Administratif", employee);
                AddToResignedList(employee, "Administratif");
            }
        }

        foreach (var employee in tempTeacherEmployees)
        {
            employee.fidelityGrade = Mathf.Clamp(employee.fidelityGrade - UnityEngine.Random.Range(0f, 0.5f), 0, maxGrade);
            if (employee.fidelityGrade < 1)
            {
                FireEmployee("Enseignant", employee);
                AddToResignedList(employee, "Enseignant");
            }
        }

        foreach (var employee in tempMaintenanceEmployees)
        {
            employee.fidelityGrade = Mathf.Clamp(employee.fidelityGrade - UnityEngine.Random.Range(0f, 0.5f), 0, maxGrade);
            if (employee.fidelityGrade < 1)
            {
                FireEmployee("Entretient", employee);
                AddToResignedList(employee, "Entretient");
            }
        }

        foreach (var employee in tempItEmployees)
        {
            employee.fidelityGrade = Mathf.Clamp(employee.fidelityGrade - UnityEngine.Random.Range(0f, 0.5f), 0, maxGrade);
            if (employee.fidelityGrade < 1)
            {
                FireEmployee("Informatique", employee);
                AddToResignedList(employee, "Informatique");
            }
        }
    }

    private Dictionary<string, List<EmployeeData>> resignedEmployees = new Dictionary<string, List<EmployeeData>>
    {
        { "Administratif", new List<EmployeeData>() },
        { "Enseignant", new List<EmployeeData>() },
        { "Entretient", new List<EmployeeData>() },
        { "Informatique", new List<EmployeeData>() }
    };

    public Dictionary<string, List<EmployeeData>> GetResignedEmployees()
    {
        return resignedEmployees;
    }

    /// <summary>
    /// Ajoute un employé à la liste des employés démissionnaires pour un rôle spécifique.
    /// </summary>
    /// <param name="employee">L'employé à ajouter à la liste des démissionnaires.</param>
    /// <param name="job">Le rôle de l'employé qui a démissionné (par exemple, "Administratif", "Enseignant").</param>
    private void AddToResignedList(EmployeeData employee, string job)
    {
        if (resignedEmployees.ContainsKey(job))
        {
            resignedEmployees[job].Add(employee);
        }
        else
        {
            resignedEmployees[job] = new List<EmployeeData> { employee };
        }
    }

    /// <summary>
    /// Affiche les employés ayant démissionné pour chaque rôle dans la console de débogage.
    /// </summary>
    public void DisplayResignedEmployees()
    {
        foreach (var kvp in resignedEmployees)
        {
            Debug.Log("Resigned Employees in " + kvp.Key + ":");
            foreach (var employee in kvp.Value)
            {
                Debug.Log("Resigned Employee: " + employee.GetFirstName() + " " + employee.GetLastName());
            }
        }
    }

    /// <summary>
    ///Récupère l'indice du batiment grace au nom.
    /// </summary>
    private int GetBuildingIndexByName(string buildingName)
    {
        switch (buildingName)
        {
            case "Entretient":
                return 3;
            case "Enseignant":
                return 2;
            case "Administratif":
                return 4;
            case "Informatique":
                return 1;
            default:
                return -1;
        }
    }

    /// <summary>
    /// Réinitialise les listes des employés, des employés démissionnaires et des coefficients d'efficacité des bâtiments.
    /// </summary>
    public void ResetEmployees()
    {
        adminEmployees.Clear();
        teacherEmployees.Clear();
        maintenanceEmployees.Clear();
        itEmployees.Clear();
        adminRecruitmentList.Clear();
        teacherRecruitmentList.Clear();
        maintenanceRecruitmentList.Clear();
        itRecruitmentList.Clear();
        resignedEmployees.Clear();
        resignedEmployees = new Dictionary<string, List<EmployeeData>>
        {
            { "Administratif", new List<EmployeeData>() },
            { "Enseignant", new List<EmployeeData>() },
            { "Entretient", new List<EmployeeData>() },
            { "Informatique", new List<EmployeeData>() }
        };
        buildingEfficiencies.Clear();
        buildingEfficiencies = new List<BuildingEfficiency>
        {
            new BuildingEfficiency { buildingName = "Administratif", workEfficiencyCoefficient = 0.7f, teamEfficiencyCoefficient = 0.8f, nbEmployeeMeta = 4 },
            new BuildingEfficiency { buildingName = "Enseignant", workEfficiencyCoefficient = 0.7f, teamEfficiencyCoefficient = 0.7f, nbEmployeeMeta = 6 },
            new BuildingEfficiency { buildingName = "Entretient", workEfficiencyCoefficient = 0.4f, teamEfficiencyCoefficient = 0.6f, nbEmployeeMeta = 3},
            new BuildingEfficiency { buildingName = "Informatique", workEfficiencyCoefficient = 0.7f, teamEfficiencyCoefficient = 0.4f, nbEmployeeMeta = 4 }
        };

    }

    public void ClearAllRecruitmentLists()
    {
        adminRecruitmentList.Clear();
        teacherRecruitmentList.Clear();
        maintenanceRecruitmentList.Clear();
        itRecruitmentList.Clear();
    }
}