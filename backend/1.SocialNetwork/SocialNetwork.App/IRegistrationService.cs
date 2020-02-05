using System.Threading.Tasks;
using SocialNetwork.App.Dtos;

namespace SocialNetwork.App
{
    public interface IRegistrationService
    {
        Task<UserDto> RegisterUser(RegisterUserData newUser);        
    }
}