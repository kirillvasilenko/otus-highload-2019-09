using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;

namespace AuthService.Users
{
    /// <summary>
    /// User
    /// </summary>
    public class AuthUser
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// True, if email was verified
        /// </summary>
        public bool EmailVerified{get;set;}
        
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Name
        /// </summary>
        public string GivenName { get; set; }
        
        /// <summary>
        /// Last name
        /// </summary>
        public string FamilyName { get; set; }
        
        /// <summary>
        /// Age
        /// </summary>
        public byte Age { get; set; }
        
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// Interests
        /// </summary>
        public string Interests { get; set; }
        
        /// <summary>
        /// Gets or sets if the user is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        public ICollection<Claim> GetClaims()
                {
                    return new[]
                    {
                        new Claim(JwtClaimTypes.GivenName, GivenName ?? string.Empty),
                        new Claim(JwtClaimTypes.FamilyName, FamilyName ?? string.Empty),
                        new Claim(JwtClaimTypes.Email, Email ?? string.Empty),
                        new Claim(JwtClaimTypes.EmailVerified, EmailVerified.ToString(), ClaimValueTypes.Boolean),
                    };
                }
        
    }
}