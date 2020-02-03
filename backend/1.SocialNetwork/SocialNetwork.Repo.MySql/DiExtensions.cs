using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SocialNetwork.Model;

namespace SocialNetwork.Repo.MySql
{
    public static class DiExtensions
    {
        public static IServiceCollection AddSocialNetworkRepoMySql(this IServiceCollection services, string connectionString)
        {
            return services.AddTransient<IUsersRepo>(p =>
                new UsersRepoMySql(connectionString, p.GetService<ILogger<UsersRepoMySql>>()));

        }
    }
}