using IdentityModel;
using IdentityServer4.ResponseHandling;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.AspNet.Controllers
{
    public class TokenResultDto
    {
        public static TokenResultDto FromResponse(TokenResponse response)
        {
            return new TokenResultDto
            {
                id_token = response.IdentityToken,
                access_token = response.AccessToken,
                refresh_token = response.RefreshToken,
                expires_in = response.AccessTokenLifetime,
                token_type = OidcConstants.TokenResponse.BearerTokenType,
                scope = response.Scope
            };
        }
        
        public string id_token { get; set; }
        
        public string access_token { get; set; }
        
        public int expires_in { get; set; }
        
        public string token_type { get; set; }
        
        public string refresh_token { get; set; }
        
        public string scope { get; set; }
        
    }
}