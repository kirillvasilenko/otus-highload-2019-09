namespace SocialNetwork.Model
{
    public class AccessToken
    {
        public long Id { get; set; }
        
        public long UserId { get; set; }
        
        public string Token { get; set; }
        
        public long ExpirationTime { get; set; }
    }
}