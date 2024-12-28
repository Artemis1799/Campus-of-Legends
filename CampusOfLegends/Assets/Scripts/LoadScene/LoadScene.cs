using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
/// <summary>
/// Ce script gère le changement de scène lorsque le joueur entre en collision avec un objet (typiquement un bâtiment). 
/// Il vérifie l'état du bâtiment (ouvert ou fermé) et effectue la transition vers une nouvelle scène si le bâtiment est accessible.
/// </summary>
public class LoadScene : MonoBehaviour
{
    public string sceneName;
    public int indexBat;
    public GameObject imTransition;

    /// <summary>
    /// Méthode appelée lorsque le joueur entre en collision avec l'objet auquel ce script est attaché.
    /// </summary>
    /// <param name="collision">Données de la collision détectée.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerController playerController = collision.collider.GetComponent<PlayerController>();
            if (playerController != null && playerController.clickedOnBuilding)
            {
                bool isClosed = CrisisManager.Instance.GetBuildingState(indexBat);
                if (isClosed)
                {
                    HighlightBuilding highlightBuilding = GetComponent<HighlightBuilding>();
                    if (highlightBuilding != null)
                    {
                        highlightBuilding.HighlightInRed();
                    }
                    return;
                }
                if(!Cursor.visible)Cursor.visible = true; // Réactiver le curseur par défaut

                Debug.Log("Le joueur rentre dans : " + sceneName);
                PlayerPositionManager playerPositionManager = FindObjectOfType<PlayerPositionManager>();

                imTransition.GetComponent<Transition>().loadNextScene(sceneName);
                if (playerPositionManager != null)
                {
                    playerPositionManager.SetLastBuildingIndex(indexBat); // Enregistre le dernier b�timent quitt�
                    playerPositionManager.DisablePlayer();
                }
                // Charger la sc�ne du b�timent
                //SceneManager.LoadScene(sceneName);
            }
        }
    }

    /// <summary>
    /// Recherche un enfant par son nom dans un objet parent donné, de manière récursive.
    /// </summary>
    /// <param name="parent">Objet parent dans lequel chercher.</param>
    /// <param name="name">Nom de l'enfant à chercher.</param>
    /// <returns>Le GameObject trouvé ou null si non trouvé.</returns>
    private GameObject FindChildByName(GameObject parent, string name)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.gameObject.name == name)
                return child.gameObject;

            GameObject found = FindChildByName(child.gameObject, name);
            if (found != null)
                return found;
        }
        return null;
    }

}
