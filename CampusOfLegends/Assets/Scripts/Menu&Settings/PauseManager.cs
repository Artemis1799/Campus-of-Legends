using UnityEngine;


/// <summary>
/// Ce script gère la mise en pause du jeu en activant et désactivant les menus de pause et de paramètres.
/// Il permet de basculer entre les menus de pause et de paramètres à l'aide de la touche 'Échap'.
/// </summary>
public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;


    /// <summary>
    /// Méthode appelée à chaque frame pour vérifier les entrées de l'utilisateur.
    /// Elle permet de basculer entre les menus de pause et de paramètres lorsque l'utilisateur appuie sur la touche 'Échap'.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenu.activeSelf)
            {
                settingsMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
            }
        }
    }
}