using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// La classe <c>CursorChanger</c> permet de changer l'apparence du curseur de la souris en fonction de l'état de l'interaction,
/// en utilisant des textures personnalisées pour le curseur normal et celui au survol.
/// </summary>
public class CursorChanger : MonoBehaviour
{
    public Texture2D cursorTextureClick;  // Curseur personnalisé clique
    public Texture2D cursorTextureNormal;  // Curseur personnalisé normal

    public Vector2 hotSpot = Vector2.zero;  // Point d'origine du curseur

    /// <summary>
    /// Méthode appelée au démarrage pour initialiser le curseur et s'assurer qu'il est visible.
    /// Définit le curseur par défaut et s'assure qu'il est visible au début.
    /// </summary>
    void Start()
    {
        Cursor.SetCursor(cursorTextureNormal, Vector2.zero, CursorMode.Auto);
        if(Cursor.visible == false)
            Cursor.visible = true;
        CustomCursor.Instance.SetCursor(null);
    }
    /// <summary>
    /// Méthode appelée lorsque le curseur entre en contact avec un objet interactif (survol).
    /// Change le curseur pour la texture de clic.
    /// </summary>
    public void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTextureClick, hotSpot, CursorMode.Auto);
    }

    /// <summary>
    /// Méthode appelée lorsque le curseur quitte un objet interactif (ne survole plus l'objet).
    /// Réinitialise le curseur à son apparence normale.
    /// </summary>
    public void OnMouseExit()
    {
        Cursor.SetCursor(cursorTextureNormal, Vector2.zero, CursorMode.Auto);
    }
}
