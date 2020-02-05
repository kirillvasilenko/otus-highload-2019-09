using System.Threading.Tasks;

namespace SocialNetwork.Model
{
    public interface ITokenRepo
    {
        Task<TokenBound> IssueToken();
        
        Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
        
        Task<RefreshToken> GetRefreshToken(string refreshToken);

        Task ResetRefreshToken(string refreshToken);

        Task ResetAllRefreshTokens(long userId);
    }
}