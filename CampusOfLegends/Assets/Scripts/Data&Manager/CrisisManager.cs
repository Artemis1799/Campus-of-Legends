using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Gère les crises au sein des bâtiments, y compris les incendies, les hacks, les grèves et les conflits.
/// Elle garde une trace des états des bâtiments et gère la résolution des différentes crises comme les incendies, les hacks et les grèves.
/// </summary>
public class CrisisManager : MonoBehaviour
{
    public static CrisisManager Instance { get; private set; }
    public Dictionary<int, bool> buildingStates = new Dictionary<int, bool>();
    private FireManager fireManager;
    private HackManager hackManager;
    private Dictionary<int, bool> buildingOnStrike = new Dictionary<int, bool>();
    private Dictionary<int, bool> buildingHacked = new Dictionary<int, bool>();
    private Dictionary<int, List<(EmployeeData, EmployeeData)>> buildingConflicts = new Dictionary<int, List<(EmployeeData, EmployeeData)>>();

    public static EmployeeManager Instance2 { get; private set; }
    private Dictionary<int, bool> buildingsToResolve = new Dictionary<int, bool>();
    private Dictionary<int, bool> hacksToResolve = new Dictionary<int, bool>();
    /// <summary>
    /// Initialise l'instance de CrisisManager et s'assure qu'il n'y a qu'une seule instance dans le jeu.
    /// Configure les états des bâtiments et les références aux autres gestionnaires (FireManager, HackManager).
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeBuildingStates();
            fireManager = FindObjectOfType<FireManager>();
            hackManager = FindObjectOfType<HackManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Définit l'état d'un bâtiment (ouvert ou fermé).
    /// </summary>
    /// <param name="buildingIndex">L'index du bâtiment.</param>
    /// <param name="isClosed">Indique si le bâtiment est fermé (true) ou ouvert (false).</param>
    public void SetBuildingState(int buildingIndex, bool isClosed)
    {
        if (buildingStates.ContainsKey(buildingIndex))
        {
            buildingStates[buildingIndex] = isClosed;
        }
        else
        {
            buildingStates.Add(buildingIndex, isClosed);
        }

        fireManager.UpdateFireDisplay();
    }
    /// <summary>
    /// Récupère l'état d'un bâtiment (ouvert ou fermé).
    /// </summary>
    /// <param name="buildingIndex">L'index du bâtiment.</param>
    /// <returns>Retourne true si le bâtiment est fermé, false s'il est ouvert.</returns>
    public bool GetBuildingState(int buildingIndex)
    {
        if (buildingStates.ContainsKey(buildingIndex))
        {
            return buildingStates[buildingIndex];
        }
        return false;
    }
    /// <summary>
    /// Marque un bâtiment pour la résolution de son état.
    /// </summary>
    /// <param name="buildingIndex">L'index du bâtiment.</param>
    /// <param name="newState">Le nouvel état à définir (ouvert ou fermé).</param>
    public void MarkBuildingForResolution(int buildingIndex, bool newState)
    {
        if (buildingsToResolve.ContainsKey(buildingIndex))
        {
            buildingsToResolve[buildingIndex] = newState;
        }
        else
        {
            buildingsToResolve.Add(buildingIndex, newState);
        }
    }
    /// <summary>
    /// Résout les états des bâtiments en attente de résolution.
    /// </summary>
    public void ResolvePendingBuildingStates()
    {
        foreach (var entry in buildingsToResolve)
        {
            if (buildingStates.ContainsKey(entry.Key))
            {
                SetBuildingState(entry.Key, entry.Value);
            }
        }
        buildingsToResolve.Clear();
    }
    /// <summary>
    /// Récupère les bâtiments qui sont actuellement en feu et qui n'ont pas encore été résolus.
    /// </summary>
    /// <returns>Retourne une liste d'index des bâtiments en feu.</returns>
    public List<int> GetEffectiveBuildingsOnFire()
    {
        List<int> effectiveBuildingsOnFire = new List<int>();

        foreach (var building in buildingStates)
        {
            int buildingIndex = building.Key;
            bool isOnFire = building.Value;

            if (isOnFire && (!buildingsToResolve.ContainsKey(buildingIndex) || buildingsToResolve[buildingIndex]))
            {
                effectiveBuildingsOnFire.Add(buildingIndex);
            }
        }

        return effectiveBuildingsOnFire;
    }


    /// <summary>
    /// Définit l'état d'un bâtiment comme étant hacké ou non.
    /// </summary>
    /// <param name="buildingIndex">L'index du bâtiment.</param>
    /// <param name="isHacked">Indique si le bâtiment est hacké (true) ou non (false).</param>
    public void SetBuildingHacked(int buildingIndex, bool isHacked)
    {
        if (buildingHacked.ContainsKey(buildingIndex))
        {
            buildingHacked[buildingIndex] = isHacked;
        }
        else
        {
            buildingHacked.Add(buildingIndex, isHacked);
        }

        hackManager.UpdateHackDisplay();
    }
    /// <summary>
    /// Récupère l'état hacké d'un bâtiment.
    /// </summary>
    /// <param name="buildingIndex">L'index du bâtiment.</param>
    /// <returns>Retourne true si le bâtiment est hacké, false sinon.</returns>
    public bool GetBuildingHackedState(int buildingIndex)
    {
        if (buildingHacked.ContainsKey(buildingIndex))
        {
            return buildingHacked[buildingIndex];
        }
        return false;
    }
    /// <summary>
    /// Récupère la liste des bâtiments qui sont actuellement hackés.
    /// </summary>
    /// <returns>Retourne une liste des index des bâtiments hackés.</returns>
    public List<int> GetBuildingsHacked()
    {
        return buildingHacked
            .Where(building => building.Value)
            .Select(building => building.Key)
            .ToList();
    }
    /// <summary>
    /// Marque un hack de bâtiment pour résolution.
    /// </summary>
    /// <param name="buildingIndex">L'index du bâtiment.</param>
    /// <param name="newState">L'état à définir pour le bâtiment (hacké ou non).</param>
    public void MarkHackForResolution(int buildingIndex, bool newState)
    {
        if (hacksToResolve.ContainsKey(buildingIndex))
        {
            hacksToResolve[buildingIndex] = newState;
        }
        else
        {
            hacksToResolve.Add(buildingIndex, newState);
        }
    }
    /// <summary>
    /// Résout les hacks de bâtiments en attente de résolution.
    /// </summary>
    public void ResolvePendingHackStates()
    {
        foreach (var entry in hacksToResolve)
        {
            if (buildingHacked.ContainsKey(entry.Key))
            {
                SetBuildingHacked(entry.Key, entry.Value);
            }
        }
        hacksToResolve.Clear();
    }
    /// <summary>
    /// Récupère les bâtiments qui sont actuellement hackés et qui n'ont pas encore été résolus.
    /// </summary>
    /// <returns>Retourne une liste d'index des bâtiments hackés.</returns>
    public List<int> GetEffectiveBuildingsHacked()
    {
        List<int> effectiveBuildingsHacked = new List<int>();

        foreach (var building in buildingHacked)
        {
            int buildingIndex = building.Key;
            bool isHacked = building.Value;

            if (isHacked && (!hacksToResolve.ContainsKey(buildingIndex) || hacksToResolve[buildingIndex]))
            {
                effectiveBuildingsHacked.Add(buildingIndex);
            }
        }

        return effectiveBuildingsHacked;
    }

    /// <summary>
    /// Initialise les états des bâtiments, les grèves, les hacks et les conflits.
    /// </summary>
    private void InitializeBuildingStates()
    {
        buildingStates.Add(1, false);
        buildingStates.Add(2, false);
        buildingStates.Add(3, false);
        buildingStates.Add(4, false);

        buildingOnStrike.Add(1, false);
        buildingOnStrike.Add(2, false);
        buildingOnStrike.Add(3, false);
        buildingOnStrike.Add(4, false);

        buildingHacked.Add(1, false);
        buildingHacked.Add(2, false);
        buildingHacked.Add(3, false);
        buildingHacked.Add(4, false);

        buildingConflicts.Add(1, new List<(EmployeeData, EmployeeData)>());
        buildingConflicts.Add(2, new List<(EmployeeData, EmployeeData)>());
        buildingConflicts.Add(3, new List<(EmployeeData, EmployeeData)>());
        buildingConflicts.Add(4, new List<(EmployeeData, EmployeeData)>());
    }
    /// <summary>
    /// Vérifie si une crise (incendie, grève, hack) se déclenche en fonction des chances calculées.
    /// </summary>
    public void CheckForCrisis()
    {
        float fireBaseChance = 3;
        float maintenanceEfficiency = ResourceManager.Instance.batPersoEfficacity;
        int maintenanceEmployees = EmployeeManager.Instance.GetEmployeeList("Entretient").Count;

        int fireChance = Mathf.FloorToInt(fireBaseChance * (1 + maintenanceEfficiency / 100))
                         - Mathf.FloorToInt(maintenanceEmployees / 10f);

        // Calcul des chances pour une grève
        float strikeBaseChance = 3;
        float adminEfficiency = ResourceManager.Instance.batAdminEfficacity;
        int adminEmployees = EmployeeManager.Instance.GetEmployeeList("Administratif").Count;

        int strikeChance = Mathf.FloorToInt(strikeBaseChance * (1 + adminEfficiency / 100))
                           - Mathf.FloorToInt(adminEmployees / 10f);

        // Calcul des chances pour un hack
        float hackBaseChance = 3;
        float itEfficiency = ResourceManager.Instance.batInfoEfficacity;
        int itEmployees = EmployeeManager.Instance.GetEmployeeList("Informatique").Count;

        int hackChance = Mathf.FloorToInt(hackBaseChance * (1 + itEfficiency / 100))
                         - Mathf.FloorToInt(itEmployees / 10f);

        Debug.Log("Chance feu : " + fireChance + " | Chance grève : " + strikeChance + " | Chance hack : " + hackChance);

        // Vérification des crises
        if (fireChance < 0 || Random.Range(0, fireChance) == 0)
        {
            int randomBuildingIndex = Random.Range(1, 5);
            SetBuildingState(randomBuildingIndex, true);
            Debug.Log("Building " + randomBuildingIndex + " is on fire!");
        }

        if (strikeChance < 0 || Random.Range(0, strikeChance) == 0)
        {
            StartStrike();
            Debug.Log("Strike started in all buildings!");
        }

        if (hackChance < 0 || Random.Range(0, hackChance) == 0)
        {
            int randomBuildingIndex = Random.Range(1, 5);
            SetBuildingHacked(randomBuildingIndex, true);
            Debug.Log("Building " + randomBuildingIndex + " has been hacked!");
        }

        StartConflict();
    }
    /// <summary>
    /// Démarre une grève dans tous les bâtiments.
    /// </summary>
    public void StartStrike()
    {
        foreach (int buildingIndex in buildingStates.Keys)
        {
            if (!buildingOnStrike[buildingIndex])
            {
                List<EmployeeData> employees = EmployeeManager.Instance.GetEmployeeListByBuildingIndex(buildingIndex);

                if (employees == null || employees.Count == 0)
                {
                    continue;
                }
                buildingOnStrike[buildingIndex] = true;
                int numberOfStrikers = Random.Range(2, 5);
                EmployeeManager.Instance.StartStrike(buildingIndex, numberOfStrikers);
            }
        }
    }
    /// <summary>
    /// Met fin à la grève dans un bâtiment spécifique.
    /// </summary>
    /// <param name="buildingIndex">L'index du bâtiment où la grève doit être terminée.</param>
    public void EndStrike(int buildingIndex)
    {
        if (buildingOnStrike.ContainsKey(buildingIndex) && buildingOnStrike[buildingIndex])
        {
            buildingOnStrike[buildingIndex] = false;
            EmployeeManager.Instance.EndStrike(buildingIndex);
        }
    }
    /// <summary>
    /// Met fin à toutes les grèves dans tous les bâtiments.
    /// </summary>
    public void EndAllStrikes()
    {
        List<int> keysToUpdate = new List<int>();

        foreach (int buildingIndex in buildingOnStrike.Keys)
        {
            if (buildingOnStrike[buildingIndex])
            {
                keysToUpdate.Add(buildingIndex);
            }
        }

        foreach (int buildingIndex in keysToUpdate)
        {
            EndStrike(buildingIndex);
        }
    }
    /// <summary>
    /// Démarre des conflits entre les employés dans les bâtiments.
    /// </summary>
    public void StartConflict()
    {
        foreach (int buildingIndex in buildingStates.Keys)
        {
            if (Random.Range(0, 2) == 0)
            {
                // Récupérer les employés du bâtiment
                List<EmployeeData> employees = EmployeeManager.Instance.GetEmployeeListByBuildingIndex(buildingIndex);

                // Filtrer les employés qui ne sont pas déjà dans un conflit et qui ont une note inférieure à 1
                List<EmployeeData> conflictCandidates = employees
                    .Where(employee => employee.workTeamGrade < 2 &&
                                       !buildingConflicts[buildingIndex].Any(conflict => conflict.Item1 == employee || conflict.Item2 == employee))
                    .ToList();

                // Tant qu'il y a au moins 2 employés disponibles, créer des paires
                while (conflictCandidates.Count >= 2)
                {
                    // Sélectionner deux employés au hasard
                    EmployeeData employee1 = conflictCandidates[Random.Range(0, conflictCandidates.Count)];
                    conflictCandidates.Remove(employee1); // Retirer le premier employé

                    EmployeeData employee2 = conflictCandidates[Random.Range(0, conflictCandidates.Count)];
                    conflictCandidates.Remove(employee2); // Retirer le second employé

                    // Réduire leur efficacité et ajouter le conflit
                    employee1.workEfficiencyGrade /= 2;
                    employee2.workEfficiencyGrade /= 2;

                    buildingConflicts[buildingIndex].Add((employee1, employee2));

                    Debug.Log($"Conflict created in building {buildingIndex}: {employee1.GetFirstName()} vs {employee2.GetFirstName()}");
                }
            }
        }
    }
    /// <summary>
    /// Supprime un employé d'un conflit et restaure son efficacité.
    /// </summary>
    /// <param name="employee">L'employé à retirer du conflit.</param>
    public void RemoveFromConflicts(EmployeeData employee)
    {
        foreach (var buildingIndex in buildingConflicts.Keys.ToList())
        {
            var conflicts = buildingConflicts[buildingIndex];

            // Trouver le conflit impliquant cet employé
            var conflict = conflicts.FirstOrDefault(c => c.Item1 == employee || c.Item2 == employee);
            if (conflict != default)
            {
                // Restaurer l'efficacité des employés
                conflict.Item1.workEfficiencyGrade *= 2;
                conflict.Item2.workEfficiencyGrade *= 2;

                // Supprimer le conflit
                conflicts.Remove(conflict);

            }
        }
    }
    /// <summary>
    /// Affiche tous les conflits actuellement dans les bâtiments.
    /// </summary>
    public void DisplayAllConflicts()
    {
        foreach (var buildingIndex in buildingConflicts.Keys)
        {
            var conflicts = buildingConflicts[buildingIndex];
            if (conflicts.Count > 0)
            {
                Debug.Log($"Building {buildingIndex} has the following conflicts:");
                foreach (var conflict in conflicts)
                {
                    Debug.Log($"Conflict: {conflict.Item1.GetFirstName()} vs {conflict.Item2.GetFirstName()}");
                }
            }
            else
            {
                Debug.Log($"Building {buildingIndex} has no conflicts.");
            }
        }
    }
    /// <summary>
    /// Récupère une liste des bâtiments qui sont actuellement en feu.
    /// </summary>
    /// <returns>Retourne une liste d'index des bâtiments en feu.</returns>
    public List<int> GetBuildingsOnFire()
    {
        return buildingStates
            .Where(building => building.Value)
            .Select(building => building.Key)
            .ToList();
    }
    /// <summary>
    /// Récupère un dictionnaire des bâtiments en grève avec le nombre d'employés en grève.
    /// </summary>
    /// <returns>Retourne un dictionnaire avec l'index du bâtiment et le nombre d'employés en grève.</returns>
    public Dictionary<int, int> GetBuildingsOnStrike()
    {
        var strikeSummary = new Dictionary<int, int>();

        foreach (var building in buildingOnStrike)
        {
            if (building.Value) // si le batiment est en gr�ve
            {
                int buildingIndex = building.Key;
                int strikersCount = EmployeeManager.Instance.GetStrikingEmployees(buildingIndex).Count;
                strikeSummary[buildingIndex] = strikersCount;
            }
        }

        return strikeSummary; // Renvoie {b�timent : nombre d'employ�s en gr�ve}
    }
    /// <summary>
    /// Récupère tous les conflits dans tous les bâtiments.
    /// </summary>
    /// <returns>Retourne un dictionnaire des conflits par bâtiment.</returns>
    public Dictionary<int, List<(EmployeeData, EmployeeData)>> GetAllConflicts()
    {
        return buildingConflicts;
    }
    /// <summary>
    /// Affiche le statut de toutes les crises (incendie, grève, hack, conflits).
    /// </summary>
    public void DisplayCrisisStatus()
    {
        Debug.Log("=== CRISIS STATUS REPORT ===");

        // Afficher les bâtiments en feu
        List<int> buildingsOnFire = GetBuildingsOnFire();
        if (buildingsOnFire.Count > 0)
        {
            Debug.Log("Buildings on Fire:");
            foreach (int building in buildingsOnFire)
            {
                Debug.Log($"- Building {building}");
            }
        }
        else
        {
            Debug.Log("No buildings are currently on fire.");
        }

        // Afficher les bâtiments en grève
        var strikingBuildings = GetBuildingsOnStrike();
        if (strikingBuildings.Count > 0)
        {
            Debug.Log("Buildings on Strike:");
            foreach (var kvp in strikingBuildings)
            {
                Debug.Log($"- Building {kvp.Key} has {kvp.Value} employees on strike.");
            }
        }
        else
        {
            Debug.Log("No buildings are currently on strike.");
        }

        // Afficher les bâtiments hackés
        List<int> buildingsHacked = GetBuildingsHacked();
        if (buildingsHacked.Count > 0)
        {
            Debug.Log("Buildings hacked:");
            foreach (int building in buildingsHacked)
            {
                Debug.Log($"- Building {building}");
            }
        }
        else
        {
            Debug.Log("No buildings are currently hacked.");
        }

        // Afficher les conflits
        DisplayAllConflicts();

        Debug.Log("=== END OF CRISIS STATUS REPORT ===");
    }
    /// <summary>
    /// Récupère tous les conflits sous forme de chaînes de caractères.
    /// </summary>
    /// <returns>Retourne une liste de chaînes représentant les conflits.</returns>
    public List<string[]> GetAllConflictsString()
    {
        List<string[]> retour = new List<string[]>();
        foreach (var buildingIndex in buildingConflicts.Keys)
        {
            var conflicts = buildingConflicts[buildingIndex];
            if (conflicts.Count > 0)
            {
                foreach (var conflict in conflicts)
                {
                    string BatName = "tmp";
                    switch (buildingIndex)
                    {
                        case 1:
                            BatName = "l'informatique";
                            break;
                        case 2:
                            BatName = "l'enseignement";
                            break;
                        case 3:
                            BatName = "l'administration";
                            break;
                        case 4:
                            BatName = "l'entretien";
                            break;

                    }
                    retour.Add(new string[] { BatName, conflict.Item1.GetFirstName() + " " + conflict.Item1.GetLastName(), conflict.Item2.GetFirstName() + " " + conflict.Item2.GetLastName() });
                }
            }
        }
        return retour;
    }/// <summary>
/// Récupère le nombre total de crises globales (conflits d'employés + événements de crise).
/// </summary>
/// <returns>Retourne le nombre total de crises globales.</returns>
    public int getNumberOfGlobalCrisis()
    {
        return getNumberOfEmpConflicts() + getNumberOfEventCrisis();
    }
    /// <summary>
    /// Récupère le nombre d'événements de crise (incendie, grève, hack, démission d'employés).
    /// </summary>
    /// <returns>Retourne le nombre d'événements de crise.</returns>
    public int getNumberOfEventCrisis()
    {
        Dictionary<string, List<EmployeeData>> resignedEmployees = EmployeeManager.Instance.GetResignedEmployees();
        int compteur = 0;
        foreach (var emp in resignedEmployees)
        {
            compteur += emp.Value.Count;
        }
        return GetEffectiveBuildingsOnFire().Count + GetBuildingsOnStrike().Count + compteur + GetEffectiveBuildingsHacked().Count;
    }
    /// <summary>
    /// Récupère le nombre total de conflits d'employés dans tous les bâtiments.
    /// </summary>
    /// <returns>Retourne le nombre total de conflits d'employés.</returns>
    public int getNumberOfEmpConflicts()
    {
        int compteur = 0;
        foreach (var bats in buildingConflicts)
        {
            compteur += bats.Value.Count;
        }
        return compteur;
    }
    /// <summary>
    /// Réinitialise tous les états de crise (incendie, grève, hack, conflits) à leurs valeurs par défaut.
    /// </summary>
    public void ResetAllCrisis()
    {
        buildingStates = new Dictionary<int, bool>();
        buildingOnStrike = new Dictionary<int, bool>();
        buildingHacked = new Dictionary<int, bool>();
        buildingConflicts = new Dictionary<int, List<(EmployeeData, EmployeeData)>>();
        InitializeBuildingStates();
        fireManager.DisableFires();
        hackManager.DisableHack();
    }
}
