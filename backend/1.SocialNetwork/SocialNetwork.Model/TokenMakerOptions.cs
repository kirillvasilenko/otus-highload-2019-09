using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SocialNetwork.Model
{
    public class TokenMakerOptions
    {

        public TimeSpan AccessTokenLifespan { get; set; } = TimeSpan.FromHours(1);
        
        public TimeSpan RefreshTokenLifespan { get; set; } = TimeSpan.FromDays(30);
        
        public string Issuer { get; set; } = "hl-socialnetwork";
        
        public string Audience { get; set; } = "hl-socialnetwork";

        /// <summary>
        /// Token encryption key. At least 16 symbols.
        /// </summary>
        public string Secret { get; set; } = "01234567890123456789";
        
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
        
    }
}