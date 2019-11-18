using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNetwork.AspNet.Utils
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder LoadSigningCredentialFrom(this IIdentityServerBuilder builder, string path, string password)
        {
            if (!string.IsNullOrEmpty(path))
            {
                builder.AddSigningCredential(new X509Certificate2(path, password));
            }
            else
            {
                builder.AddDeveloperSigningCredential();
            }

            return builder;
        }
    }
}