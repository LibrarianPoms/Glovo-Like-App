using Aymane.netProjet.Data;
using Aymane.netProjet.Entities;

namespace Aymane.netProjet.Repos
{
    public class ProduitRepos
    {
        private readonly MyDbContextGlovo _context;
        public ProduitRepos(MyDbContextGlovo context)
        {
            _context = context;
        }
        public List<Produit> GetProduits()
        {
            return _context.Produits.ToList();
        }
        public Produit? GetProduitById(int id)
        {
            return _context.Produits.Find(id);
        }
        public void Add(Produit produit)
        {
            _context.Produits.Add(produit);
            _context.SaveChanges();
        }
        public void Update(Produit produit)
        {
            _context.Produits.Update(produit);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var produit = _context.Produits.Find(id);
            if (produit != null)
            {
                _context.Produits.Remove(produit);
                _context.SaveChanges();
            }
        }
    }
}
