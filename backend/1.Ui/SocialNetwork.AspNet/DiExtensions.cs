using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Repo.MySql;

namespace SocialNetwork.AspNet
{
    public static class DiExtensions
    {
        public static void ConfigureDi(
            this IServiceCollection services,
            IConfiguration configuration,
            ref Action<IMapperConfigurationExpression> mapperConfigurationChain)
        {
            services.AddSocialNetworkApp(ref mapperConfigurationChain);
            services.AddReposMySql(configuration.GetConnectionString("MySql"));
            
            // Configure AutoMapper
            var mapperConfig = new MapperConfiguration(mapperConfigurationChain);
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}