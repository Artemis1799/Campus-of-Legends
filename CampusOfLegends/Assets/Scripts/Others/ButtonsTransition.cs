using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cette classe gère le comportement du bouton principal.
/// Lorsqu'on clique sur ce bouton, elle active ou désactive un effet de glissement sur un autre objet via un Animator.
/// </summary>
public class ButtonSlide : MonoBehaviour
{
    public Animator animator;
    public Button mainButton;

    /// <summary>
    /// Méthode appelée au démarrage pour lier l'événement de clic du bouton principal.
    /// </summary>
    void Start()
    {
        mainButton.onClick.AddListener(OnMainButtonClick);
    }

    /// <summary>
    /// Méthode appelée lors du clic sur le bouton principal.
    /// Elle active ou désactive l'animation de glissement en fonction de l'état actuel de l'Animator.
    /// </summary>
    void OnMainButtonClick()
    {
        animator.SetBool("IsSliding", !animator.GetBool("IsSliding"));
    }
}
