using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace SocialNetwork.AspNet
{
    public static class Config
    {
        public static readonly string WebAppScopes = $"webapp.public {OidcConstants.StandardScopes.OfflineAccess}";

        public static readonly Client WebAppClient = new Client
        {
            ClientId = "web.spa",
            RequireClientSecret = false,
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

            AllowedScopes =
            {
                "webapp.public"
            },

            AllowOfflineAccess = true
        };
        
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "webapp",
                    DisplayName = "Web Application",
                    Scopes = new List<Scope>
                    {
                        new Scope("webapp.public")
                    },
                    UserClaims = new []{"role"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                WebAppClient
            };
    }
}