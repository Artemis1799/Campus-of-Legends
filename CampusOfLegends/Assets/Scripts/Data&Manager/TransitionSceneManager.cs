using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère la transition de la scène de bilan vers la scène principale (carte).
/// Affiche les statistiques du semestre en cours, incluant l'efficacité des bâtiments,
/// l'attractivité de l'entreprise, et la quantité d'argent disponible.
/// Permet au joueur de revenir à la carte après avoir consulté le bilan.
///
/// Cette classe interagit avec ResourceManager pour récupérer les données pertinentes pour
/// le semestre en cours et PlayerPositionManager pour gérer la position du joueur lorsqu'il quitte la scène.
/// </summary>
public class TransitionSceneManager : MonoBehaviour
{
    public TextMeshProUGUI semesterText;
    public TextMeshProUGUI efficaciteBatimentInformatiqueText;
    public TextMeshProUGUI efficaciteBatimentAdministratifText;
    public TextMeshProUGUI efficaciteBatimentEnseignantText;
    public TextMeshProUGUI efficaciteBatimentEntretientText;
    public TextMeshProUGUI attractiviteText;
    public TextMeshProUGUI argentText;

    private PlayerPositionManager playerPositionManager;

    void Start()
    {
        playerPositionManager = FindObjectOfType<PlayerPositionManager>();
        playerPositionManager.lastBuildingIndex = 0;
        UpdateBilan();
    }

    private void UpdateBilan()
    {
        if (ResourceManager.Instance != null)
        {
            semesterText.text = "Bilan du semestre : " + (ResourceManager.Instance.CurrentTurn - 1);
            efficaciteBatimentInformatiqueText.text = "Efficacité Bâtiment Informatique: " + GetDeltaText(ResourceManager.Instance.deltaBatInfoEfficacity, "%") + " -> ("+ResourceManager.Instance.batInfoEfficacity.ToString("F1")+" %)";
            efficaciteBatimentAdministratifText.text = "Efficacité Bâtiment Administratif: " + GetDeltaText(ResourceManager.Instance.deltaBatAdminEfficacity, "%")+ " -> ("+ResourceManager.Instance.batAdminEfficacity.ToString("F1") + " %)";
            efficaciteBatimentEnseignantText.text = "Efficacité Bâtiment Enseignant: " + GetDeltaText(ResourceManager.Instance.deltaBatEnseiEfficacity, "%")+ " -> ("+ResourceManager.Instance.batEnseiEfficacity.ToString("F1") + " %)";
            efficaciteBatimentEntretientText.text = "Efficacité Bâtiment Entretien: " + GetDeltaText(ResourceManager.Instance.deltaBatPersoEfficacity, "%")+ " -> ("+ResourceManager.Instance.batPersoEfficacity.ToString("F1") +" %)";
            attractiviteText.text = "Attractivité: " + GetDeltaText(ResourceManager.Instance.deltaAttractiveness, "%")+ " -> ("+ResourceManager.Instance.Attractiveness.ToString("F1") +" %)";
            argentText.text = "Argent: " + GetDeltaText(ResourceManager.Instance.deltaMoney, "€") + " -> ("+ResourceManager.Instance.Money.ToString("F0")+ " €)";

            // Réinitialiser les deltas aprés l'affichage du bilan
            ResourceManager.Instance.ResetDeltas();
        }
    }

    /// <summary>
    /// Formate et colore les valeurs de delta (variation) en fonction de leur signe.
    /// </summary>
    /// <param name="delta">La variation (delta) à afficher</param>
    /// <param name="unit">L'unité associée à la variation (par exemple, "%", "€")</param>
    /// <returns>Le texte formaté avec la couleur appropriée</returns>
    private string GetDeltaText(float delta, string unit)
    {
        if (delta > 0)
        {
            return "<color=green>+" + delta.ToString("F1") + unit + "</color>";
        }
        else if (delta < 0)
        {
            return "<color=red>" + delta.ToString("F1") + unit + "</color>";
        }
        else
        {
            return "<color=#808080>" + delta.ToString("F1") + unit + "</color>";
        }
    }

    /// <summary>
    /// Gère la transition pour revenir à la scène de la carte (mapV3).
    /// Cette méthode est appelée lorsque l'utilisateur souhaite quitter le bilan et revenir à la carte.
    /// </summary>
    public void goBackToMap()
    {
        if (playerPositionManager != null)
        {
            playerPositionManager.HandlePlayerPosition();
        }
        SceneManager.LoadScene("mapV3");
    }
}
