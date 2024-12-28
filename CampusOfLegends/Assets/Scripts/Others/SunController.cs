using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    /// <summary>
    /// Cette classe contrôle la simulation du mouvement du soleil dans la scène en fonction du temps.
    /// Le soleil suit une trajectoire circulaire autour du centre de la scène, avec un mouvement continu, et ajuste
    /// l'intensité et la couleur de la lumière directionnelle pour simuler les variations de la lumière en fonction de l'heure de la journée.
    /// </summary>
    /// 
    public float rotationSpeed = 0.1f; // Vitesse de rotation du soleil
    public float radius = 100f; // Rayon de l'orbite du soleil
    public float height = 50f; // Hauteur du soleil par rapport au centre de la scène
    public Vector3 center = Vector3.zero; // Centre de la scène
    public Light sunLight; // Référence à la lumière directionnelle


    /// <summary>
    /// Méthode appelée à chaque frame pour mettre à jour la position du soleil et ajuster l'intensité de la lumière.
    /// Le soleil se déplace en fonction du temps, et l'intensité ainsi que la couleur de la lumière directionnelle
    /// sont ajustées pour simuler les changements de lumière pendant la journée.
    /// </summary>
    private void Update()
    {
        // Calculer l'angle de rotation en fonction du temps écoulé
        float angle = Time.time * rotationSpeed;

        // Calculer la position du soleil en fonction de l'angle et du rayon
        float x = center.x + radius * Mathf.Cos(angle);
        float z = center.z + radius * Mathf.Sin(angle);
        float y = center.y + height;

        // Définir la position du soleil
        transform.position = new Vector3(x, y, z);

        // Faire en sorte que le soleil regarde toujours le centre de la scène
        transform.LookAt(center);

        // Ajuster l'intensité et la couleur de la lumière en fonction de l'angle de rotation
        float intensity = Mathf.Clamp01(Mathf.Pow(Mathf.Sin(angle), 2f)); // Utiliser une fonction non linéaire pour ajuster l'intensité
        sunLight.intensity = intensity;

        // Ajuster la couleur de la lumière pour simuler une belle journée ensoleillée
        if (intensity > 0.5f)
        {
            // Journée ensoleillée
            sunLight.color = Color.Lerp(new Color(1f, 0.95f, 0.8f), Color.white, (intensity - 0.5f) * 2f);
        }
        else
        {
            // Lever/coucher du soleil
            sunLight.color = Color.Lerp(new Color(1f, 0.5f, 0f), new Color(1f, 0.95f, 0.8f), intensity * 2f);
        }
    }
}