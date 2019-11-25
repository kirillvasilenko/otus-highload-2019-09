using System.Collections.Generic;
using System.Threading.Tasks;

namespace UsersService.Model
{
    public interface IUsersRepo
    {
        Task<User> Get(long id);
        
        Task<User> GetByEmail(string email);

        Task<User> Add(User user);

        Task<User> Delete(long id);

        Task<int> GetUsersCount();
        
        Task<IEnumerable<User>> GetUsers(int skip, int take);
    }
}