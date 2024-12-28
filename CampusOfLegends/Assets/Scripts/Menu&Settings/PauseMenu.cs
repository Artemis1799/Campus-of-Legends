using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
/// <summary>
/// Ce script gère le comportement du menu de pause, du menu des paramètres et du bouton "Next Semester" dans le jeu.
/// Il permet d'afficher ou de masquer le menu de pause et les paramètres en fonction des actions du joueur.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject nextSemesterButton;
    public static bool isPaused = false;
    /// <summary>
    /// Vérifie si la touche 'Échap' a été pressée. Si c'est le cas, elle bascule entre l'état de pause et d'activation du menu de pause.
    /// Le bouton "Next Semester" est également caché ou affiché en fonction de l'état du jeu.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "mapV3")
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            nextSemesterButton.SetActive(!isPaused);

            if (settingsMenu.activeSelf)
            {
                settingsMenu.SetActive(false);
                nextSemesterButton.SetActive(true);
            }
        }
    }
    /// <summary>
    /// Méthode pour revenir au menu principal. Elle charge la scène "menuPrincipal".
    /// </summary>
    public void Menu()
    {
        SceneManager.LoadScene("menuPrincipal");
    }
    /// <summary>
    /// Méthode qui affiche le menu des paramètres.
    /// </summary>
    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
    }
    /// <summary>
    /// Méthode pour fermer le menu des paramètres.
    /// </summary>
    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }
    /// <summary>
    /// Méthode pour fermer le menu de pause et réactiver le bouton "Next Semester".
    /// </summary>
    public void ClosePauseMenu()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        nextSemesterButton.SetActive(true);
    }
    /// <summary>
    /// Méthode pour basculer l'état du menu de pause en fonction de l'appui sur le bouton de pause dans le menu.
    /// </summary>
    public void SettingsButtonPressed()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        nextSemesterButton.SetActive(!isPaused);

    }
}
