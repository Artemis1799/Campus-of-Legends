using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// La classe <c>CrisisForm</c> gère l'affichage et la gestion des informations d'une crise dans le jeu.
/// Elle affiche un objet de crise, un message et permet de résoudre la crise en cliquant sur un bouton.
/// </summary>
public class CrisisForm : MonoBehaviour
{
    public TextMeshProUGUI ObjectText; 
    public TextMeshProUGUI MessageText;
    public Button theButton;
    public static CrisisManager Instance { get; private set; }
    public static ResourceManager Instance2 { get; private set; }
    public static GenerateEventCrisis Instance3 { get; private set; }

    /// <summary>
    /// Met à jour le formulaire de crise avec les informations spécifiées.
    /// Elle active ou désactive le bouton en fonction de l'état de la crise (si elle peut être résolue).
    /// </summary>
    /// <param name="Object">L'objet affecté par la crise (par exemple, le bâtiment concerné).</param>
    /// <param name="Message">Le message expliquant la crise ou l'incident.</param>
    /// <param name="isActivated">Si <c>true</c>, le bouton pour résoudre la crise sera activé.</param>
    /// <param name="buildingIndex">L'indice du bâtiment concerné par la crise.</param>
    /// <param name="isOnFire">Indique si le bâtiment est en feu (utilisé pour déterminer l'action à entreprendre).</param>
    public void UpdateForm(string Object, string Message, bool isActivated, int buildingIndex, bool isOnFire)
    {
        ObjectText.text=Object;
        MessageText.text=Message;
        theButton.gameObject.SetActive(isActivated);
        if (theButton != null && isActivated == true)
        {
            if(isOnFire){
                theButton.onClick.AddListener(() =>
                {
                    ResourceManager.Instance.AddMoney(-15000f);
                    CrisisManager.Instance.MarkBuildingForResolution(buildingIndex, false);
                    GenerateEventCrisis.Instance.RefreshCanvas();
                    CanvasUpdater.Instance.UpdateCanvas();
                });
            }
            else
            {
                theButton.onClick.AddListener(() =>
                {
                    ResourceManager.Instance.AddMoney(-10000f);
                    CrisisManager.Instance.MarkHackForResolution(buildingIndex, false);
                    GenerateEventCrisis.Instance.RefreshCanvas();
                    CanvasUpdater.Instance.UpdateCanvas();
                });
            }
        }
    }
}
