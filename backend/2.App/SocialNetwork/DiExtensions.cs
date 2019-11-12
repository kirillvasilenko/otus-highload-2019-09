using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Dtos;

namespace SocialNetwork
{
    public static class DiExtensions
    {
        public static void AddSocialNetworkApp(this IServiceCollection services, ref Action<IMapperConfigurationExpression> mapperConfigurationChain)
        {
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<IAuthService, AuthService>();

            mapperConfigurationChain += RegisterUserData.ConfigureMapper;
        }
    }
}