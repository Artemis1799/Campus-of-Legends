using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La classe <c>empLine</c> gère les actions liées à un employé dans une interface utilisateur.
/// Elle permet d'effectuer différentes actions comme l'ajout de primes, l'envoi en formation,
/// et le licenciement d'un employé. Chaque action met à jour les données de l'employé et ajuste
/// les ressources du jeu.
/// </summary>
public class empLine : MonoBehaviour
{
    private EmployeeData _emp;

    /// <summary>
    /// Méthode appelée lors du clic sur un bouton dans l'interface.
    /// </summary>
    /// <param name="button">L'identifiant du bouton cliqué (0 = ajouter prime, 1 = envoyer en formation, 2 = licencier).</param>
    public void OnButtonClick(int button)
    {
        if (button == 0)
        {
            addPrime();

        }
        else if (button == 1)
        {
            sendForTaining();

        }
        else if (button == 2)
        {
            fired();

        }
        CloseConfirmation.InstanceConfirmation.setConfirmationMenuActive();
        CanvasUpdater.Instance.UpdateCanvas();
    }

    /// <summary>
    /// Méthode pour définir les données de l'employé à afficher ou modifier.
    /// </summary>
    /// <param name="empData">Les données de l'employé.</param>
    public void setEmpData(EmployeeData empData)
    {
        this._emp = empData;
    }

    /// <summary>
    /// Méthode pour ajouter une prime à un employé. Un montant aléatoire est attribué à la prime,
    /// et une somme est déduite des ressources du jeu.
    /// </summary>
    public void addPrime()
    {
        float randomValue = Random.Range(0.5f, 1.5f);
        EmployeeManager.Instance.addPrime(randomValue, this._emp);
        ResourceManager.Instance.AddMoney(-1000);
    }

    /// <summary>
    /// Méthode pour envoyer un employé en formation. L'efficacité au travail et la cohésion de l'équipe
    /// de l'employé sont améliorées avec des valeurs aléatoires, et des ressources sont déduites.
    /// </summary>
    private void sendForTaining()
    {
        float randomValue1 = Random.Range(0.5f, 1.5f);
        EmployeeManager.Instance.moreWorkEfficiency( randomValue1, this._emp);
        float randomValue2 = Random.Range(0.5f, 1.5f);
        EmployeeManager.Instance.moreTeamEfficiency( randomValue2, this._emp);
        ResourceManager.Instance.AddMoney(-4000);
        if (CrisisManager.Instance != null)
        {
            CrisisManager.Instance.RemoveFromConflicts(this._emp);
        }
    }
    /// <summary>
    /// Méthode pour licencier un employé. L'employé est retiré de la gestion et les ressources sont ajustées.
    /// </summary>
    private void fired()
    {
        if (_emp != null && EmployeeManager.Instance != null)
        {
            EmployeeManager.Instance.FireEmployee(_emp.job, _emp);
            Debug.Log("Employee fired: " + _emp.GetFirstName() + " " + _emp.GetLastName());
            EmployeeTableManager.Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Employee data or EmployeeManager instance is null.");
        }
    }
}
