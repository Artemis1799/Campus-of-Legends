using System.Collections.Generic;

/// <summary>
/// Cette classe contient des paramètres statiques qui sont utilisés pour partager des informations
/// entre les scènes, spécifiquement pour le trombinoscope.
/// Elle permet de stocker des informations telles que l'appelant de la scène (par exemple, le nom de la scène)
/// et la liste des employés à afficher dans le trombinoscope.
/// </summary>
public static class TrombinoParam
{
    /// <summary>
    /// Le nom de la scène appelante, utilisé pour déterminer d'où provient la demande pour afficher le trombinoscope.
    /// Ce paramètre peut être utilisé pour effectuer des actions spécifiques à la scène d'origine.
    /// </summary>
    public static string caller;
    /// <summary>
    /// La liste des employés à afficher dans le trombinoscope.
    /// Cette liste est remplie par le gestionnaire d'employés et est utilisée pour afficher les informations des employés
    /// dans la scène du trombinoscope.
    /// </summary>
    public static List<EmployeeData> Employees;
}
