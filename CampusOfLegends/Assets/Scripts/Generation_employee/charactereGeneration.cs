using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class charactereGeneration : MonoBehaviour
{
    private GameObject playerPrefab;
    private void Awake()
    {
        playerPrefab = Resources.Load<GameObject>("Character/Player");
    }

    /// <summary>
    /// Appelé avant la première frame d'update. 
    /// Cette méthode effectue les actions suivantes :
    /// - Trouve les objets nommés "Cube1", "Cube2" et "Cube3" dans la scène.
    /// - Instancie le prefab du joueur s'il a été chargé correctement.
    /// - Positionne l'instance du joueur à la position (0, 0, 0).
    /// - Affiche un message dans la console en fonction du succès ou de l'échec du chargement du prefab.
    /// </summary>
    void Start()
    {
        GameObject cube1 = GameObject.Find("Cube1");
        GameObject cube2 = GameObject.Find("Cube2");
        GameObject cube3 = GameObject.Find("Cube3");

        if (playerPrefab != null)
        {
            // Instancier le prefab dans la scène
            GameObject playerInstance = Instantiate(playerPrefab);

            // Positionner l'objet dans la scène
            playerInstance.transform.position = new Vector3(0, 0, 0);

            Debug.Log("Le prefab Player a été instancié avec succès !");
        }
        else
        {
            Debug.LogError("Le prefab Player n'a pas été trouvé dans Resources/Character !");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
