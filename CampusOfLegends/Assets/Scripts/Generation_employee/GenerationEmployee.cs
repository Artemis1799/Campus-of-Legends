using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe permettant de generer les employés.
/// </summary>
public class GenerationEmployee : MonoBehaviour
{

    private List<string> femalePrefabNames;

    private List<string> malePrefabNames;

    private GameObject playerPrefab;


    private List<string> firstNames;
    private List<string> lastNames;
    private List<string> jobs;
    private List<string> maleFirstNames;
    private List<string> femaleFirstNames;

    void Awake()
    {
        InitializeLists();
    }

    void InitializeLists()
    {
        femalePrefabNames = new List<string>
    {
        "Character_Female_1",
        "Character_Female_2",
        "Character_Female_3",
        "Character_Female_4"
    };

        malePrefabNames = new List<string>()
        {
            "Character_Male_1",
            "Character_Male_2",
            "Character_Male_3",
            "Character_Male_4",
            "Character_Male_5",
            "Character_Male_6",
            "Character_Male_7",
            "Character_Male_8"
        };


        firstNames = new List<string>()
        {
            "Alice", "Bob", "Charlie", "David", "Eva", "Fiona", "George", "Hannah", "Ian", "Julia",
            "Kevin", "Laura", "Michael", "Nina", "Oscar", "Paul", "Quinn", "Rachel", "Steve", "Tina",
            "Uma", "Victor", "Wendy", "Xander", "Yvonne", "Zack", "Aaron", "Bella", "Chris", "Diana",
            "Ethan", "Freya", "Gareth", "Helen", "Isaac", "Jack", "Katie", "Liam", "Mia", "Noah",
            "Oliver", "Penelope", "Quincy", "Riley", "Sam", "Tara", "Ursula", "Vincent", "Will", "Xena",
            "Yara", "Zoe", "Adam", "Brenda", "Carl", "Deborah", "Eric", "Faith", "Gina", "Harvey", "Ivy",
            "James", "Kara", "Logan", "Megan", "Nathan", "Olivia", "Patrick", "Rebecca", "Simon", "Teresa",
            "Ulrich", "Vera", "Walter", "Ximena", "Yuri", "Zara", "Antonio", "Brianna", "Connor", "Donna",
            "Eli", "Faye", "Grant", "Hailey", "Ian", "Jordan", "Kim", "Lucas", "Maddie", "Nick",
            "Pierre", "Marie", "Jean", "Sophie", "Jacques", "Clément", "Lucie", "Henri",
            "Camille", "Emilie", "Louis", "Charlotte", "Antoine", "Catherine", "Nicolas",
            "Elodie", "Paul", "Julie", "Philippe", "Isabelle", "Yves", "Monique", "André",
            "Martine", "Léa", "René", "Alice", "François", "Claire", "Didier", "Margaux",
            "Olivier", "Suzanne", "Claude", "Hélène", "Alain", "Mireille", "Thierry",
            "Simone", "Gérard", "Michèle", "Pascal", "Valérie", "Michel", "Jacqueline",
            "Christian", "Bernadette", "Gilles", "Stéphanie", "Laurent", "Pascale",
            "Brigitte", "Jean-Claude", "Daniel", "Nadine", "Guillaume", "Marion", "Alain",
            "Thomas", "Aurélie", "Pasquale", "Romain", "Florence", "Adrien", "Christine",
            "Lucas", "Anne", "Sébastien", "Manon", "Samuel", "Virginie", "Alexandre",
            "Chantal", "Damien", "Claire", "Maxime", "Sandrine", "Grégoire", "Élise",
            "Vincent", "Patricia", "Marc", "Corinne", "Hugo", "Amandine", "Rémy", "Morgane",
            "Quentin", "Julie", "Arnaud", "Émilien", "Nathalie", "Xavier", "Valentin",
            "Marine", "Benjamin", "Pauline", "Jonathan", "Delphine", "Franck", "Diane",
            "Fabrice", "Solène", "David"
        };

        lastNames = new List<string>()
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
            "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin",
            "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson",
            "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores",
            "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts",
            "Gomez", "Phillips", "Evans", "Turner", "Diaz", "Parker", "Cruz", "Edwards", "Collins", "Reyes",
            "Stewart", "Morris", "Morales", "Murphy", "Cook", "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper",
            "Peterson", "Bailey", "Reed", "Kelly", "Howard", "Ramos", "Kim", "Cox", "Ward", "Richardson",
            "Watson", "Brooks", "Chavez", "Wood", "James", "Bennett", "Gray", "Mendoza", "Ruiz", "Hughes",
            "Price", "Alvarez", "Castillo", "Sanders", "Patel", "Myers", "Long", "Ross", "Foster", "Jimenez",
            "Martin", "Bernard", "Dubois", "Thomas", "Robert", "Richard", "Petit", "Durand",
            "Leroy", "Moreau", "Simon", "Laurent", "Lefebvre", "Michel", "Garcia", "David",
            "Bertrand", "Roux", "Vincent", "Fournier", "Morel", "Girard", "Andre", "Lefevre",
            "Mercier", "Dupont", "Lambert", "Bonnet", "Francois", "Martinez", "Legrand",
            "Garnier", "Faure", "Rousseau", "Blanc", "Guerin", "Muller", "Henry", "Roussel",
            "Nicolas", "Perrin", "Morin", "Mathieu", "Clement", "Gauthier", "Dumont",
            "Lopez", "Fontaine", "Chevalier", "Robin", "Masson", "Sanchez", "Gonzalez",
            "Nguyen", "Lucas", "Dupuis", "Brun", "Marchand", "Denis", "Rodriguez", "Marie",
            "Renard", "Schmitt", "Roy", "Colin", "Vidal", "Caron", "Picard", "Roger",
            "Fabre", "Aubry", "Pinto", "Rolland", "Barbier", "Arnaud", "Martel", "Lemoine",
            "Humbert", "Benoit", "Bertin", "Rey", "Leblanc", "Dufour", "Perrot", "Jacquet",
            "Lemoine", "Dupuy", "Meunier", "Lacoste", "Leclerc", "Bailly", "Leger", "Paris",
            "Clerc", "Ferreira", "Verdier", "Renaud", "Gilles", "Collet", "Laporte",
            "Besson", "Marty", "Carre"
        };

        jobs = new List<string>()
        {
            "Administratif", "Enseignant", "Entretient", "Informatique"
        };

        maleFirstNames = new List<string>()
        {
            "Bob", "Charlie", "David", "George", "Ian", "Kevin", "Michael", "Oscar", "Paul", "Quinn",
            "Steve", "Victor", "Xander", "Zack", "Aaron", "Chris", "Carl", "Eric", "Gareth", "Harvey",
            "James", "Logan", "Nathan", "Patrick", "Simon", "Ulrich", "Walter", "Yuri", "Antonio", "Connor",
            "Eli", "Grant", "Jordan", "Lucas", "Nick", "Pierre", "Jean", "Jacques", "Henri", "Louis",
            "Antoine", "Nicolas", "Philippe", "Yves", "André", "René", "François", "Didier", "Olivier",
            "Claude", "Alain", "Thierry", "Gérard", "Pascal", "Michel", "Christian", "Gilles", "Laurent",
            "Jean-Claude", "Daniel", "Guillaume", "Alain", "Thomas", "Pasquale", "Romain", "Adrien",
            "Sébastien", "Samuel", "Alexandre", "Damien", "Maxime", "Grégoire", "Vincent", "Marc", "Hugo",
            "Rémy", "Quentin", "Arnaud", "Émilien", "Xavier", "Valentin", "Benjamin", "Jonathan", "Franck",
            "Fabrice", "David"
        };

        femaleFirstNames = new List<string>()
        {
            "Alice", "Eva", "Fiona", "Hannah", "Julia", "Laura", "Nina", "Rachel", "Tina", "Uma", "Wendy",
            "Yvonne", "Bella", "Diana", "Freya", "Helen", "Katie", "Mia", "Penelope", "Riley", "Tara",
            "Xena", "Yara", "Zoe", "Brenda", "Deborah", "Faith", "Gina", "Ivy", "Kara", "Megan", "Olivia",
            "Rebecca", "Teresa", "Vera", "Ximena", "Zara", "Brianna", "Donna", "Faye", "Hailey", "Kim",
            "Maddie", "Marie", "Sophie", "Clément", "Lucie", "Camille", "Emilie", "Charlotte", "Catherine",
            "Elodie", "Julie", "Isabelle", "Monique", "Martine", "Léa", "Claire", "Margaux", "Suzanne",
            "Hélène", "Mireille", "Simone", "Michèle", "Valérie", "Jacqueline", "Bernadette", "Stéphanie",
            "Pascale", "Brigitte", "Nadine", "Marion", "Aurélie", "Florence", "Christine", "Anne", "Manon",
            "Virginie", "Chantal", "Claire", "Sandrine", "Élise", "Patricia", "Corinne", "Amandine", "Morgane",
            "Julie", "Nathalie", "Marine", "Pauline", "Delphine", "Diane", "Solène"
        };
    }

    /// <summary>
    /// Génère un salaire en fonction du rôle et des notes de performance.
    /// </summary>
    /// <param name="job">Le rôle de l'employé.</param>
    /// <param name="efficiencyGrade">La note d'efficacité de l'employé.</param>
    /// <param name="teamGrade">La note de travail en équipe de l'employé.</param>
    /// <returns>Le salaire généré de l'employé.</returns>
    public int GenerateSalary(string job, float efficiencyGrade, float teamGrade)
    {
        int minSal, maxSal;
        float averageGrade = (efficiencyGrade + teamGrade) / 2;

        switch (job)
        {
            case "Enseignant":
                minSal = 1800;
                maxSal = 6000;
                break;
            case "Entretient":
                minSal = 1800;
                maxSal = 2000;
                break;
            case "Administratif":
                minSal = 1800;
                maxSal = 2700;
                break;
            case "Informatique":
                minSal = 2000;
                maxSal = 4000;
                break;
            default:
                minSal = 1800;
                maxSal = 5000;
                break;
        }

        float gradeFactor = Mathf.Lerp(0.75f, 1.25f, averageGrade / 10f);
        int salary = Mathf.RoundToInt(Random.Range(minSal, maxSal) * gradeFactor);

        return Mathf.Clamp(salary, minSal, maxSal);
    }

    /// <summary>
    /// Génère un prénom aléatoire basé sur le sexe spécifié.
    /// </summary>
    /// <param name="sexe">Le sexe de la personne ('M' pour masculin, 'F' pour féminin).</param>
    /// <returns>Le prénom généré.</returns>
    public string GenerateFirstName(char sexe)
    {
        if (sexe == 'M')
        {
            return maleFirstNames[Random.Range(0, maleFirstNames.Count)];
        }
        else
        {
            return femaleFirstNames[Random.Range(0, femaleFirstNames.Count)];
        }
    }


    /// <summary>
    /// Instancie un prefab aléatoire basé sur le sexe.
    /// </summary>
    /// <param name="sexe">Le sexe de l'employé ('M' pour masculin, 'F' pour féminin).</param>
    /// <returns>Le GameObject représentant le prefab de l'employé généré.</returns>

    private GameObject InstantiateRandomPrefab(char sexe)
    {
        List<string> prefabNames = (sexe == 'M') ? malePrefabNames : femalePrefabNames;
        string randomPrefabName = prefabNames[Random.Range(0, prefabNames.Count)];
        string randomPrefabPath = $"{((sexe == 'M') ? "Male" : "Female")}/{randomPrefabName}";
        GameObject prefab = Resources.Load<GameObject>(randomPrefabPath);
        return prefab;
    }


    /// <summary>
    /// Génère un employé avec un ensemble d'attributs aléatoires, y compris le rôle, le salaire, et plus encore.
    /// </summary>
    /// <param name="emp">L'objet Employee à initialiser.</param>
    /// <param name="job">Le rôle de l'employé à générer.</param>
    /// <returns>L'objet Employee initialisé.</returns>
    public Employee GenerateEmployee(Employee emp, string job)
    {
        if (!jobs.Contains(job))
        {
            return null;
        }

        char sexe = Random.Range(0, 2) == 0 ? 'M' : 'F';
        string firstName = GenerateFirstName(sexe);
        string lastName = lastNames[Random.Range(0, lastNames.Count)];

        int age = Random.Range(25, 60);
        float workEfficiencyGrade = (float)System.Math.Round(Random.Range(0f, 5f), 1);
        float workTeamGrade = (float)System.Math.Round(Random.Range(0f, 5f), 1);

        int salary = GenerateSalary(job, workEfficiencyGrade, workTeamGrade);

        GameObject prefabCreer = InstantiateRandomPrefab(sexe);
        float fidelityGrade = (float)System.Math.Round(Random.Range(2f, 5f), 1);


        emp.SetEmployee(firstName, lastName, job, salary, age, workEfficiencyGrade, workTeamGrade, sexe, prefabCreer, fidelityGrade, false );
        return emp;
    }



    /// <summary>
    /// Génère trois employés avec des rôles définis.
    /// </summary>
    /// <param name="job">Le rôle de tous les employés générés.</param>
    /// <returns>Une liste d'employés générés.</returns>
    public List<Employee> GenerateThreeEmployees(string job)
    {
        List<Employee> lstEmp = new List<Employee>();
        for (int i = 0; i < 3; i++)
        {
            GameObject empObj = new GameObject("Employee" + i);
            Employee emp = empObj.AddComponent<Employee>();
            lstEmp.Add(GenerateEmployee(emp, job));
        }
        return lstEmp;
    }

}
