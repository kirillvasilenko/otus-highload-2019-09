using System.Globalization;

namespace SocialNetwork.Model
{
    public class TokenBound
    {
        public AccessToken AccessToken { get; set; }

        public RefreshToken RefreshToken { get; set; }
    
}
    
    public class AccessToken
    {
        public long Id { get; set; }
        
        public long UserId { get; set; }
        
        public string Token { get; set; }
        
        public long ExpirationTime { get; set; }
    }
    
    public class RefreshToken
    {
        public long Id { get; set; }
        
        public long UserId { get; set; }
        
        public string Token { get; set; }
        
        public long ExpirationTime { get; set; }
    }
}