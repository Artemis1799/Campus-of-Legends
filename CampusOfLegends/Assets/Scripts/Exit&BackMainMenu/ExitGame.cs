using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    /// <summary>
    /// Méthode appelée lors du clic pour quitter le jeu.
    /// Cette méthode utilise <see cref="Application.Quit"/> pour fermer l'application.
    /// Note : Cette action fonctionne uniquement dans une build standalone 
    /// et ne fonctionne pas dans l'éditeur Unity.
    /// </summary>
    public void OnClickExit()
    {
        Application.Quit();
    }

}


