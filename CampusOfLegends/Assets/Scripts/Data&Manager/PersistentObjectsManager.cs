using UnityEngine;

/// <summary>
/// Met des objets en Dont destroy on load
/// </summary>
public class PersistentObjectsManager : MonoBehaviour
{
    public static PersistentObjectsManager Instance { get; private set; }

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
}
