using System.Threading.Tasks;
using AuthService.Users.Dtos;

namespace AuthService.Users
{
    public interface IRegistrationUsersService
    {
        Task<AuthUser> RegisterUser(RegisterUserData newUser);        
    }
}