using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La classe FPSViewer permet de calculer et afficher les images par seconde (FPS) dans la console pour suivre les performances du jeu.
/// Elle lisse la mesure des FPS à l'aide du deltaTime et l'affiche une fois par seconde.
/// </summary>
public class FPSViewer : MonoBehaviour
{
    private float deltaTime = 0.0f;

    /// <summary>
    /// Cette fonction est appelée à chaque frame du jeu.
    /// Elle calcule et affiche les FPS moyennes toutes les secondes.
    /// </summary>
    void Update()
    {
        // Calculer le deltaTime pour lisser les FPS
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        // Afficher les FPS toutes les secondes
        if (Time.frameCount % 60 == 0) // Ajustez selon la fréquence désirée
        {
            float fps = 1.0f / deltaTime;
            Debug.Log($"FPS: {fps:0.}");
        }
    }
}
