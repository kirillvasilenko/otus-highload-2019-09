using System.Threading.Tasks;
using SocialNetwork.Dtos;
using SocialNetwork.Model;

namespace SocialNetwork
{
    public interface IRegistrationService
    {
        Task<User> RegisterUser(RegisterUserData data);
    }
}