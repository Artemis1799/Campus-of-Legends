using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Cette classe est utilisée pour gérer l'activation d'un objet dans la scène Unity.
/// Lors du démarrage, elle vérifie si l'objet a été assigné et l'active.
/// Cela permet de contrôler la visibilité ou l'activation d'un objet au début du jeu ou lors du chargement d'une scène.
/// </summary>
public class ObjectLoader : MonoBehaviour
{
    public GameObject Object;
    // Start is called before the first frame update
    /// <summary>
    /// Méthode appelée au début du jeu, lors du démarrage de la scène.
    /// Elle active l'objet si celui-ci a été assigné.
    /// </summary>
    void Start()
    {
        if(Object != null)
        {
            Object.gameObject.SetActive(true);

        }
    }


}
