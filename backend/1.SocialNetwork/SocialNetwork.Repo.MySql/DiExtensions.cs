using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SocialNetwork.Model;

namespace SocialNetwork.Repo.MySql
{
    public static class DiExtensions
    {
        public static IServiceCollection AddSocialNetworkRepoMySql(this IServiceCollection services, string connectionString)
        {
            return services
                .AddScoped(p => new DbConnectionControllerMySql(connectionString))
                .AddScoped<IDbConnectionController>(p => p.GetService<DbConnectionControllerMySql>())
                .AddScoped<IDbConnectionProvider>(p => p.GetService<DbConnectionControllerMySql>())
                .AddScoped<IUsersRepo, UsersRepoMySql>()
                .AddScoped<ITokenRepo, TokenRepoMySql>();

        }
    }
}