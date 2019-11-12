using System.Threading.Tasks;
using SocialNetwork.Model;

namespace SocialNetwork
{
    public interface IAuthService
    {
        Task<User> Authenticate(string email, string password);
    }
}