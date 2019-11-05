using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using SocialNetwork.Model;
using SocialNetwork.Repo;

namespace SocialNetwork
{
    public class UsersService:IUsersService
    {
        private readonly IUsersRepo repo;

        public UsersService(IUsersRepo repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await repo.GetUsers();
        }

        public async Task<User> GetUser(long id)
        {
            return await repo.Get(id);
        }
    }
}