using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SocialNetwork.Repo.MySql
{
    public static class DiExtensions
    {
        public static void AddReposMySql(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IUsersRepo>(p =>
                new UsersRepoMySql(connectionString, p.GetService<ILogger<UsersRepoMySql>>()));

        }
    }
}