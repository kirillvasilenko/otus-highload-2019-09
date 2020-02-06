using System.Threading.Tasks;

namespace SocialNetwork.Model
{
    public interface ITokenRepo
    {
        
        Task<RefreshToken> AddRefreshToken(RefreshToken token);
        
        Task<RefreshToken> GetRefreshToken(string refreshToken);

        Task ResetRefreshToken(string refreshToken);

        Task ResetAllRefreshTokens(long userId);
    }
}