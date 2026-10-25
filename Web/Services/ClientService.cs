using Aymane.netProjet.Entities;
using Aymane.netProjet.Repos;

namespace Aymane.netProjet.Services
{
    public class ClientService
    {
        private readonly ClientRepos _clientRepos;
        public ClientService(ClientRepos clientRepos)
        {
            _clientRepos = clientRepos;
        }

        public List<Client> GetAllClients()
        {
            return _clientRepos.Gets();
        }

        public Client? GetClientById(int id)
        {
            return _clientRepos.Get(id);
        }

        public void CreateClient(Client client)
        {
            _clientRepos.Add(client);
        }

        public void UpdateClient(Client client)
        {
            _clientRepos.Update(client);
        }

        public void RegisterClient(Aymane.netProjet.Models.CreationClientViewModel model)
        {
            var passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<Client>();
            var client = new Client
            {
                Nom = model.Nom,
                Prenom = model.Prenom,
                Telephone = model.NumdeTelephone,
                Email = model.EMail,
                CreateAt = DateTime.Now,
                IsDelete = false
            };
            client.MotPasse = passwordHasher.HashPassword(client, model.MotPasse);
            _clientRepos.Add(client);
        }

        public Client? AuthenticateClient(string email, string password)
        {
            var client = _clientRepos.Gets().FirstOrDefault(c => c.Email == email);
            if (client == null) return null;

            var passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<Client>();
            var result = passwordHasher.VerifyHashedPassword(client, client.MotPasse, password);

            return result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success ? client : null;
        }

        public void DeleteClient(int id)
        {
            var client = _clientRepos.Get(id);
            if (client != null)
            {
                _clientRepos.Delete(client);
            }
        }
    }
}
