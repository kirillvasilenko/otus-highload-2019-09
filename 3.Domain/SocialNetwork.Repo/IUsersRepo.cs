using System.Collections.Generic;
using System.Threading.Tasks;
using SocialNetwork.Model;

namespace SocialNetwork.Repo
{
    public interface IUsersRepo
    {
        Task<User> Get(long id);
        
        Task<User> GetByEmail(string email);

        Task<User> Add(User user);

        Task<User> Delete(long id);

        Task<IEnumerable<User>> GetUsers();
    }
}