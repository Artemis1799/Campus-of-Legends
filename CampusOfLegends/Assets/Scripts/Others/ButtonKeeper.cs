using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cette classe permet de gérer la visibilité des boutons sur l'interface utilisateur.
/// Au démarrage, tous les boutons sont désactivés. 
/// Les boutons sont réactivés lorsque l'animation est terminée en appelant la méthode `OnAnimationEnd`.
/// </summary>
public class MaintainButtonVisibility : MonoBehaviour
{
    public Button[] buttons;

    /// <summary>
    /// Méthode appelée au démarrage. Elle désactive tous les boutons dans le tableau `buttons`.
    /// </summary>
    void Start()
    {
        // Assurez-vous que les boutons sont initialement désactivés
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Cette méthode est appelée à la fin d'une animation pour activer tous les boutons.
    /// Elle est conçue pour être utilisée comme un événement de fin d'animation.
    /// </summary>
    public void OnAnimationEnd()
    {
        // Activer les boutons à la fin de l'animation
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }
}
