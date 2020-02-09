using Amursoft.PasswordHasher;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using SocialNetwork.Model;

namespace SocialNetwork.App
{
    public static class DiExtensions
    {
        public static IServiceCollection AddSocialNetworkApp(this IServiceCollection services, IConfigurationSection authSection)
        {
            return services
                .AddAutoMapper(typeof(DiExtensions).Assembly)
                .AddScoped<ISystemClock, SystemClock>()
                .Configure<TokenMakerOptions>(authSection)
                .AddScoped<ITokenMaker, TokenMaker>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IUsersService, UsersService>()
                .AddScoped<IRegistrationService, RegistrationService>()
                .AddPbkdf2PasswordHasher();
        }
    }
}