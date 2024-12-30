using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Nécessaire pour utiliser le Slider
/// <summary>
/// La classe <c>TipsMenu</c> gère l'affichage d'un menu de conseils dans le jeu.
/// Les conseils s'affichent un par un, avec une barre de progression indiquant le temps restant pour chaque conseil.
/// L'utilisateur peut afficher ou masquer ce menu en utilisant les boutons associés.
/// </summary>
public class TipsMenu : MonoBehaviour
{
    public string tip0;
    public bool visible;
    private List<string> lstTip;
    public TextMeshProUGUI tiptext;
    public Slider progressBar; // Le slider de la barre de progression
    public float tipDuration = 5f; // Durée entre chaque tip
    public GameObject plusButton;
    public GameObject minusButton;
    public float moveDistance = 17.5f;  // Distance à déplacer

    /// <summary>
    /// Méthode appelée au démarrage du jeu.
    /// Initialise les conseils et configure le slider et les boutons.
    /// </summary>
    void Start()
    {
        visible = true;
        lstTip = new List<string>();
        tip0 = "Tips : ";
        lstTip.Add("Vous pouvez recruter des employés et les visualiser dans le trombinoscope de leur bâtiment");
        lstTip.Add("Des crises peuvent survenir, visualisez souvent vos mails dans le bâtiment central pour vous tenir informé");
        lstTip.Add("Besoin de licencier, donner une prime ou même envoyer un employé en formation -> Bâtiment central");
        lstTip.Add("Les employés perdent de leur esprit d'équipe au fur et à mesure du temps, veillez à la cohésion !");
        lstTip.Add("Vous pouvez perdre si votre réputation est trop basse !");
        lstTip.Add("Vous pouvez perdre si vous êtes trop dans le négatif !");
        lstTip.Add("Seulement 8 semestres à tenir pour réussir votre carrière");

        // Initialiser le slider
        if (progressBar != null)
        {
            progressBar.minValue = 0;
            progressBar.maxValue = tipDuration;
            progressBar.value = 0;
        }

        // Ajouter les listeners pour les boutons
        if (minusButton != null)
        {
            minusButton.GetComponent<Button>().onClick.AddListener(MinusClick);
        }

        StartCoroutine(ChangeTipsEvery3Seconds());
    }
    /// <summary>
    /// Coroutine qui change le tip affiché toutes les 3 secondes, en mettant à jour la barre de progression.
    /// </summary>
    IEnumerator ChangeTipsEvery3Seconds()
    {
        while (visible)
        {
            for (int i = 0; i < lstTip.Count; i++)
            {
                tiptext.text = tip0 + lstTip[i]; // Mise à jour du texte du tip
                yield return StartCoroutine(UpdateProgressBar()); // Met à jour la barre de progression
            }
        }
    }
    /// <summary>
    /// Coroutine qui met à jour la barre de progression en fonction du temps écoulé pour chaque tips.
    /// </summary>
    IEnumerator UpdateProgressBar()
    {
        float elapsedTime = 0f;

        // Faire monter la barre progressivement
        while (elapsedTime < tipDuration)
        {
            elapsedTime += Time.deltaTime;
            if (progressBar != null)
            {
                progressBar.value = elapsedTime;
            }
            yield return null; // Attend le prochain frame
        }

        // Réinitialiser la barre pour le prochain tip
        if (progressBar != null)
        {
            progressBar.value = 0;
        }
    }
    /// <summary>
    /// Méthode appelée lorsque le bouton moins est cliqué. Masque le menu et déplace son objet.
    /// </summary>
    public void MinusClick()
    {
        visible = false;

        // Ajuster la position locale de l'objet
        transform.localPosition = new Vector3(transform.localPosition.x, -554f, transform.localPosition.z);
        minusButton.SetActive(false);
        plusButton.SetActive(true);
    }
    /// <summary>
    /// Méthode appelée lorsque le bouton plus est cliqué. Affiche le menu et déplace son objet.
    /// </summary>
    public void plusClick()
    {
        visible = true;

        // Ajuster la position locale de l'objet
        transform.localPosition = new Vector3(transform.localPosition.x, -490f, transform.localPosition.z);
        minusButton.SetActive(true);
        plusButton.SetActive(false);
    }


}
