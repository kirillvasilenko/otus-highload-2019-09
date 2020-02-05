using Amursoft.PasswordHasher;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNetwork.App
{
    public static class DiExtensions
    {
        public static IServiceCollection AddSocialNetworkApp(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(typeof(DiExtensions).Assembly)
                .AddScoped<IAuthService, AuthService>()
                .AddTransient<IRegistrationService, RegistrationService>()
                .AddPbkdf2PasswordHasher();
        }
    }
}