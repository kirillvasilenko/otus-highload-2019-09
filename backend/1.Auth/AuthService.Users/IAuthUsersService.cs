using System.Threading.Tasks;

namespace AuthService.Users
{
    public interface IAuthUsersService
    {
        Task<bool> ValidateCredentials(string username, string password);

        Task<AuthUser> FindByUsername(string username);
        
        Task<AuthUser> FindById(long id);

    }
}