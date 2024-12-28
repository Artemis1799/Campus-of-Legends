using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// La classe <c>SceneChanger</c> est responsable du changement de scènes dans le jeu.
/// Elle gère le processus de sortie d'un bâtiment et le déplacement du joueur vers une nouvelle scène.
/// </summary>
public class SceneChanger : MonoBehaviour
{

    /// <summary>
    /// Méthode qui permet de quitter un bâtiment et de charger une nouvelle scène.
    /// Elle enregistre l'index du dernier bâtiment quitté et désactive le joueur avant de charger la scène de la carte.
    /// </summary>
    /// <param name="buildingIndex">L'index du bâtiment actuel pour savoir où le joueur était avant de quitter.</param>
    public void ExitBuilding(int buildingIndex)
    {
        PlayerPositionManager playerPositionManager = FindObjectOfType<PlayerPositionManager>();
        if (playerPositionManager != null)
        {
            playerPositionManager.SetLastBuildingIndex(buildingIndex); // Enregistre le dernier bâtiment quitté
            playerPositionManager.DisablePlayer();
            FireManager.Instance.DisableFires();
        }

        SceneManager.LoadSceneAsync("mapV3");

        // Appeler HandlePlayerPosition après le chargement de la scène
        if (playerPositionManager != null)
        {
            playerPositionManager.HandlePlayerPosition();
            FireManager.Instance.EnableFires();
        }
    }


    /// <summary>
    /// Méthode qui permet de changer de scène en fonction du nom de la scène.
    /// </summary>
    /// <param name="sceneName">Le nom de la scène à charger.</param>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
