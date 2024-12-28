using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Cette classe gère l'interaction avec les bâtiments dans le jeu, y compris la mise en surbrillance des bâtiments
/// lorsque le joueur passe la souris dessus, le changement de couleur permanent lorsqu'un bâtiment est cliqué, et
/// l'affichage d'un tooltip avec le nom du bâtiment. La classe permet également de personnaliser le curseur pendant
/// ces interactions.
/// </summary>
public class HighlightBuilding : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask buildingLayer;
    public Color highlightColor = new Color(255f / 255f, 238f / 255f, 172f / 255f);
    public Color closedHighlightColor = Color.red;
    public float highlightOpacity = 0.5f;
    public GameObject tooltipPrefab;
    public string buildingName;

    private Renderer currentBuildingRenderer;
    private Color originalColor;
    private float originalOpacity;
    private GameObject tooltipInstance;
    private Transform currentBuilding;
    private Transform clickedBuilding;

    private Vector2 hotSpot = Vector2.zero;  // Point d'origine du curseur
    public Texture2D cursorTextureClick;  // Curseur personnalisé clique
    public Texture2D cursorTextureNormal;  // Curseur personnalisé normal
    public static CustomCursor Instance { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main;
    }


    /// <summary>
    /// Mise à jour de la logique de survol et de clic des bâtiments.
    /// Cette méthode gère le changement de couleur lors du survol des bâtiments et le changement permanent de couleur lors du clic.
    /// Elle permet également de personnaliser le curseur en fonction de l'état (normal ou clique).
    /// </summary>
    void Update()
    {
        // Lancer un raycast depuis la souris
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Si le ray touche quelque chose dans le Layer des b�timents
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingLayer))
        {
            Transform building = hit.transform;

            // Si la souris survole un nouveau b�timent
            if (building != currentBuilding)
            {
                // R�initialiser l'ancien b�timent
                if (currentBuildingRenderer != null)
                {
                    ResetBuildingColor();
                    CustomCursor.Instance.SetCursor(null);
                    Cursor.SetCursor(cursorTextureNormal, hotSpot, CursorMode.Auto);
                    //HideTooltip();
                }

                // Stocker le nouveau b�timent
                currentBuilding = building;
                currentBuildingRenderer = building.GetComponent<Renderer>();

                // V�rifier si le b�timent a d�j� �t� cliqu�
                if (currentBuildingRenderer != null && currentBuilding != clickedBuilding)
                {
                    originalColor = currentBuildingRenderer.material.color;
                    originalOpacity = originalColor.a; // Sauvegarder l'opacit� d'origine

                    Color highlightWithOpacity = highlightColor;
                    highlightWithOpacity.a = highlightOpacity; // Appliquer l'opacit� � la couleur de surbrillance
                    currentBuildingRenderer.material.color = highlightWithOpacity;
                }

                // Afficher le nom du b�timent
                //ShowTooltip(building);
                if(!PauseMenu.isPaused)CustomCursor.Instance.SetCursor(cursorTextureClick);
            }

            // V�rifier si le joueur a cliqu� sur le b�timent
            if (Input.GetMouseButtonDown(0))
            {
                HandleBuildingClick(building);
            }
        }
        else
        {
            // Si la souris ne survole plus un b�timent
            if (currentBuildingRenderer != null)
            {
                ResetBuildingColor();
                currentBuildingRenderer = null;
                CustomCursor.Instance.SetCursor(null);
                Cursor.SetCursor(cursorTextureNormal, hotSpot, CursorMode.Auto);

                //HideTooltip();
            }

            currentBuilding = null;

            // V�rifier si le joueur a cliqu� ailleurs
            if (Input.GetMouseButtonDown(0))
            {
                ResetAllBuildingsColor();
            }
        }
    }

    /// <summary>
    /// Gère le clic sur un bâtiment en changeant sa couleur de manière permanente.
    /// Cette méthode change la couleur du bâtiment cliqué en une couleur spécifique (highlightColor).
    /// </summary>
    /// <param name="building">Le bâtiment qui a été cliqué.</param>
    private void HandleBuildingClick(Transform building)
    {
        if (currentBuildingRenderer != null)
        {
            clickedBuilding = building;
            currentBuildingRenderer.material.color = highlightColor; // Changer la couleur du b�timent de mani�re permanente
        }
    }

    /// <summary>
    /// Restaure la couleur originale du bâtiment, y compris son opacité.
    /// Cette méthode est utilisée pour réinitialiser la couleur du bâtiment après le survol.
    /// </summary>
    private void ResetBuildingColor()
    {
        if (currentBuildingRenderer != null)
        {
            if (currentBuilding != clickedBuilding)
            {
                Color resetColor = originalColor;
                resetColor.a = originalOpacity; // Restaurer l'opacit� d'origine
                currentBuildingRenderer.material.color = resetColor;
            }
        }
    }

    /// <summary>
    /// Restaure la couleur de tous les bâtiments précédemment cliqués.
    /// Cette méthode est utilisée lorsque le joueur clique à l'extérieur d'un bâtiment pour réinitialiser tous les bâtiments.
    /// </summary>
    private void ResetAllBuildingsColor()
    {
        if (clickedBuilding != null)
        {
            Renderer clickedBuildingRenderer = clickedBuilding.GetComponent<Renderer>();
            if (clickedBuildingRenderer != null)
            {
                clickedBuildingRenderer.material.color = originalColor;
                clickedBuilding = null;
            }
        }
    }

    /// <summary>
    /// Affiche un tooltip avec le nom du bâtiment.
    /// Cette méthode instancie un prefab de tooltip à proximité du bâtiment et affiche son nom.
    /// </summary>
    /// <param name="building">Le bâtiment pour lequel le tooltip sera affiché.</param>
    private void ShowTooltip(Transform building)
    {
        if (tooltipPrefab != null && tooltipInstance == null)
        {
            // Cr�e une instance du tooltip � c�t� du b�timent
            tooltipInstance = Instantiate(tooltipPrefab, building.position + new Vector3(0, 2, 0), Quaternion.identity);
            Text tooltipText = tooltipInstance.GetComponentInChildren<Text>();
            if (tooltipText != null)
            {
                tooltipText.text = buildingName; // Affiche le nom du b�timent
            }
        }
    }

    /// <summary>
    /// Masque le tooltip, s'il existe.
    /// Cette méthode est utilisée pour détruire le tooltip une fois qu'il n'est plus nécessaire.
    /// </summary>
    private void HideTooltip()
    {
        if (tooltipInstance != null)
        {
            Destroy(tooltipInstance);
            tooltipInstance = null;
        }
    }
    /// <summary>
    /// Change la couleur du bâtiment en rouge pour indiquer qu'il est fermé.
    /// Cette méthode est utilisée pour marquer un bâtiment comme fermé en lui attribuant une couleur rouge.
    /// </summary>
    public void HighlightInRed()
    {
        if (currentBuildingRenderer != null)
        {
            currentBuildingRenderer.material.color = closedHighlightColor;
        }
    }

}
