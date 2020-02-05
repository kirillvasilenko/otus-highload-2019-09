using System.Threading.Tasks;
using SocialNetwork.App.Dtos;
using SocialNetwork.Model;

namespace SocialNetwork.App
{
    public interface IAuthService
    {
        Task<TokenDto> AuthenticateUser(string email, string password);

        Task<TokenDto> AuthenticateUser(UserDto user);
        
        Task<bool> ValidateCredentials(string username, string password);

        Task<UserDto> FindByUsername(string username);
        
        Task<UserDto> FindById(long id);

    }
}