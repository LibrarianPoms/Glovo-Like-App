using Aymane.netProjet.Entities;
using Aymane.netProjet.Repos;
using Aymane.netProjet.Shared;

namespace Aymane.netProjet.Services
{
    public class CommandeService
    {
        private readonly CommandeRepos _commandeRepos;
        public CommandeService(CommandeRepos commandeRepos)
        {
            _commandeRepos = commandeRepos;
        }

        public List<Commande> GetAllOrders()
        {
            return _commandeRepos.Gets();
        }

        public dynamic GetRecentOrdersWithClients(List<Client> clients)
        {
            var orders = _commandeRepos.Gets();
            var result = from c in orders
                         join cl in clients on c.ClientId equals cl.Id
                         orderby c.CreateAt descending
                         select new
                         {
                             c.Id,
                             ClientNom = cl.Nom + " " + cl.Prenom,
                             c.CreateAt,
                             c.Statut,
                             c.Montant
                         };
            return result.Take(5).ToList();
        }

        public Commande? GetOrderById(int id)
        {
            return _commandeRepos.Get(id);
        }

        public void ChangeOrderStatus(int id, StatutCommande status)
        {
            var order = _commandeRepos.Get(id);
            if (order != null)
            {
                order.Statut = status;
                _commandeRepos.Update(order);
            }
        }

        public void ProcessCheckout(int clientId, string adresse, List<Aymane.netProjet.Models.CartItemViewModel> cartItems)
        {
            var commande = new Commande
            {
                ClientId = clientId,
                CreateAt = DateTime.Now,
                Adresse = adresse,
                Montant = cartItems.Sum(i => i.Prix * i.Quantite),
                Statut = StatutCommande.EnPreparation,
                IsDelete = false
            };

            var details = cartItems.Select(item => new CommandeDetail
            {
                ProduitId = item.ProduitId,
                Quantite = item.Quantite,
                PrixUnitaire = item.Prix,
                CreateAt = DateTime.Now,
                IsDelete = false
            }).ToList();

            _commandeRepos.AddOrderWithDetails(commande, details);
        }

        public void DeleteOrder(int id)
        {
            _commandeRepos.Delete(id);
        }
    }
}
