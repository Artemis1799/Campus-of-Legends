using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère un curseur personnalisé qui suit la souris et peut afficher une texture personnalisée.
/// </summary>
public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture;      // Texture pour le curseur
    public RectTransform cursorTransform; // Référence à l'objet UI du curseur
    public RawImage cursorImage; 
    public static CustomCursor Instance { get; private set; }

    /// <summary>
    /// Méthode appelée lors de l'initialisation de l'objet. Assure que l'instance de cette classe est unique.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Change la texture du curseur et gère sa visibilité.
    /// </summary>
    /// <param name="aCursorTexture">La nouvelle texture à utiliser pour le curseur.</param>
    public void SetCursor(Texture2D aCursorTexture)
    {
        cursorTexture = aCursorTexture;
        if (cursorTexture != null)
        {
            cursorImage.texture = aCursorTexture;
            Cursor.visible = false; // Désactiver le curseur par défaut
            cursorTransform.gameObject.SetActive(true); // Cacher le curseur visuel

        }
        else
        {
            Cursor.visible = true; // Réactiver le curseur par défaut
            cursorTransform.gameObject.SetActive(false); // Cacher le curseur visuel
        }
    }

    /// <summary>
    /// Met à jour la position du curseur personnalisé pour qu'il suive la souris à l'écran.
    /// </summary>
    void Update()
    {
        if (cursorTexture != null && cursorTransform != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            cursorTransform.position = mousePosition + new Vector3(10f, -10f, 0); // Suivre la souris avec un léger décalage
        }
    }
}
