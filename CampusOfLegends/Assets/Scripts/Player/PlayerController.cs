using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
/// <summary>
/// Cette classe gère le contrôle du joueur dans le jeu, y compris le mouvement, la rotation,
/// l'interaction avec les bâtiments, et la gestion des animations et des effets visuels (comme les particules de clic).
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Vitesse de déplacement du personnage
    public float rotationSpeed = 720f; // Vitesse de rotation du personnage
    private Vector3 targetPosition; // La position vers laquelle le personnage doit se déplacer
    private bool isMoving = false; // Booléen pour savoir si le personnage est en mouvement
    public bool clickedOnBuilding = false;
    public int lastBuildingLayer = -1;
    public static SettingsManager Instance { get; private set; }

    public AudioSource footstepAudioSource;

    public ParticleSystem clickParticle;
    private ParticleSystem currentParticleInstance; // Référence à l'instance créée du système de particules
    Animator m_Animator;

    /// <summary>
    /// Initialise les paramètres de départ du joueur, y compris l'animation et le son des pas.
    /// </summary>
    void Start()
    {
        // Initialise la position cible à la position actuelle du personnage
        targetPosition = transform.position;
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool("isWalking", false);
        SettingsManager.Instance.SoundsSource.Add(footstepAudioSource);
        footstepAudioSource.volume = SettingsManager.Instance.SoundVolume;
    }

    /// <summary>
    /// Empêche la destruction de cet objet lors du changement de scène.
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    /// <summary>
    /// Met à jour la position et l'animation du personnage, en gérant le mouvement, le clic sur les bâtiments,
    /// et l'interaction avec l'environnement.
    /// </summary>
    void Update()
    {
        if (transform.position.y < -3f)
        {
            // Position de spawn
            Vector3 spawnPosition = new Vector3(29.37599f, -2.616484f, 3.482854f);

            // Repositionner le joueur à la position de spawn
            transform.position = spawnPosition;

            // Réinitialiser la position cible pour éviter des mouvements non voulus
            targetPosition = spawnPosition;
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            targetPosition = new Vector3(-4.99f, 0.50f, 5.75f);
            transform.position = targetPosition;
        }
        if (PauseMenu.isPaused)
        {
            m_Animator.SetBool("isWalking", false);
            return; // Ne traite pas les clics de la souris si le jeu est en pause
        }

        // Vérifie si le joueur a cliqué avec le bouton gauche de la souris
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
                if (selectedObject != null && selectedObject.GetComponent<Button>() != null)
                {
                    return; // Ignore le clic si c'est sur un bouton
                }

            }
            // Crée un ray à partir de la position de la caméra vers la position de la souris
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int layerMask = ~LayerMask.GetMask("murOffMap");

            // Si le raycast touche une surface
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.gameObject != gameObject)
                {
                    if (hit.collider.name == "murOffMap")
                    {
                        return; // Ignore le clic si l'objet cliqué est "murOffMap"
                    }
                    if (hit.collider.CompareTag("Building"))
                    {
                        clickedOnBuilding = true; // Le joueur a cliqué sur un bâtiment
                        lastBuildingLayer = hit.collider.gameObject.layer;  // en gros quand le joueur clique sur un bâtiment on récupère le layer pour la condition dans LoadScene
                    }
                    else
                    {
                        clickedOnBuilding = false;
                        lastBuildingLayer = -1;
                    }

                    // Stocke la position du clic comme la nouvelle position cible
                    targetPosition = hit.point;
                    targetPosition.y = 0.612f; // Ajuste la hauteur si nécessaire
                    isMoving = true; // Le personnage commence à se déplacer

                    // Si une particule existe, la détruire
                    if (currentParticleInstance != null)
                    {
                        Destroy(currentParticleInstance.gameObject);
                    }

                    // Instancier la nouvelle particule
                    currentParticleInstance = Instantiate(clickParticle, targetPosition, Quaternion.identity);
                }
            }
        }

        // Si le personnage doit se déplacer
        if (isMoving)
        {
            // Calculer la direction du personnage vers la position cible
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Faire tourner le personnage vers la direction cible
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Déplacer le personnage vers la position cible
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            m_Animator.SetBool("isWalking", true);

            // Jouer le son de pas
            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Play();
            }

            // Arrêter le mouvement lorsque le personnage atteint la position cible
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                m_Animator.SetBool("isWalking", false);

                // Arrêter le son de pas
                footstepAudioSource.Stop();

                // Si une particule existe, la détruire
                if (currentParticleInstance != null)
                {
                    Destroy(currentParticleInstance.gameObject);
                }
            }
        }
    }

    /// <summary>
    /// Réinitialise la position cible à la position actuelle du personnage et arrête le mouvement.
    /// </summary>
    public void ResetTargetPosition()
    {
        targetPosition = transform.position; // Réinitialiser la position cible à la position actuelle du personnage
        isMoving = false; // Arrêter le mouvement
        m_Animator.SetBool("isWalking", false); // Arrêter l'animation de marche
        footstepAudioSource.Stop(); // Arrêter le son de pas
    }

    /// <summary>
    /// Déplace le joueur à une position prédéfinie en dehors de la carte.
    /// </summary>
    public void ToOutMap()
    {
        targetPosition = new Vector3(-55.81f, 2f, -96.2f);
        transform.position = targetPosition;
    }

}
