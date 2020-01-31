using System.Collections.Generic;
using System.Threading.Tasks;

namespace UsersService.Model
{
    public interface IUsersRepo
    {
        Task<User> GetUser(long id);
        
        Task<User> GetUserByEmail(string email);

        Task<User> AddUser(User user);

        Task<User> DeleteUser(long id);

        Task<int> GetUsersCount();
        
        Task<IEnumerable<User>> GetUsers(int skip, int take);
    }
}