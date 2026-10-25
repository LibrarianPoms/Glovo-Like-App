using System.ComponentModel.DataAnnotations.Schema;
using Aymane.netProjet.Shared;

namespace Aymane.netProjet.Entities
{
    [Table("Commande")]
    public class Commande : Base
    {
        public int Numero { get; set; }
        public int ClientId { get; set; }
        public int? LivreurId { get; set; }
        public string Adresse { get; set; } = string.Empty;
        public decimal Montant { get; set; }
        public StatutCommande Statut { get; set; }
        public List<CommandeDetail> CommandeDetails { get; set; } 
    }
}
