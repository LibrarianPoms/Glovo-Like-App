using System.ComponentModel.DataAnnotations.Schema;

namespace Aymane.netProjet.Entities
{
    public class CommandeDetail : Base
    {
        [ForeignKey("Commande")]
        public int CommandeId { get; set; }
        public Commande Commande { get; set; } = null!;
        public int ProduitId { get; set; }
        public int Quantite { get; set; }
        public decimal PrixUnitaire { get; set; }




    }
}
