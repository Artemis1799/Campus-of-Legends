using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Ce script gère les interactions du menu principal du jeu.
/// Il permet de commencer une partie, d'ouvrir et fermer les paramètres, et de quitter l'application.
/// </summary>
public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject settingsMenu;


    /// <summary>
    /// Méthode appelée lorsqu'on clique sur le bouton "Start Game".
    /// Elle enregistre le nom de la scène à charger et charge l'écran de chargement.
    /// </summary>
    public void StartGame()
    {
        PlayerPrefs.SetString("LevelToLoad", levelToLoad);
        SceneManager.LoadScene("LoadingScreen");
    }

    /// <summary>
    /// Méthode appelée lorsqu'on clique sur le bouton "Settings".
    /// Elle active le menu des paramètres.
    /// </summary>
    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
    }


    /// <summary>
    /// Méthode appelée pour fermer le menu des paramètres.
    /// </summary>
    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }

    /// <summary>
    /// Méthode appelée pour quitter l'application lorsque l'utilisateur clique sur le bouton "Quit".
    /// </summary>

    public void Quit()
    {
        Application.Quit();
    }
}
