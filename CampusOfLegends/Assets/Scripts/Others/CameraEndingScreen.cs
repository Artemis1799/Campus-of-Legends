using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cette classe gère le comportement de la caméra lors de la séquence de fin de jeu.
/// La caméra tourne autour d'une cible donnée et finit par s'arrêter à une position finale après un certain temps.
/// </summary>
public class CameraEndingScreen : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 0.1f;
    public float radius = 25f;
    public float height = 25f;
    public float initialAngleOffset = Mathf.PI / 2; // Ajout d'un décalage initial de 90 degrés (PI/2 radians)
    public float FinalTargetPositionZ;
    private bool isArrived = false;
    private Vector3 finalCameraPosition;
    private Quaternion finalCameraRotation;

    /// <summary>
    /// Met à jour la position et la rotation de la caméra à chaque frame.
    /// Si la caméra n'est pas arrivée, elle tourne autour de la cible.
    /// Sinon, elle se fixe sur la position finale.
    /// </summary>
    private void Update()
    {
        if (!isArrived)
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
        else
        {
            Vector3 lookAtPosition = target.position + new Vector3(0f, 2f, 0f);
            transform.LookAt(lookAtPosition);
        }
    }

    /// <summary>
    /// Méthode appelée pour arrêter la caméra à une position finale spécifique.
    /// Elle met la caméra en position statique et fixe sa rotation pour regarder la cible.
    /// </summary>
    public void SetIsArrived()
    {
        // Positionner la caméra à une position finale définie
        finalCameraPosition = new Vector3(-4.956f, 2.383f,FinalTargetPositionZ);
        transform.position = finalCameraPosition;
        // Fixer la direction de la caméra vers une position statique (avec hauteur)
        Vector3 lookAtPosition = target.position + new Vector3(0f, 2f, 0f);
        transform.LookAt(lookAtPosition);

        // Indiquer que la caméra est arrivée
        isArrived = true;
    }
}
