using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// La classe <c>CanvasUpdater</c> est responsable de la mise à jour de l'interface utilisateur (UI) en fonction des données du jeu.
/// Elle met à jour des éléments tels que l'argent, le semestre, le nombre d'étudiants, ainsi que les sliders de popularité, d'attractivité et d'efficacité des bâtiments.
/// </summary>
public class CanvasUpdater : MonoBehaviour
{
    public static CanvasUpdater Instance { get; private set; }

    public TextMeshProUGUI moneyAmountText;
    public TextMeshProUGUI currentSemester;
    public TextMeshProUGUI numberOfStudents;
    public Slider popularitySlider;
    public Slider attractivenessSlider;
    public Slider buildingEfficacitySlider;

    /// <summary>
    /// Méthode appelée au démarrage du jeu. Elle met à jour l'UI avec les données initiales.
    /// </summary>
    private void Start()
    {
        UpdateCanvas();
    }
    /// <summary>
    /// Méthode appelée au moment de l'éveil de l'objet. Elle s'assure que l'instance de la classe <c>CanvasUpdater</c> est unique.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("GenerateEventCrisis instance already exists. Destroying duplicate.");
            Destroy(this);
        }
    }

    /// <summary>
    /// Met à jour tous les éléments de l'interface utilisateur (UI) en fonction des données actuelles du jeu.
    /// Cela inclut l'argent, le nombre d'étudiants, le semestre actuel, ainsi que les sliders de popularité, d'attractivité et d'efficacité des bâtiments.
    /// </summary>

    public void UpdateCanvas()
    {
        if (ResourceManager.Instance != null)
        {
            // Mettre à jour le texte de l'argent
            if (moneyAmountText != null)
            {
                moneyAmountText.text =  ResourceManager.Instance.Money.ToString("F2") + "€";
            }
            if (numberOfStudents != null)
            {
                numberOfStudents.text = "Nombre d'étudiants : " + ResourceManager.Instance.getNumberOfStudents();
            }
            // Mettre à jour le texte du semestre
            if (currentSemester != null)
            {
                currentSemester.text = "Semestre : " + ResourceManager.Instance.CurrentTurn.ToString();
            }

            // Mettre à jour le slider de popularité
            if (popularitySlider != null)
            {
                popularitySlider.value = ResourceManager.Instance.Popularity;
            }

            // Mettre à jour le slider d'attractivité
            if (attractivenessSlider != null)
            {
                attractivenessSlider.value = ResourceManager.Instance.Attractiveness;
            }

            // Mettre à jour le slider en fonction du tag de la scène
            string sceneTag = gameObject.scene.name; // Utilisez le nom de la scène ou un autre identifiant unique
            Debug.Log("Scene tag: " + sceneTag);
            switch (sceneTag)
            {
                case "Bat_Administration":
                    if (buildingEfficacitySlider != null)
                    {
                        buildingEfficacitySlider.value = ResourceManager.Instance.batAdminEfficacity;
                    }
                    break;
                case "Bat_Enseignants":
                    if (buildingEfficacitySlider != null)
                    {
                        buildingEfficacitySlider.value = ResourceManager.Instance.batEnseiEfficacity;
                    }
                    break;
                case "Bat_Entretien":
                    if (buildingEfficacitySlider != null)
                    {
                        buildingEfficacitySlider.value = ResourceManager.Instance.batPersoEfficacity;
                    }
                    break;
                case "Bat_Informatique":
                    if (buildingEfficacitySlider != null)
                    {
                        buildingEfficacitySlider.value = ResourceManager.Instance.batInfoEfficacity;
                    }
                    break;
                default:
                    Debug.LogWarning("Scene tag not recognized: " + sceneTag);
                    break;
            }
        }
    }
}
