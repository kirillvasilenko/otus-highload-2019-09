using System;
using System.Collections.Generic;
using System.Reflection;
using AuthService;
using AuthService.Users.UsersService.Repo;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.AspNet.Services;

namespace SocialNetwork.AspNet.Utils
{
    public static class DiExtensions
    {
        public static void ConfigureDi(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var autoMapperAssemblies = new List<Assembly>();
            services.AddUsersServiceRepo(autoMapperAssemblies);
            
            services.AddAutoMapper(autoMapperAssemblies);
        }
        
        public static IIdentityServerBuilder AddUsers(this IIdentityServerBuilder builder)
        {
            return builder
                .AddProfileService<UserProfileService>()
                .AddResourceOwnerValidator<UserPasswordValidator>();
        }
    }
}