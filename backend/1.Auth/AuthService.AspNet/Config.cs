using System.Collections.Generic;
using IdentityServer4.Models;

namespace SocialNetwork.AspNet
{
    public static class Config
    {
        
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "users",
                    DisplayName = "Users API",
                    Scopes = new List<Scope>
                    {
                        new Scope("users.read"),
                        new Scope("users.write"),
                        new Scope("users.admin"),
                    },
                    UserClaims = new []{"role"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "web.spa",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    AllowedScopes =
                    {
                        "users.read",
                        "users.write"
                    },

                    AllowOfflineAccess = true
                }
            };
    }
}