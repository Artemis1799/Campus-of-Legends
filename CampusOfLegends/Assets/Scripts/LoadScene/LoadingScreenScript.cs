using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Ce script gère l'écran de chargement d'une scène dans le jeu. 
/// Il affiche une barre de progression et un texte de pourcentage pour informer l'utilisateur du 
/// processus de chargement de la scène sélectionnée.
/// </summary>
public class LoadingScreenManager : MonoBehaviour
{
    public Slider progressBar; // Référence au Slider de la barre de progression
    public TMP_Text progressText; // Optionnel, pour afficher le pourcentage

    /// <summary>
    /// Méthode Start qui est appelée lorsque le script est activé.
    /// Elle récupère le nom de la scène à charger à partir des PlayerPrefs et lance le chargement de la scène.
    /// </summary>
    private void Start()
    {
        // Récupère le nom de la scène à charger
        string sceneName = PlayerPrefs.GetString("LevelToLoad");
        StartCoroutine(LoadSceneAsync(sceneName));
    }


    /// <summary>
    /// Coroutine qui charge la scène de manière asynchrone tout en mettant à jour la barre de progression.
    /// </summary>
    /// <param name="sceneName">Nom de la scène à charger.</param>
    /// <returns>Retourne un IEnumerator, utilisé pour exécuter des tâches de manière asynchrone.</returns>
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Lance le chargement asynchrone de la scène
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Empêche le changement de scène avant que le chargement ne soit complet
        operation.allowSceneActivation = false;

        // Met à jour la barre de progression pendant le chargement
        while (!operation.isDone)
        {
            // La progression est donnée entre 0 et 0.9
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;

            if (progressText != null)
            {
                progressText.text = ": "+ Mathf.RoundToInt(progress * 100f) + " %";
            }

            // Vérifie si le chargement est terminé
            if (operation.progress >= 0.9f)
            {
                // Affiche la barre pleine avant de charger la scène
                progressBar.value = 1f;
                // Active la scène lorsque le joueur est prêt
                operation.allowSceneActivation = true;
            }

            yield return null; // Attend la prochaine frame
        }
    }
}
