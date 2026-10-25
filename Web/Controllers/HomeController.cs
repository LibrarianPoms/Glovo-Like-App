using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Aymane.netProjet.Models;

namespace Aymane.netProjet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Services.ProduitService _produitService;
        public HomeController(ILogger<HomeController> logger, 
            Services.ProduitService produitService)
        {
            _logger = logger;
            _produitService = produitService;
        }


        public IActionResult Index()
        {
            return View(_produitService.GetProduitIndex());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}
