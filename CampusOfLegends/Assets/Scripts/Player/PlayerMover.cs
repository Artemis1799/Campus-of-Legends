using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Cette classe gère le mouvement du personnage dans le jeu, y compris la position cible, la rotation, 
/// l'animation de marche et l'activation d'écrans de fin ou de crédits lorsque le personnage arrive à sa destination.
/// </summary>
public class PlayerMover : MonoBehaviour
{
    public float speed = 1f; // Vitesse de déplacement du personnage
    public float rotationSpeed = 720f; // Vitesse de rotation du personnage
    public float targetPositionX;
    public float targetPositionZ;
    public CameraEndingScreen cms;
    private Vector3 targetPosition; // La position vers laquelle le personnage doit se déplacer
    private bool isMoving = false; // Booléen pour savoir si le personnage est en mouvement
    private bool DoNothing = false;
    public CreditsScroller crs;
    Animator m_Animator;

    /// <summary>
    /// Initialise les paramètres de départ du joueur, y compris la position cible et l'animation de marche.
    /// </summary>
    void Start()
    {
        // Initialise la position cible à la position actuelle du personnage
        targetPosition = transform.position;
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool("isWalking", true);
    }
    /// <summary>
    /// Empêche la destruction du joueur lors du changement de scène.
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Met à jour la position et l'animation du personnage. Si le personnage atteint sa destination,
    /// il déclenche les actions de fin et les crédits.
    /// </summary>
    void Update()
    {
        if(!DoNothing)
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

        targetPosition.y = 0.650f; // Ajuste la hauteur si nécessaire
        targetPosition.x= targetPositionX;
        targetPosition.z=targetPositionZ;
        isMoving = true; // Le personnage commence à se déplacer


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

            // Arrêter le mouvement lorsque le personnage atteint la position cible
            if (Vector3.Distance(transform.position, targetPosition) < 0.8f)
            {
                isMoving = false;
                m_Animator.SetBool("isWalking", false);
                cms.SetIsArrived();
                crs.setIsDisplayed();
                DoNothing = true;
            }
        }
        }
    }

    /// <summary>
    /// Réinitialise la position cible et arrête le mouvement du personnage.
    /// </summary>
    public void ResetTargetPosition2()
    {
        targetPosition = transform.position; // Réinitialiser la position cible à la position actuelle du personnage
        isMoving = false; // Arrêter le mouvement
        m_Animator.SetBool("isWalking", false); // Arrêter l'animation de marche
    }
}
