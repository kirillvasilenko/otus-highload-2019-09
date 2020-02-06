using System.Threading.Tasks;
using SocialNetwork.App.Dtos;
using SocialNetwork.Model;

namespace SocialNetwork.App
{
    public interface IAuthService
    {
        Task<TokenDto> IssueToken(string email, string password);

        Task<TokenDto> RefreshToken(string refreshToken);

        Task ResetToken(string refreshToken);

        Task ResetAllTokens(long userId);

    }
}