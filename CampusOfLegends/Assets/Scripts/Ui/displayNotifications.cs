using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// La classe <c>displayNotifications</c> est responsable de l'affichage des notifications liées aux crises et aux conflits
/// dans l'interface utilisateur, en affichant le nombre d'événements et de crises actuels à l'utilisateur.
/// </summary>
public class displayNotifications : MonoBehaviour
{
    public GameObject notificationsLabelGlobal;
    public GameObject notificationsLabelCrisis;
    public GameObject notificationsLabelConflicts;

    public static CrisisManager Instance { get; private set; }
    /// <summary>
    /// Méthode appelée au démarrage pour initialiser et afficher les notifications.
    /// </summary>
    void Start()
    {
        if(notificationsLabelGlobal!=null){
            TextMeshProUGUI[] tmpText = notificationsLabelGlobal.GetComponentsInChildren<TextMeshProUGUI>();
            int number = CrisisManager.Instance.getNumberOfGlobalCrisis();
            Debug.Log("Nombre de crises globales = "+number);
            if(number==0){
                notificationsLabelGlobal.gameObject.SetActive(false);
                }
            else{
                notificationsLabelGlobal.gameObject.SetActive(true);
                tmpText[0].text = number.ToString();
            }
        }
        if(notificationsLabelCrisis!=null){
            TextMeshProUGUI[] tmpText = notificationsLabelCrisis.GetComponentsInChildren<TextMeshProUGUI>();
            int number = CrisisManager.Instance.getNumberOfEventCrisis();
            if(number==0){
                notificationsLabelCrisis.gameObject.SetActive(false);
                }
            else{
                notificationsLabelCrisis.gameObject.SetActive(true);
                tmpText[0].text = number.ToString();
            }
        }
        if(notificationsLabelConflicts!=null){
            TextMeshProUGUI[] tmpText = notificationsLabelConflicts.GetComponentsInChildren<TextMeshProUGUI>();
            int number = CrisisManager.Instance.getNumberOfEmpConflicts();
            Debug.Log("Nombre de conflits = "+number);
            if(number==0){
                notificationsLabelConflicts.gameObject.SetActive(false);
            }
            else{
                notificationsLabelConflicts.gameObject.SetActive(true);
                tmpText[0].text = number.ToString();
            }
        }
    }
}
