using System.Collections.Generic;
using System.Threading.Tasks;
using UsersService.Model;

namespace UsersService
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetUsers(int skip, int take);
        
        Task<User> GetUser(long id);
    }
}