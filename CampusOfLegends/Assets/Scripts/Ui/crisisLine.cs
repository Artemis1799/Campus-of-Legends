using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// La classe <c>crisisLine</c> représente une ligne d'une crise dans l'interface utilisateur.
/// Elle contient des informations sur la crise et permet d'afficher les détails de la crise via un bouton.
/// </summary>
public class crisisLine : MonoBehaviour
{
    public string CrisisName;
    public string CrisisText;
    public bool ActivedButton;
    public int buildingIndex;
    public Button mainButton;
    public CrisisForm form;
    public bool isOnFire;

    /// <summary>
    /// Méthode appelée au démarrage pour configurer le bouton.
    /// Elle ajoute un listener au bouton qui met à jour le formulaire de crise avec les détails de la crise.
    /// </summary>
    void Start()
    {  
        if (mainButton != null)
        {
            mainButton.onClick.AddListener(() =>
            {
               form.UpdateForm(CrisisName, CrisisText, ActivedButton, buildingIndex, isOnFire);
            });
        }
        else
        {
            Debug.LogError("ButtonOpenForm: Missing references - MainButton not assigned.");
        }
    }

    /// <summary>
    /// Méthode pour définir les données de la crise.
    /// </summary>
    /// <param name="CrisisName">Le nom de la crise.</param>
    /// <param name="CrisesText">Le texte expliquant la crise.</param>
    /// <param name="ActivedBool">Indique si le bouton pour résoudre la crise doit être activé.</param>
    /// <param name="buildingIndex">L'indice du bâtiment concerné par la crise.</param>
    /// <param name="isOnFire">Indique si la crise est liée à un incendie.</param>
    public void setData(string CrisisName, string CrisesText, bool ActivedBool, int buildingIndex, bool isOnFire)
    {
        this.CrisisName = CrisisName;
        this.CrisisText = CrisesText;
        this.ActivedButton = ActivedBool;
        this.buildingIndex = buildingIndex;
        this.isOnFire = isOnFire;
    }

}
