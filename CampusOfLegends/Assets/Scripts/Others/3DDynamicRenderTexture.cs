using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // Ajoutez cette ligne pour utiliser List<>

/// <summary>
/// Cette classe permet d'afficher dynamiquement des modèles 3D dans une interface utilisateur en utilisant des
/// caméras et des RenderTextures. Chaque modèle est associé à une caméra qui le rend dans une texture, 
/// laquelle est ensuite affichée dans un composant RawImage.
/// </summary>
public class Dynamic3DSpriteDisplay : MonoBehaviour
{
    public RawImage DisplayImagePrefab; // Préfab de la RawImage à utiliser
    public Transform ModelContainer; // Conteneur des modèles pour le placement

    private List<GameObject> models; // Liste des modèles à afficher
    private List<Camera> cameras; // Liste des caméras associées
    private List<RenderTexture> renderTextures; // Liste des RenderTextures associées


    /// <summary>
    /// Méthode appelée au démarrage. Initialise les listes pour les modèles, caméras et RenderTextures.
    /// </summary>
    void Start()
    {
        models = new List<GameObject>();
        cameras = new List<Camera>();
        renderTextures = new List<RenderTexture>();
    }


    /// <summary>
    /// Affiche un modèle 3D dans l'interface utilisateur en créant un modèle, une caméra et une RawImage.
    /// Chaque modèle est instancié dans le conteneur, une caméra est créée pour chaque modèle et l'affichage
    /// du modèle est rendu dans une RenderTexture, qui est ensuite assignée à une RawImage pour l'affichage.
    /// </summary>
    /// <param name="modelPrefab">Le préfabriqué du modèle 3D à afficher.</param>
    /// <returns>La RenderTexture utilisée pour afficher le modèle.</returns>
    public RenderTexture ShowModel(GameObject modelPrefab)
    {
 
        GameObject newModel = Instantiate(modelPrefab, ModelContainer);
        newModel.transform.localScale = new Vector3(86.4f, 86.4f, 86.4f);
        //newModel.transform.localPosition = new Vector3(0, 0, 0); // Décaler de 100 unités sur l'axe x
        newModel.transform.localPosition = new Vector3(220 * models.Count, 0, 0); // Décaler de 100 unités sur l'axe x
        
        newModel.transform.localRotation = Quaternion.Euler(0f, -177f, 0f);
        // Instancier le modèle et le placer dans le conteneur
        newModel.AddComponent<rotateSprite>(); 
        SetLayerRecursively(newModel, "sprites");

        // Créer une nouvelle caméra pour le modèle
        GameObject cameraObj = new GameObject("ModelCamera_" + models.Count);
        Camera newCamera = cameraObj.AddComponent<Camera>();
        newCamera.transform.SetParent(ModelContainer);
        newCamera.transform.localPosition = new Vector3(220 * models.Count, 73, -155);
        newCamera.clearFlags = CameraClearFlags.SolidColor;
        newCamera.backgroundColor = new Color(1f, 1f, 1f, 0.24f); // Blanc avec 61 de transparence (0.24 en alpha)

        // Créer une nouvelle RenderTexture
        RenderTexture newRenderTexture = new RenderTexture(1024, 1024, 24);
        newRenderTexture.name = "ModelRenderTexture_" + models.Count;
        
        newCamera.targetTexture = newRenderTexture;

        // Instancier une RawImage et l'assigner à la RenderTexture
        RawImage newDisplayImage = Instantiate(DisplayImagePrefab, ModelContainer);
        newDisplayImage.texture = newRenderTexture;
        //newDisplayImage.transform.localPosition = new Vector3(200 * models.Count, 0, 0); // Décaler de 100 unités sur l'axe x

        // Ajouter les nouveaux objets aux listes
        models.Add(newModel);
        cameras.Add(newCamera);
        renderTextures.Add(newRenderTexture);
        return newRenderTexture;
    }

    /// <summary>
    /// Modifie le layer d'un objet et de tous ses enfants récursivement.
    /// </summary>
    /// <param name="obj">L'objet dont le layer doit être changé.</param>
    /// <param name="layerName">Le nom du layer à appliquer.</param>
    public void SetLayerRecursively(GameObject obj, string layerName)
    {
        // Change le layer de l'objet actuel
        obj.layer = LayerMask.NameToLayer(layerName);

        // Parcourt tous les enfants de l'objet et les met à jour
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layerName);
        }
    }
}
