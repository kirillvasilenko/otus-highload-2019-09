using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Users.UsersService.Repo
{
    public static class DiExtensions
    {
        public static void AddUsersServiceRepo(this IServiceCollection services, List<Assembly> autoMapperAssemblies)
        {
            services.AddScoped<IAuthUsersService, AuthUsersServiceRepo>();
            services.AddTransient<IRegistrationUsersService, RegistrationUsersServiceRepo>();

            autoMapperAssemblies.Add(typeof(DiExtensions).Assembly);
        }
    }
}