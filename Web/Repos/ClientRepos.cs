using Aymane.netProjet.Data;
using Aymane.netProjet.Entities;

namespace Aymane.netProjet.Repos;
public class ClientRepos
{
    private readonly MyDbContextGlovo _context;
    public ClientRepos(MyDbContextGlovo context)
    {
        _context = context;
    }

    public Client? Get(int Id)
    {
        return _context.Clients.Find(Id);
    }

    public List<Client> Gets()
    {
        return _context.Clients.ToList();
    }
    public void Add(Client client)
    {
        _context.Clients.Add(client);
        _context.SaveChanges();
    }
    public void Update(Client client)
    {
        _context.Clients.Update(client);
        _context.SaveChanges();
    }
    public void Delete(Client client)
    {
        _context.Clients.Remove(client);
        _context.SaveChanges();
    }

}
