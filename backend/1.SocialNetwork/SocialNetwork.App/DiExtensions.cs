using Amursoft.PasswordHasher;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using SocialNetwork.Model;

namespace SocialNetwork.App
{
    public static class DiExtensions
    {
        public static IServiceCollection AddSocialNetworkApp(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(typeof(DiExtensions).Assembly)
                .AddTransient<ISystemClock, SystemClock>()
                .AddTransient<ITokenMaker, TokenMaker>()
                .AddScoped<IAuthService, AuthService>()
                .AddTransient<IRegistrationService, RegistrationService>()
                .AddPbkdf2PasswordHasher();
        }
    }
}