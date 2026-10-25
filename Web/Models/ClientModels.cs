using System.ComponentModel.DataAnnotations;

namespace Aymane.netProjet.Models;

public class CreationClientViewModel
{
    [Required]
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string NumdeTelephone { get; set; }
    [DataType(DataType.EmailAddress)]
    public string EMail { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string MotPasse { get; set; }



}
