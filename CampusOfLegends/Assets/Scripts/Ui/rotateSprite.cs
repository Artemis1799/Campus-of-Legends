using UnityEngine;

/// <summary>
/// La classe <c>rotateSprite</c> permet de faire tourner un objet de manière continue.
/// Elle applique une rotation autour d'un axe spécifié avec une vitesse définie.
/// </summary>
public class rotateSprite : MonoBehaviour
{
    // Vitesse de rotation
    public float rotationSpeed = 50f;

    // Axe de rotation (X, Y, Z)
    public Vector3 rotationAxis = new Vector3(0, 1, 0); // Par défaut rotation sur l'axe Y

    /// <summary>
    /// Cette méthode est appelée une fois par frame. Elle applique la rotation à l'objet
    /// en fonction de la vitesse de rotation et de l'axe spécifié.
    /// </summary>
    void Update()
    {
        // Appliquer une rotation continue
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
