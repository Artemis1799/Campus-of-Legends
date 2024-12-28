using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Contrôleur de caméra qui suit une cible donnée avec un mouvement fluide.
/// Gère un décalage personnalisé et utilise l'interpolation pour des transitions douces.
/// </summary>
public class CameraController : MonoBehaviour
{
    public Transform target;  // Cible que la caméra suit
    public float smoothSpeed = 0.125f;  // Vitesse d'interpolation pour le suivi
    public Vector3 offset;  // Décalage de la caméra par rapport à la cible

    private void Awake()
    {
        // Calcul de l'offset basé sur la position initiale de la caméra et de la cible
        if (target != null)
        {
            offset = transform.position - target.position;
        }
                
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (target == null)
        {
            return;  // On quitte la méthode si la cible n'est pas définie
        }

        // Position désirée de la caméra en fonction de la cible et de l'offset
        Vector3 desiredPosition = target.position + offset;

        // Interpolation de la position de la caméra pour un mouvement plus fluide
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Mise à jour de la position de la caméra
        transform.position = smoothedPosition;
    }
}
