using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Gère les ressources globales du jeu, y compris la popularité, l'attractivité, l'argent, 
/// l'efficacité des bâtiments et le nombre d'étudiants. Cette classe met à jour les ressources 
/// en fonction de l'efficacité des bâtiments, gère les salaires des employés et effectue 
/// des calculs relatifs aux ressources du jeu.
/// </summary>
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    private int currentTurn;

    public float popularity;
    public float attractiveness;
    public float money;
    public float numberOfStudents;
    public float batInfoEfficacity = 50;
    public float batAdminEfficacity = 50;
    public float batPersoEfficacity = 50;
    public float batEnseiEfficacity = 50;

    public float Popularity
    {
        get { return popularity; }
        set { popularity = Mathf.Clamp(value, 0, 100); }
    }

    public float Attractiveness
    {
        get { return attractiveness; }
        set { attractiveness = Mathf.Clamp(value, 0, 100); }
    }

    public float Money
    {
        get { return money; }
        set
        {
            money = value;

        }
    }

    public int CurrentTurn
    {
        get { return currentTurn; }
        set { currentTurn = value; }
    }


    public float deltaBatInfoEfficacity;
    public float deltaBatAdminEfficacity;
    public float deltaBatPersoEfficacity;
    public float deltaBatEnseiEfficacity;
    public float deltaPopularity;
    public float deltaAttractiveness;
    public float deltaMoney;

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

    private void Start()
    {
        Popularity = 50;
        Attractiveness = 50;
        Money = 350000;
        currentTurn = 1;
    }
    /// <summary>
    /// Met à jour les ressources et affiche leur état actuel dans la console pour le débogage.
    /// </summary>
    public void UpdateResources()
    {
        Debug.Log("Popularity: " + Popularity);
        Debug.Log("Attractiveness: " + Attractiveness);
        Debug.Log("Money: " + Money);
    }
    /// <summary>
    /// Modifie la popularité et calcule l'écart entre l'ancienne et la nouvelle valeur.
    /// </summary>
    /// <param name="newPopularity">Nouvelle valeur de la popularité.</param>
    public void SetPopularity(float newPopularity)
    {
        deltaPopularity = newPopularity - Popularity;
        Popularity = newPopularity;
    }
    /// <summary>
    /// Modifie l'attractivité et calcule l'écart entre l'ancienne et la nouvelle valeur.
    /// </summary>
    /// <param name="newAttractiveness">Nouvelle valeur de l'attractivité.</param>
    public void SetAttractiveness(float newAttractiveness)
    {
        deltaAttractiveness = newAttractiveness - Attractiveness;
        Attractiveness = newAttractiveness;
    }
    /// <summary>
    /// Ajoute une certaine somme d'argent aux ressources de l'université.
    /// </summary>
    /// <param name="amount">Le montant d'argent à ajouter.</param>
    public void AddMoney(float amount)
    {
        deltaMoney += amount;
        Money += amount;
    }
    /// <summary>
    /// Soustrait une certaine somme d'argent des ressources de l'université.
    /// </summary>
    /// <param name="amount">Le montant d'argent à soustraire.</param>
    public void SubtractMoney(float amount)
    {
        deltaMoney -= amount;
        Money -= amount;
    }
    /// <summary>
    /// Calcule le nombre d'étudiants en fonction de l'attractivité et du nombre d'enseignants.
    /// </summary>
    /// <returns>Le nombre d'étudiants calculé.</returns>
    public int getNumberOfStudents()
    {
        numberOfStudents = 35 * EmployeeManager.Instance.GetEmployeeList("Enseignant").Count*(Attractiveness/100);
        return (int)Mathf.Floor(numberOfStudents);
    }

    /// <summary>
    /// Met à jour les ressources en fonction de l'efficacité des bâtiments (informatique, administratif, entretien, enseignants).
    /// Cela affecte également l'attractivité et l'argent du jeu.
    /// </summary>
    public void UpdateResourcesBasedOnEfficiency()
    {
        deltaBatInfoEfficacity += EmployeeManager.Instance.CalculateBuildingEfficiency("Informatique");
        deltaBatAdminEfficacity += EmployeeManager.Instance.CalculateBuildingEfficiency("Administratif");
        deltaBatPersoEfficacity += EmployeeManager.Instance.CalculateBuildingEfficiency("Entretient");
        deltaBatEnseiEfficacity += EmployeeManager.Instance.CalculateBuildingEfficiency("Enseignant");

        batInfoEfficacity += deltaBatInfoEfficacity;
        batAdminEfficacity += deltaBatAdminEfficacity;
        batPersoEfficacity += deltaBatPersoEfficacity;
        batEnseiEfficacity += deltaBatEnseiEfficacity;

        batInfoEfficacity = Mathf.Clamp(batInfoEfficacity, 0, 100);
        batAdminEfficacity = Mathf.Clamp(batAdminEfficacity, 0, 100);
        batPersoEfficacity = Mathf.Clamp(batPersoEfficacity, 0, 100);
        batEnseiEfficacity = Mathf.Clamp(batEnseiEfficacity, 0, 100);

        SetAttractiveness((batInfoEfficacity + batAdminEfficacity + batPersoEfficacity + batEnseiEfficacity) / 4);
        if (attractiveness < 40)
        {
            AddMoney(7000 * 40);
        }
        else if (attractiveness > 60)
        {
            AddMoney(7000 * 60);
        }
        else
        {
            AddMoney(7000 * attractiveness);
        }
        

    }
    /// <summary>
    /// Effectue le paiement des salaires des employés.
    /// </summary>
    public void PaySalaries()
    {
        float totalSalaries = EmployeeManager.Instance.CalculateTotalSalaries();
        SubtractMoney(totalSalaries);
    }
    /// <summary>
    /// Réinitialise les changements (delta) dans les ressources et l'efficacité des bâtiments.
    /// </summary>
    public void ResetDeltas()
    {
        deltaBatInfoEfficacity = 0;
        deltaBatAdminEfficacity = 0;
        deltaBatPersoEfficacity = 0;
        deltaBatEnseiEfficacity = 0;
        deltaPopularity = 0;
        deltaAttractiveness = 0;
        deltaMoney = 0;
    }
    /// <summary>
    /// Réinitialise toutes les ressources et les valeurs du jeu à leurs états initiaux.
    /// </summary>
    public void ResetRessource()
    {
        Popularity = 50;
        Attractiveness = 50;
        Money = 350000;
        currentTurn = 1;
        batAdminEfficacity = 50;
        batEnseiEfficacity = 50;
        batInfoEfficacity = 50;
        batPersoEfficacity = 50;
        ResetDeltas();

    }

}
