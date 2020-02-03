using System.Threading.Tasks;
using SocialNetwork.App.Dtos;

namespace SocialNetwork.App
{
    public interface IRegistrationUsersService
    {
        Task<UserDto> RegisterUser(RegisterUserData newUser);        
    }
}