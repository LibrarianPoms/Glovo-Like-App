using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Aymane.netProjet.Entities;
using Aymane.netProjet.Models;
using Aymane.netProjet.Services;

namespace Aymane.netProjet.Controllers
{
    public class CartController : Controller
    {
        private readonly ProduitService _produitService;
        private readonly CommandeService _commandeService;
        private const string CartSessionKey = "Cart";

        public CartController(ProduitService produitService, CommandeService commandeService)
        {
            _produitService = produitService;
            _commandeService = commandeService;
        }

        private List<CartItemViewModel> GetCart()
        {
            var json = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(json))
            {
                return new List<CartItemViewModel>();
            }

            try
            {
                return JsonSerializer.Deserialize<List<CartItemViewModel>>(json) ?? new List<CartItemViewModel>();
            }
            catch
            {
                return new List<CartItemViewModel>();
            }
        }

        private void SaveCart(List<CartItemViewModel> cart)
        {
            var json = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CartSessionKey, json);
        }

        [HttpPost]
        public IActionResult Add(int productId)
        {
            if (HttpContext.Session.GetString("Role") != "Client")
            {
                return RedirectToAction("Login", "Client");
            }

            var product = _produitService.GetProduitById(productId);
            if (product == null)
            {
                return NotFound();
            }

            var cart = GetCart();
            var existing = cart.FirstOrDefault(c => c.ProduitId == productId);
            if (existing == null)
            {
                cart.Add(new CartItemViewModel
                {
                    ProduitId = product.Id,
                    Titre = product.Titre,
                    Prix = product.Prix,
                    Quantite = 1
                });
            }
            else
            {
                existing.Quantite += 1;
            }

            SaveCart(cart);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult Update(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProduitId == productId);
            if (item != null)
            {
                if (quantity <= 0)
                {
                    cart.Remove(item);
                }
                else
                {
                    item.Quantite = quantity;
                }

                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProduitId == productId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Checkout(string adresse, string telephone)
        {
            if (HttpContext.Session.GetString("Role") != "Client")
            {
                return RedirectToAction("Login", "Client");
            }

            var cart = GetCart();
            if (cart.Count == 0)
            {
                TempData["CartError"] = "Votre panier est vide.";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(adresse) || string.IsNullOrWhiteSpace(telephone))
            {
                TempData["CartError"] = "Adresse et téléphone sont obligatoires.";
                return RedirectToAction("Index");
            }

            var clientId = HttpContext.Session.GetInt32("ClientId");
            if (clientId == null)
            {
                return RedirectToAction("Login", "Client");
            }

            try
            {
                _commandeService.ProcessCheckout(clientId.Value, adresse, cart);
                HttpContext.Session.Remove(CartSessionKey);
                return RedirectToAction("Confirmation", "Commande");
            }
            catch (Exception ex)
            {
                TempData["CartError"] = "Une erreur est survenue lors de la commande : " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
