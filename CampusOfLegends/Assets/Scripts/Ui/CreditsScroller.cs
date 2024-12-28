using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// La classe <c>CreditsScroller</c> permet de faire défiler les crédits du jeu verticalement sur l'écran.
/// Elle gère le défilement du contenu et la visibilité du panneau des crédits.
/// </summary>
public class CreditsScroller : MonoBehaviour
{
    public RectTransform content; // Référence au RectTransform du "Content"
    public float scrollSpeed = 100f; // Vitesse de défilement en pixels par seconde
    private float initialPosition;
    public GameObject CreditHolder; 
    private bool isDisplayed = false;
    /// <summary>
    /// Méthode d'initialisation. Elle stocke la position initiale et désactive le panneau des crédits au démarrage.
    /// </summary>
    void Start()
    {
        // Stocker la position initiale pour réinitialiser si besoin
        initialPosition = content.anchoredPosition.y;

        // Assurez-vous que CreditHolder est désactivé au démarrage
        if (CreditHolder != null)
        {
            CreditHolder.SetActive(false);
        }
    }
    /// <summary>
    /// Méthode appelée chaque frame pour déplacer le contenu des crédits vers le haut.
    /// Le contenu défile à une vitesse déterminée par <c>scrollSpeed</c>.
    /// </summary>
    void Update()
    {
        // Faire défiler le contenu vers le haut
        if(isDisplayed)
        {
            content.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            // Si le contenu dépasse l'écran, réinitialiser la position
            /*if (content.anchoredPosition.y > content.sizeDelta.y)
            {
                content.anchoredPosition = new Vector2(content.anchoredPosition.x, initialPosition);
            }*/
        }
    }
    /// <summary>
    /// Méthode pour commencer à afficher les crédits et activer le panneau des crédits.
    /// </summary>
    public void setIsDisplayed()
    {
        isDisplayed = true;
        if (CreditHolder != null)
        {
            CreditHolder.SetActive(true);
        }
    }
}
