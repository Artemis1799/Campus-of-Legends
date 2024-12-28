using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// La classe <c>ButtonOpenForm</c> gère l'interaction entre un bouton et un formulaire d'employé. 
/// Elle permet d'ouvrir et de fermer le formulaire d'employé avec une animation associée, et met à jour les informations affichées dans le formulaire en fonction des données de l'employé.
/// </summary>
public class ButtonOpenForm : MonoBehaviour
{
    public Animator animator;
    public Button mainButton;
    public EmployeeForm employeeForm;
    public string employeeName; 
    public float efficiency;
    public float team;
    public int salary;
    public float fidelity;
    /// <summary>
    /// Méthode appelée au démarrage pour configurer l'interaction entre le bouton et le formulaire.
    /// Elle ajoute un listener au bouton pour ouvrir le formulaire avec les informations de l'employé lorsque le bouton est cliqué.
    /// </summary>
    void Start()
    {
        if (mainButton != null && animator != null)
        {
            mainButton.onClick.AddListener(() =>
            {
                Debug.Log("Button clicked, toggling animation.");
                employeeForm.UpdateForm(employeeName,salary,efficiency,team, fidelity);
                OnMainButtonClick();
            });
        }
        else
        {
            Debug.LogError("ButtonOpenForm: Missing references - MainButton or Animator not assigned.");
        }
    }
    /// <summary>
    /// Méthode appelée lorsque le bouton est cliqué. Elle déclenche l'animation d'ouverture ou de fermeture du formulaire en inversant l'état de l'animation.
    /// </summary>
    void OnMainButtonClick()
    {
        if (animator != null)
        {
            bool currentState = animator.GetBool("IsOpening");
            Debug.Log($"Current IsOpening: {currentState}, toggling to {!currentState}");
            animator.SetBool("IsOpening", !currentState);
        }
    }
    /// <summary>
    /// Méthode permettant d'assigner les références du bouton principal et de l'animator à la classe.
    /// </summary>
    /// <param name="button">Le bouton principal qui déclenche l'ouverture du formulaire.</param>
    /// <param name="anim">L'animator utilisé pour l'animation du formulaire.</param>
    public void AssignReferences(Button button, Animator anim)
    {
        mainButton = button;
        animator = anim;
    }
    /// <summary>
    /// Méthode pour fermer le formulaire d'employé. Elle désactive l'animation d'ouverture et cache le formulaire.
    /// </summary>
    public void CloseForm()
{
    if (animator != null)
    {
        Debug.Log("Current IsOpening toggling to false");
        animator.SetBool("IsOpening", false);
        if (employeeForm != null)
        {
            employeeForm.gameObject.SetActive(false);
        }
    }
}

}
