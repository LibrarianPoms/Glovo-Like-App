using Microsoft.AspNetCore.Mvc;
using Aymane.netProjet.Services;
using Aymane.netProjet.Models;

namespace Aymane.netProjet.Controllers
{
    public class ProduitController : Controller
    {
        private readonly ProduitService _produitService;

        public ProduitController(ProduitService produitService)
        {
            _produitService = produitService;
        }

        public IActionResult Details(int id)
        {
            var produit = _produitService.GetProduitById(id);

            if (produit == null)
                return NotFound();

            var viewModel = new ProduitListViewModel
            {
                Id = produit.Id,
                Titre = produit.Titre,
                Description = produit.Description,
                Prix = produit.Prix
            };

            return View(viewModel);
        }
    }
}
