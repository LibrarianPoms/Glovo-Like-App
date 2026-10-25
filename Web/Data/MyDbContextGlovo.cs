using Microsoft.EntityFrameworkCore;
using Aymane.netProjet.Entities;

namespace Aymane.netProjet.Data;
public class MyDbContextGlovo : DbContext
{

    public MyDbContextGlovo(DbContextOptions<MyDbContextGlovo> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Commande> Commandes { get; set; }
    public DbSet<Livreur> Livreurs { get; set; }
    public DbSet<Produit> Produits { get; set; }
    public DbSet<CommandeDetail> CommandeDetails { get; set; }
}
