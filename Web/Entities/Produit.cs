using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aymane.netProjet.Entities
{
    [Table("Produit")]
    public class Produit : Base
    {
        [MaxLength(300)]
        public string Titre { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Prix { get; set; } = 0;
        public bool Stock { get; set; }
    }
}
