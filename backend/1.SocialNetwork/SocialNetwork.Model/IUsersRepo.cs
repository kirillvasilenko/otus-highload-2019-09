using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{
    public interface IUsersRepo
    {
        Task<User> GetUser(long id, bool throwExceptionIfNotFound = true);
        
        Task<User> GetUserByEmail(string email, bool throwExceptionIfNotFound = true);

        Task<User> AddUser(User user);

        Task<User> DeleteUser(long id);

        Task<int> GetUsersCount();
        
        Task<IEnumerable<User>> GetUsers(int skip, int take);
    }
}