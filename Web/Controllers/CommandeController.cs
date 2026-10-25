using Microsoft.AspNetCore.Mvc;
using Aymane.netProjet.Services;
using Aymane.netProjet.Entities;

namespace Aymane.netProjet.Controllers
{
    public class CommandeController : Controller
    {
        private readonly CommandeService _commandeService;

        public CommandeController(CommandeService commandeService)
        {
            _commandeService = commandeService;
        }

        public IActionResult Index()
        {
            var orders = _commandeService.GetAllOrders();
            return View(orders);
        }

        public IActionResult MyOrders()
        {
            var clientId = HttpContext.Session.GetInt32("ClientId");
            if (clientId == null)
            {
                return RedirectToAction("Login", "Client");
            }

            var orders = _commandeService.GetAllOrders()
                .Where(c => c.ClientId == clientId.Value)
                .OrderByDescending(c => c.CreateAt)
                .ToList();

            return View(orders);
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            var order = _commandeService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            var clientId = HttpContext.Session.GetInt32("ClientId");
            var role = HttpContext.Session.GetString("Role");
            
            if (role != "Admin" && order.ClientId != clientId)
            {
                return Forbid();
            }

            return View(order);
        }
    }
}
