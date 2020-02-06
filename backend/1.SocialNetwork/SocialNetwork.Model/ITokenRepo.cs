using System.Threading.Tasks;

namespace SocialNetwork.Model
{
    public interface ITokenRepo
    {
        
        Task<RefreshToken> AddRefreshToken(RefreshToken token);
        
        Task<RefreshToken> GetRefreshToken(string refreshToken);

        Task DeleteRefreshToken(long tokenId);
        
        Task DeleteRefreshToken(string refreshToken);

        Task DeleteAllRefreshTokens(long userId);
    }
}