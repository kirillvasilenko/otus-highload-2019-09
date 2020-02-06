using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using IdentityModel;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace SocialNetwork.Model
{
    public class TokenMaker:ITokenMaker
    {
        private readonly ISystemClock clock;
        
        private readonly TokenMakerOptions opts;

        public TokenMaker(ISystemClock clock, IOptions<TokenMakerOptions> opts)
        {
            this.clock = clock;
            this.opts = opts.Value;
        }
        
        public TokenBound MakeToken(long userId)
        {
            var now = clock.UtcNow;
            var expirationTime = now.Add(opts.AccessTokenLifespan);
            var refreshTokenExpirationTime = now.Add(opts.RefreshTokenLifespan);
            
            var claims = new[]
            {
                new Claim(JwtClaimTypes.Subject, userId.ToString())
            };

            var signingKey = opts.GetSymmetricSecurityKey();

            var jwt = new JwtSecurityToken(
                issuer: opts.Issuer,
                audience: opts.Audience,
                notBefore: now.UtcDateTime,
                expires: expirationTime.UtcDateTime,
                claims: claims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            return new TokenBound
            {
                AccessToken = new AccessToken
                {
                    ExpirationTime = expirationTime.ToUnixTimeSeconds(),
                    Token = encodedJwt,
                    UserId = userId
                },
                RefreshToken = new RefreshToken
                {
                    ExpirationTime = refreshTokenExpirationTime.ToUnixTimeSeconds(),
                    Token = GenerateRefreshToken(),
                    UserId = userId
                }
            };
        }
        
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}