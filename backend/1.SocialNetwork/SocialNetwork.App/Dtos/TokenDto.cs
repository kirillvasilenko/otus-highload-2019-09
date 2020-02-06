

namespace SocialNetwork.App.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        
        public long AccessTokenExpiresIn { get; set; }
        
        public string RefreshToken { get; set; }
        
        public long RefreshTokenExpiresIn { get; set; }
        
    }
}