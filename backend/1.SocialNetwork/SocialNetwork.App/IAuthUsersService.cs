using System.Threading.Tasks;
using SocialNetwork.App.Dtos;

namespace SocialNetwork.App
{
    public interface IAuthUsersService
    {
        Task<bool> ValidateCredentials(string username, string password);

        Task<UserDto> FindByUsername(string username);
        
        Task<UserDto> FindById(long id);

    }
}