using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// La classe <c>Minimap</c> est responsable de l'affichage de la mini-carte dans le jeu. Elle met à jour la position du joueur sur la mini-carte en fonction de la position du joueur dans le monde,
/// et calcule le ratio entre les distances du monde réel et celles de la mini-carte pour une mise à l'échelle correcte.
/// </summary>
public class Minimap : MonoBehaviour
{
	[Header("References")]
	public RectTransform minimapPoint_1;
	public RectTransform minimapPoint_2;
	public Transform worldPoint_1;
	public Transform worldPoint_2;
	public GameObject minimap;

	[Header("Player")]
	public RectTransform playerMinimap;
	public Transform playerWorld;

	private float minimapRatio;



	private void Awake()
	{
		CalculateMapRatio();
	}

    /// <summary>
    /// Méthode appelée chaque frame pour mettre à jour la position du joueur sur la mini-carte
    /// et activer ou désactiver l'affichage de la mini-carte en fonction de la scène active.
    /// </summary>
    private void Update()
	{
		if (SceneManager.GetActiveScene().name == "mapV3")
		{
			minimap.gameObject.SetActive(true);
			playerMinimap.anchoredPosition = minimapPoint_1.anchoredPosition + new Vector2((playerWorld.position.x - worldPoint_1.position.x) * minimapRatio,(playerWorld.position.z - worldPoint_1.position.z) * minimapRatio);
		}
		else
		{
			minimap.gameObject.SetActive(false);
		}
	}

    /// <summary>
    /// Calcule le ratio entre la distance dans le monde réel et la distance sur la mini-carte.
    /// Ce ratio est utilisé pour ajuster la position du joueur sur la mini-carte.
    /// </summary>
    public void CalculateMapRatio()
	{
		//distance world ignoring Y axis
		Vector3 distanceWorldVector = worldPoint_1.position - worldPoint_2.position;
		distanceWorldVector.y = 0f;
		float distanceWorld = distanceWorldVector.magnitude;


		//distance minimap
		float distanceMinimap = Mathf.Sqrt(
								Mathf.Pow((minimapPoint_1.anchoredPosition.x - minimapPoint_2.anchoredPosition.x), 2) +
								Mathf.Pow((minimapPoint_1.anchoredPosition.y - minimapPoint_2.anchoredPosition.y), 2));


		minimapRatio = distanceMinimap / distanceWorld;
	}
}