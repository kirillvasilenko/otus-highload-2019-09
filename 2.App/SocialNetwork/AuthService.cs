using System;
using System.Threading.Tasks;
using SocialNetwork.Model;
using SocialNetwork.Repo;

namespace SocialNetwork
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepo repo;

        public AuthService(IUsersRepo repo)
        {
            this.repo = repo;
        }
        
        public async Task<User> Authenticate(string email, string password)
        {
            var user = await repo.GetByEmail(email);

            if (user.Password != password)
            {
                throw new InvalidOperationException("Email or password is wrong.");
            }

            return user;
        }
    }
}