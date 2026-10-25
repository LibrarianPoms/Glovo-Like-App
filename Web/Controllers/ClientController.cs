using Microsoft.AspNetCore.Mvc;
using Aymane.netProjet.Entities;
using Aymane.netProjet.Models;
using Aymane.netProjet.Services;

namespace Aymane.netProjet.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(CreationClientViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _clientService.RegisterClient(model);

            return RedirectToAction("Login", "Client");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Client model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.MotPasse))
            {
                ModelState.AddModelError("", "Email et mot de passe obligatoires");
                return View(model);
            }

            // Hardcoded Admin Login
            if (model.Email == "admin@admin.com" && model.MotPasse == "admin")
            {
                HttpContext.Session.SetString("Role", "Admin");
                return RedirectToAction("Dashboard", "Admin");
            }

            // Client Login
            var client = _clientService.AuthenticateClient(model.Email, model.MotPasse);

            if (client == null)
            {
                ModelState.AddModelError("", "Email ou mot de passe incorrect");
                return View(model);
            }

            HttpContext.Session.SetInt32("ClientId", client.Id);
            HttpContext.Session.SetString("ClientNom", client.Nom);
            HttpContext.Session.SetString("ClientPrenom", client.Prenom);
            HttpContext.Session.SetString("Role", "Client");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
