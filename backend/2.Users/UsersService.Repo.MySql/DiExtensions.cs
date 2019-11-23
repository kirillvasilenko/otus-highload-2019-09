using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UsersService.Model;

namespace UsersService.Repo.MySql
{
    public static class DiExtensions
    {
        public static IServiceCollection AddReposMySql(this IServiceCollection services, string connectionString)
        {
            return services.AddTransient<IUsersRepo>(p =>
                new UsersRepoMySql(connectionString, p.GetService<ILogger<UsersRepoMySql>>()));

        }
    }
}