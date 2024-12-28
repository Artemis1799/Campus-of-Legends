using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère les actions liées à la fin de chaque tour (ou semestre) dans le jeu.
/// Cette classe est responsable de l'avancement du jeu d'un tour à l'autre en mettant à jour les ressources, 
/// les employés, les crises, et en gérant les conditions de victoire ou de défaite du joueur.
/// </summary>
public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    /// <summary>
    /// Passe au tour suivant en mettant à jour les ressources, l'état des employés et les crises.
    /// Si des conditions de défaite ou de victoire sont remplies, l'écran de fin approprié est chargé.
    /// Sinon, la scène de transition pour le semestre suivant est chargée.
    /// </summary>
    public void NextTurn()
    {
        ResourceManager.Instance.CurrentTurn++;
        ResourceManager.Instance.UpdateResourcesBasedOnEfficiency();
        EmployeeManager.Instance.DecreaseFidelity();
        EmployeeManager.Instance.ClearAllRecruitmentLists();
        ResourceManager.Instance.PaySalaries();
        CrisisManager.Instance.EndAllStrikes();
        CrisisManager.Instance.ResolvePendingBuildingStates();
        CrisisManager.Instance.ResolvePendingHackStates();
        CrisisManager.Instance.CheckForCrisis();
        CrisisManager.Instance.DisplayCrisisStatus();
        EmployeeManager.Instance.DisplayResignedEmployees();

        // conditions de victoire et de défaite
        // Vérifie si la situation financière du joueur est trop mauvaise
        if (ResourceManager.Instance.Money < -200000)
        {

            PlayerController gameObject = GameObject.Find("Player").GetComponent<PlayerController>();
            gameObject.ToOutMap(); // Le joueur est envoyé en dehors de la map pour qu'il ne g�ne pas l'animation de fin
            SceneManager.LoadScene("endingSceneLose");
        }
        // Vérifie si le tour 9 est atteint, ce qui pourrait déclencher la fin du jeu
        else if (ResourceManager.Instance.CurrentTurn == 9)
        {
            PlayerController gameObject = GameObject.Find("Player").GetComponent<PlayerController>();
            // Vérifie si l'attractivité est trop faible pour une victoire
            if (ResourceManager.Instance.Attractiveness < 50)
            {
                gameObject.ToOutMap();
                SceneManager.LoadScene("endingSceneLose");
            }
            else
            {
                gameObject.ToOutMap();
                SceneManager.LoadScene("endingSceneWin");
            }
        }
        else if(EmployeeManager.Instance.GetEmployeeListByBuildingIndex(2).Count == 0)
        {
            PlayerController gameObject = GameObject.Find("Player").GetComponent<PlayerController>();
            gameObject.ToOutMap();
            SceneManager.LoadScene("endingSceneLose");
        }
        // Si aucune condition de défaite n'est remplie, passe à la scène de transition pour le semestre suivant
        else
        {
            SceneManager.LoadScene("TransitionSemester");
        }

    }
}
