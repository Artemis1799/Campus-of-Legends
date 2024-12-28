using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// La classe <c>OpenFormCrises</c> gère l'ouverture et la fermeture du formulaire de crise dans l'interface utilisateur.
/// Elle est attachée à un bouton qui, lorsqu'il est cliqué, met à jour et affiche un formulaire de crise contenant des informations spécifiques sur la crise en cours.
/// </summary>
public class OpenFormCrises : MonoBehaviour
{
    public Button mainButton;
    public CrisisForm crisisForm;
    public string CrisisName;
    public string CrisisText;
    public bool ActivedButton;
    public int buildingIndex;
    public bool isOnFire;

    /// <summary>
    /// Méthode appelée au démarrage pour initialiser le bouton avec un listener qui ouvre le formulaire de crise lorsque le bouton est cliqué.
    /// </summary>
    void Start()
    {

        if (mainButton != null)
        {
            mainButton.onClick.AddListener(() =>
            {
               crisisForm.UpdateForm(CrisisName, CrisisText, ActivedButton, buildingIndex, isOnFire);
                OnMainButtonClick();
            });
        }
        else
        {
            Debug.LogError("ButtonOpenForm: Missing references - MainButton not assigned.");
        }
    }

    /// <summary>
    /// Méthode permettant de configurer les données de la crise (nom, texte, état du bouton, etc.) avant l'affichage.
    /// </summary>
    /// <param name="CrisisName">Nom de la crise à afficher.</param>
    /// <param name="CrisesText">Description de la crise à afficher.</param>
    /// <param name="ActivedBool">Indique si le bouton de la crise doit être activé ou non.</param>
    /// <param name="cfm">Référence au formulaire de crise.</param>
    /// <param name="buildingIndex">Index du bâtiment concerné par la crise.</param>
    /// <param name="isOnFire">Indique si la crise concerne un incendie.</param>
    public void setData(string CrisisName, string CrisesText, bool ActivedBool, CrisisForm cfm, int buildingIndex, bool isOnFire)
    {
        this.CrisisName = CrisisName;
        this.CrisisText = CrisesText;
        this.ActivedButton = ActivedBool;
        this.crisisForm = cfm;
        this.buildingIndex = buildingIndex;
        this.isOnFire = isOnFire;
    }
    /// <summary>
    /// Méthode appelée lorsque le bouton principal est cliqué. Elle affiche le formulaire de crise.
    /// </summary>
    void OnMainButtonClick()
    {
        if (crisisForm != null)
        {
            crisisForm.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// Méthode pour fermer le formulaire de crise.
    /// </summary>
    public void CloseForm()
    {
        if (crisisForm != null)
        {
            crisisForm.gameObject.SetActive(false);
        }
    }

}
