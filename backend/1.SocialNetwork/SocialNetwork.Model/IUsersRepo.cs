using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{
    public interface IUsersRepo
    {
        Task<User> GetUser(long id, bool throwExceptionIfNotFound = true);
        
        Task<User> GetUserByEmail(string email, bool throwExceptionIfNotFound = true);

        Task<User> AddUser(User user);

        Task<User> UpdateUser(User user);

        Task DeleteUser(long userId);

        Task<int> GetUsersCount(GetUsersQuery query);
        
        Task<IEnumerable<User>> GetUsers(GetUsersQuery query, int skip, int take);
    }
}