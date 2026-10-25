using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Aymane.netProjet.Entities;
using Aymane.netProjet.Data;
using Aymane.netProjet.Shared;

using Aymane.netProjet.Attributes;

namespace Aymane.netProjet.Controllers
{
    [AdminSessionAuthorize]
    public class AdminController : Controller
    {
        private readonly Services.ClientService _clientService;
        private readonly Services.CommandeService _commandeService;
        private readonly Services.ProduitService _produitService;

        public AdminController(
            Services.ClientService clientService,
            Services.CommandeService commandeService,
            Services.ProduitService produitService)
        {
            _clientService = clientService;
            _commandeService = commandeService;
            _produitService = produitService;
        }
        public IActionResult Dashboard()
        {
            var clients = _clientService.GetAllClients();
            var orders = _commandeService.GetAllOrders();

            // Statistics
            ViewData["TotalUsers"] = clients.Count();
            ViewData["TotalOrders"] = orders.Count();
            ViewData["TotalRevenue"] = orders.Sum(c => c.Montant); 
            ViewData["PendingTickets"] = 0; 

            // Recent Orders
            ViewBag.RecentOrders = _commandeService.GetRecentOrdersWithClients(clients);

            return View();
        }

        public IActionResult Users()
        {
            var users = _clientService.GetAllClients();
            return View(users);
        }


        [HttpPost]
        public IActionResult SetOrderStatus(int id, StatutCommande status)
        {
            _commandeService.ChangeOrderStatus(id, status);
            return RedirectToAction("Dashboard");
        }

        public IActionResult DeleteUser(int id)
        {
            _clientService.DeleteClient(id);
            return RedirectToAction("Users");
        }


        public IActionResult Products()
        {
            var products = _produitService.GetAllProduits();
            return View(products);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Produit model)
        {
            if (ModelState.IsValid)
            {
                model.CreateAt = DateTime.Now;
                model.IsDelete = false;
                _produitService.AddProduit(model);
                return RedirectToAction("Products");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = _produitService.GetProduitById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Produit model)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _produitService.GetProduitById(model.Id);
                if (existingProduct != null)
                {
                    existingProduct.Titre = model.Titre;
                    existingProduct.Description = model.Description;
                    existingProduct.Prix = model.Prix;
                    existingProduct.Stock = model.Stock;
                    
                    _produitService.UpdateProduit(existingProduct);
                    return RedirectToAction("Products");
                }
            }
            return View(model);
        }

        public IActionResult DeleteProduct(int id)
        {
            _produitService.DeleteProduit(id);
            return RedirectToAction("Products");
        }
    }
}
