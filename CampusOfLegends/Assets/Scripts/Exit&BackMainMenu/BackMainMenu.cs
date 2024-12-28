using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMainMenu : MonoBehaviour
{
    /// <summary>
    /// Méthode appelée lors du clic pour revenir au menu principal.
    /// Elle effectue les étapes suivantes :
    /// - Envoie le joueur hors de la carte en cours.
    /// - Réinitialise le jeu à son état initial.
    /// - Charge la scène du menu principal.
    /// </summary>
    public void OnClickBackToMainMenu()
    {
        PlayerController gameObject = GameObject.Find("Player").GetComponent<PlayerController>();
        gameObject.ToOutMap();
        ResetGame();
        SceneManager.LoadScene("menuPrincipal");
    }

    /// <summary>
    /// Réinitialise le jeu à son état initial pour pouvoir recommencer une partie.
    /// Les actions effectuées sont :
    /// - Réinitialisation des ressources du jeu (argent, popularité, attractivité, efficacité).
    /// - Suppression des employés.
    /// - Réinitialisation des crises.
    /// - Réinitialisation de la position du joueur.
    /// </summary>
    private void ResetGame()
    {
        Debug.Log("Reset Game");
        PauseMenu.isPaused = false;
        // Réinitialisation des ressources (argent, popularité, attractivité, efficacité)
        ResourceManager resourceManager = GameObject.Find("GameManager").GetComponent<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager not found");
            return;
        }
        else
        {
            resourceManager.ResetRessource();
        }
        // Suppression des employés
        EmployeeManager empMng = GameObject.Find("GameManager").GetComponent<EmployeeManager>();
        if (empMng == null)
        {
            Debug.LogError("EmployeeManager not found");
            return;
        }
        else
        {
            empMng.ResetEmployees();
        }
 
        // Réinitialisation des crises
        CrisisManager.Instance.ResetAllCrisis();

        PlayerPositionManager playerPositionManager = GameObject.Find("GameManager").GetComponent<PlayerPositionManager>();
        if (playerPositionManager == null)
        {
            Debug.LogError("PlayerPositionManager not found");
            return;
        }
        else
        {
            playerPositionManager.ResetPosition();
        }

    }
}
