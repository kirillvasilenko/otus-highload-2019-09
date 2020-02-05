using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Internal;

namespace SocialNetwork.Model
{

    public class TokenRepo : ITokenRepo
    {
        private readonly ISystemClock clock;

        public TokenRepo(ISystemClock clock)
        {
            this.clock = clock;
        }
        
        public Task<TokenBound> IssueToken()
        {
            var now = clock.UtcNow.TruncateMilliseconds();
            var userId = User.Id.ToString();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(CultureInfo.InvariantCulture),
                    ClaimValueTypes.Integer64),

                new Claim(ClaimsIdentity.DefaultNameClaimType, userId),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, User.Role),
                new Claim(AuthSessionId, Id.ToString()),
            };

            var signingKey = AuthConfig.Instance.GetSymmetricSecurityKey();


            var jwt = new JwtSecurityToken(
                issuer: AuthConfig.Instance.Issuer,
                audience: AuthConfig.Instance.Audience,
                claims: claims,
                notBefore: now.UtcDateTime,
                expires: now.Add(AuthConfig.Instance.AccessTokenLifetime).UtcDateTime,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthTokenResult
            {
                AccessToken = encodedJwt,
                AccessExpireTime = now.Add(AuthConfig.Instance.AccessTokenLifetime),
                RefreshToken = refreshToken,
                RefreshExpireTime = now.Add(AuthConfig.Instance.RefreshTokenLifetime),
                UserId = userId
            };
        }

        public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken> GetRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task ResetRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task ResetAllRefreshTokens(long userId)
        {
            throw new NotImplementedException();
        }
    }
    
    public class TokenMaker
    {
        private AuthTokenResult MakeJwtToken(string refreshToken)
        {
            
        }
    }

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