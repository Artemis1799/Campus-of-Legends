using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Cette classe gère la position du joueur dans le jeu. Elle détermine où le joueur apparaît
/// lorsqu'il change de scène en fonction du dernier bâtiment visité. Elle fournit également des méthodes
/// pour activer et désactiver le joueur et pour gérer les points d'entrée associés aux différents bâtiments.
/// </summary>
public class PlayerPositionManager : MonoBehaviour
{
    public int lastBuildingIndex = 0;
    public Transform player;

    // Points d'entrée
    public Transform posBase;
    public Transform entryInfo;
    public Transform entryEns;
    public Transform entryAdmin;
    public Transform entryEntr;
    public Transform entryAccueil;
    

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        HandlePlayerPosition();
    }
    /// <summary>
    /// Inscrit les méthodes qui s'exécutent lorsque la scène est chargée.
    /// </summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    /// <summary>
    /// Désinscrit les méthodes lorsqu'elles ne sont plus nécessaires.
    /// </summary>
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    /// <summary>
    /// Cette méthode est appelée après chaque chargement de scène. Elle vérifie
    /// la scène courante et prend les actions nécessaires (désactiver ou activer le joueur, etc.).
    /// </summary>
    /// <param name="scene">La scène qui a été chargée.</param>
    /// <param name="mode">Le mode de chargement de la scène.</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "transitionSemester")
        {
            DisablePlayer();
            FireManager.Instance.EnableFires();

        }
        else if (scene.name == "mapV3")
        {
            HandlePlayerPosition();
            FireManager.Instance.EnableFires();
        }
    }
    /// <summary>
    /// Gère la position du joueur en fonction du dernier bâtiment indexé.
    /// </summary>
    public void HandlePlayerPosition()
    {
        if (lastBuildingIndex != -1)
        {
            Transform entryPoint = GetEntryPoint(lastBuildingIndex);
            if (entryPoint != null && player != null)
            {
                player.position = entryPoint.position;
                player.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Définit l'index du dernier bâtiment dans lequel le joueur s'est trouvé.
    /// </summary>
    /// <param name="index">L'index du bâtiment.</param>
    public void SetLastBuildingIndex(int index)
    {
        lastBuildingIndex = index;
    }


    /// <summary>
    /// Retourne le point d'entrée associé à un bâtiment donné.
    /// </summary>
    /// <param name="buildingIndex">L'index du bâtiment.</param>
    /// <returns>Le transform du point d'entrée du bâtiment spécifié.</returns>
    public Transform GetEntryPoint(int buildingIndex)
    {
        switch (buildingIndex)
        {
            case 0: return posBase;
            case 1: return entryInfo;
            case 2: return entryEns;
            case 3: return entryAdmin;
            case 4: return entryEntr;
            case 5: return entryAccueil;
            default:
                return null;
        }
    }


    /// <summary>
    /// Désactive le joueur (réinitialise la position et l'état du joueur).
    /// </summary>
    public void DisablePlayer()
    {
        if (player != null)
        {
            player.GetComponent<PlayerController>().ResetTargetPosition();
            player.gameObject.SetActive(false);
            FireManager.Instance.DisableFires();
        }
    }

    /// <summary>
    /// Réinitialise la position du joueur au point d'entrée de base.
    /// </summary>
    public void ResetPosition()
    {
        SetLastBuildingIndex(0);

    }
}
