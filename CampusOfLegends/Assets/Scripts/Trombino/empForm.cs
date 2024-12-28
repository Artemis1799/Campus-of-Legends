using UnityEngine;
using TMPro;


/// <summary>
/// Cette classe gère l'affichage des informations d'un employé dans un formulaire.
/// Elle met à jour les différents champs TextMeshPro avec des données telles que le nom,
/// le salaire, l'efficacité, la cohésion de l'équipe et la fidélité.
/// </summary>
public class EmployeeForm : MonoBehaviour
{
    public TextMeshProUGUI nameText; // Champ pour afficher le nom de l'employé
    public TextMeshProUGUI salaryText; // Autres informations si nécessaires
    public TextMeshProUGUI efficiencyText; // Autres informations si nécessaires
    public TextMeshProUGUI teamText; // Autres informations si nécessaires
    public TextMeshProUGUI fidelityText; // Autres informations si nécessaires


    /// <summary>
    /// Met à jour les champs du formulaire avec les informations de l'employé.
    /// </summary>
    /// <param name="name">Le nom de l'employé.</param>
    /// <param name="salary">Le salaire de l'employé.</param>
    /// <param name="efficiency">L'efficacité de l'employé (sur 5).</param>
    /// <param name="team">La cohésion de l'équipe de l'employé (sur 5).</param>
    /// <param name="fidelity">La fidélité de l'employé (sur 5).</param>
    public void UpdateForm(string name, int salary, float efficiency, float team, float fidelity)
    {
        nameText.text = name;
        salaryText.text = "Salaire : "+salary;
        efficiencyText.text = "Efficacité : "+efficiency+" / 5";
        teamText.text = "Cohésion : " +team+ " / 5";
        fidelityText.text = "Fidélité : "+fidelity+" / 5";
    }
}
