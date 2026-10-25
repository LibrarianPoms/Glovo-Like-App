using System;

namespace Aymane.netProjet.Models
{
    public class CartItemViewModel
    {
        public int ProduitId { get; set; }
        public string Titre { get; set; } = string.Empty;
        public decimal Prix { get; set; }
        public int Quantite { get; set; }

        public decimal Total => Prix * Quantite;
    }
}
