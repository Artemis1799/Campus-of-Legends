using UnityEngine;


/// <summary>
/// Cette classe gère les points d'entrée pour différents bâtiments dans le jeu.
/// Elle permet de récupérer le point d'entrée spécifique en fonction d'un indice de bâtiment.
/// Les points d'entrée sont représentés par des objets de type Transform, qui sont assignés dans l'éditeur Unity.
/// </summary>
public class EntrancePoints : MonoBehaviour
{
    public Transform entryInfo;
    public Transform entryEns;
    public Transform entryAdmin;
    public Transform entryEntr;

    /// <summary>
    /// Cette méthode retourne le point d'entrée correspondant à l'indice du bâtiment passé en paramètre.
    /// </summary>
    /// <param name="buildingIndex">L'indice du bâtiment pour lequel on veut récupérer le point d'entrée.
    /// L'indice doit être 1, 2, 3 ou 4, correspondant à Informatique, Enseignants, Administratif et Entretien respectivement.</param>
    /// <returns>Retourne le Transform du point d'entrée correspondant ou null si l'indice est invalide.</returns>
    public Transform GetEntryPoint(int buildingIndex)
    {
        Debug.Log("[EntrancePoints] GetEntryPoint called with index: " + buildingIndex);
        switch (buildingIndex)
        {
            case 1: return entryInfo;
            case 2: return entryEns;
            case 3: return entryAdmin;
            case 4: return entryEntr;
            default:
                Debug.LogError("[EntrancePoints] Invalid building index: " + buildingIndex);
                return null;
        }
    }

}