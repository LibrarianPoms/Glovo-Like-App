using Aymane.netProjet.Entities;
using Aymane.netProjet.Models;

namespace Aymane.netProjet.Services
{
    public class ProduitService
    {
        private readonly Repos.ProduitRepos _produitRepos;
        public ProduitService(Repos.ProduitRepos produitRepos)
        {
            _produitRepos = produitRepos;
        }

        public List<ProduitListViewModel> GetProduitIndex()
        {
           return _produitRepos.GetProduits()
            .Where(a => a.Stock == true)
            .Select(p => new ProduitListViewModel
           {
               Id = p.Id,
               Titre = p.Titre,
               Description = p.Description,
               Prix = p.Prix
           }).ToList();
        }

        public List<Produit> GetAllProduits()
        {
            return _produitRepos.GetProduits();
        }

        public Produit? GetProduitById(int id)
        {
            return _produitRepos.GetProduitById(id);
        }

        public void AddProduit(Produit produit)
        {
            _produitRepos.Add(produit);
        }

        public void UpdateProduit(Produit produit)
        {
            _produitRepos.Update(produit);
        }

        public void DeleteProduit(int id)
        {
            _produitRepos.Delete(id);
        }
    }
}
