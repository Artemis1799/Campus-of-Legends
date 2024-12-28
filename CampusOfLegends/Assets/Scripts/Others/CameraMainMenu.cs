using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cette classe gère le mouvement de la caméra autour d'une cible dans le menu principal.
/// La caméra effectue une rotation circulaire autour de la cible à une vitesse constante.
/// </summary>
public class CameraMainMenu : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 0.1f;
    public float radius = 25f;
    public float height = 25f;
    public float initialAngleOffset = Mathf.PI / 2; // Ajout d'un décalage initial de 90 degrés (PI/2 radians)

    /// <summary>
    /// Mise à jour de la position de la caméra à chaque frame.
    /// Cette méthode fait tourner la caméra autour de la cible selon un mouvement circulaire.
    /// </summary>
    private void Update()
    {
        if (target != null)
        {
            // Calculer l'angle de rotation en fonction du temps écoulé
            float angle = Time.time * rotationSpeed + initialAngleOffset * 3;

            // Calculer la position de la caméra en fonction de l'angle et du rayon
            float x = target.position.x + radius * Mathf.Cos(angle);
            float z = target.position.z + radius * Mathf.Sin(angle);
            float y = target.position.y + height;

            // Définir la position de la caméra
            transform.position = new Vector3(x, y, z);

            // Faire en sorte que la caméra regarde toujours la cible
            transform.LookAt(target);
        }
    }
}
