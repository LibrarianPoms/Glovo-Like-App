using Aymane.netProjet.Data;
using Aymane.netProjet.Entities;
using Aymane.netProjet.Shared;
using Microsoft.EntityFrameworkCore;

namespace Aymane.netProjet.Repos
{
    public class CommandeRepos
    {
        private readonly MyDbContextGlovo _context;
        public CommandeRepos(MyDbContextGlovo context)
        {
            _context = context;
        }

        public Commande? Get(int id)
        {
            return _context.Commandes.Find(id);
        }

        public List<Commande> Gets()
        {
            return _context.Commandes.ToList();
        }

        public void Add(Commande commande)
        {
            _context.Commandes.Add(commande);
            _context.SaveChanges();
        }

        public void AddOrderWithDetails(Commande commande, List<CommandeDetail> details)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Commandes.Add(commande);
                _context.SaveChanges();

                foreach (var detail in details)
                {
                    detail.CommandeId = commande.Id;
                    _context.CommandeDetails.Add(detail);
                }
                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(Commande commande)
        {
            _context.Commandes.Update(commande);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var commande = _context.Commandes.Find(id);
            if (commande != null)
            {
                _context.Commandes.Remove(commande);
                _context.SaveChanges();
            }
        }
    }
}
