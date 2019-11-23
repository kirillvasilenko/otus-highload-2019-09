using Amursoft.PasswordHasher;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using UsersService.Repo.MySql;

namespace AuthService.Users.UsersService.Repo
{
    public static class DiExtensions
    {
        public static IServiceCollection AddUsersServiceRepo(this IServiceCollection services, string userDbConnection)
        {
            return services
                .AddAutoMapper(typeof(DiExtensions).Assembly)
                .AddScoped<IAuthUsersService, AuthUsersServiceRepo>()
                .AddTransient<IRegistrationUsersService, RegistrationUsersServiceRepo>()
                .AddPbkdf2PasswordHasher()
                .AddReposMySql(userDbConnection);
        }
    }
}