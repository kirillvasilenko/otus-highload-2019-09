using System.Globalization;

namespace SocialNetwork.Model
{
    public class RefreshToken
    {
        public long Id { get; set; }
        
        public long UserId { get; set; }
        
        public string Token { get; set; }
        
        public long ExpirationTime { get; set; }
    }
}