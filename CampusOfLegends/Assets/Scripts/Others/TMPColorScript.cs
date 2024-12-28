using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cette classe permet de changer la couleur d'un matériau au démarrage du jeu.
/// Elle modifie la couleur du matériau attaché au GameObject en définissant sa couleur principale.
/// </summary>
public class TMPColorScript : MonoBehaviour
{
    // Start is called before the first frame update

    /// <summary>
    /// Méthode appelée au début de la scène, au démarrage du jeu.
    /// Cette méthode récupère le composant `Renderer` du GameObject et définit la couleur de son matériau.
    /// </summary>
    void Start()
    {
        Color couleur = Color.gray;

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color",couleur);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
