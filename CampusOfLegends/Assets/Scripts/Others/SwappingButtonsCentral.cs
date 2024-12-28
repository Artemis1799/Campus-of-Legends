using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cette classe permet de gérer le changement d'état des boutons et objets dans l'interface utilisateur.
/// Lorsqu'un bouton principal est cliqué, certains objets sont activés et d'autres sont désactivés.
/// Cela permet de contrôler la visibilité de plusieurs éléments de l'interface utilisateur de manière dynamique.
/// </summary>
public class SwappingButtonsCentral : MonoBehaviour
{
    public Button mainButton;
    public GameObject enabling;
    public GameObject disabling1;
    public GameObject disabling2;
    public GameObject disablingForm;

    /// <summary>
    /// Méthode appelée lorsqu'un clic sur le bouton principal se produit.
    /// Cette méthode active et désactive les objets dans l'interface utilisateur en fonction de l'état du bouton.
    /// </summary>
    public void OnMainButtonClick()
    {
        enabling.gameObject.SetActive(true);
        disabling1.gameObject.SetActive(false);
        disabling2.gameObject.SetActive(false);
        disablingForm.gameObject.SetActive(false);
    }

}
