using UnityEngine;

/// <summary>
/// Le gestionnaire des incendies dans le jeu. Il s'assure que les objets de feu sont affichés ou masqués en fonction de l'état des bâtiments (incendie actif ou non).
/// Ce script permet de gérer l'affichage des effets visuels pour les bâtiments en feu, en activant ou désactivant les objets de feu associés.
/// </summary>
public class FireManager : MonoBehaviour
{
    public static FireManager Instance { get; private set; }
    private GameObject[] fireObjects; // Tableau pour stocker les objets de feu
    public static SettingsManager Instance2 { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ne pas détruire cet objet lors du changement de sc�ne
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // R�cup�rer les enfants de l'objet "Feu batiments"
        fireObjects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            fireObjects[i] = transform.GetChild(i).gameObject;
        }

        // Mettre à jour l'affichage des feux en fonction de l'�tat des b�timents
        UpdateFireDisplay();
    }

    /// <summary>
    /// Met à jour l'affichage des feux en fonction de l'état des bâtiments (feu actif ou non).
    /// </summary>
    public void UpdateFireDisplay()
    {
        if (fireObjects != null)
        {
            for (int i = 0; i < fireObjects.Length; i++)
            {
                bool isClosed = CrisisManager.Instance.GetBuildingState(i + 1);
                fireObjects[i].SetActive(isClosed);
                if(isClosed)
                {
                    //SettingsManager.Instance.SoundsSource.Add(fireObjects[i].GetComponent<AudioSource>());
                    //fireObjects[i].GetComponent<AudioSource>().volume = SettingsManager.Instance.SoundVolume;
                }
            }
        }
    }

    /// <summary>
    /// Désactive tous les feux (cache les objets de feu).
    /// </summary>
    public void DisableFires()
    {
        if (fireObjects != null)
        {
            foreach (GameObject fire in fireObjects)
            {
                fire.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Active tous les feux (affiche les objets de feu en fonction de l'état des bâtiments).
    /// </summary>
    public void EnableFires()
    {
        if (fireObjects != null)
        {
            for (int i = 0; i < fireObjects.Length; i++)
            {
                bool isClosed = CrisisManager.Instance.GetBuildingState(i + 1);
                fireObjects[i].SetActive(isClosed);
            }
        }
    }
}
