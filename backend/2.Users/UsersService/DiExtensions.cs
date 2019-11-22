using Microsoft.Extensions.DependencyInjection;

namespace UsersService
{
    public static class DiExtensions
    {
        public static void AddUsersService(this IServiceCollection services)
        {
            services.AddTransient<IUsersService, UsersService>();

        }
    }
}