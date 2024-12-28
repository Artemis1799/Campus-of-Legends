using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Ce script permet de charger une scène en fonction du bâtiment sélectionné via l'interface utilisateur.
/// Lorsque le joueur clique sur un bouton associé à un bâtiment, ce script détermine la scène correspondante à charger.
/// </summary>
public class LoadScenFromUi : MonoBehaviour
{
    public string sceneName;
    public int indexBat;


    /// <summary>
    /// Méthode appelée lorsque l'utilisateur clique sur un bouton pour changer de scène.
    /// </summary>
    public void ButtonClicked()
    {
        sceneName=TrombinoParam.caller;
        switch(sceneName)
        {
            case "Administratif":
                sceneName="Bat_Administration";
                break;
            case "Enseignant":
                sceneName="Bat_Enseignants";
                break;
            case "Entretient":
                sceneName="Bat_Entretien";
                break;
            case "Informatique":
                sceneName="Bat_Informatique";
                break;                
        }
        
        SceneManager.LoadScene(sceneName);
    }
}
