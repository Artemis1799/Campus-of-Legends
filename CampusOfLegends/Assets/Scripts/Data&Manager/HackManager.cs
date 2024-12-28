using UnityEngine;

/// <summary>
/// Le gestionnaire des hacks dans le jeu. Il gère l'affichage des effets visuels pour les bâtiments hackés.
/// Ce script permet d'activer ou de désactiver l'affichage des hacks sur les bâtiments, en fonction de l'état de chaque bâtiment.
/// </summary>
public class HackManager : MonoBehaviour
{
    public static HackManager Instance { get; private set; }
    private GameObject[] hackObjects;
    public static SettingsManager Instance2 { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        hackObjects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            hackObjects[i] = transform.GetChild(i).gameObject;
        }

        UpdateHackDisplay();
    }

    /// <summary>
    /// Met à jour l'affichage des hacks en fonction de l'état des bâtiments (hackés ou non).
    /// </summary>
    public void UpdateHackDisplay()
    {
        if (hackObjects != null)
        {
            for (int i = 0; i < hackObjects.Length; i++)
            {
                // Vérifie si le bâtiment est hacké
                bool isClosed = CrisisManager.Instance.GetBuildingHackedState(i + 1);
                hackObjects[i].SetActive(isClosed);
                if(isClosed)
                {
                    //SettingsManager.Instance.SoundsSource.Add(hackObjects[i].GetComponent<AudioSource>());
                    //hackObjects[i].GetComponent<AudioSource>().volume = SettingsManager.Instance.SoundVolume;
                }
            }
        }
    }

    /// <summary>
    /// Désactive tous les hacks (cache les objets de hack).
    /// </summary>
    public void DisableHack()
    {
        if (hackObjects != null)
        {
            foreach (GameObject hack in hackObjects)
            {
                hack.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Active tous les hacks (affiche les objets de hack en fonction de l'état des bâtiments).
    /// </summary>
    public void EnableHack()
    {
        if (hackObjects != null)
        {
            for (int i = 0; i < hackObjects.Length; i++)
            {
                // Vérifie l'état du bâtiment et active ou désactive le hack en conséquence
                bool isClosed = CrisisManager.Instance.GetBuildingHackedState(i + 1);
                hackObjects[i].SetActive(isClosed);
            }
        }
    }
}
