using System;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.AspNet;
using SocialNetwork.Dtos;
using Xunit;

namespace SocialNetwork.Tests._1.Ui
{
    public class AutoMapperTests
    {
        
        [Fact]
        public void ConfigurationIsValid()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();
            Action<IMapperConfigurationExpression> mapperConfigurationChain = cnf => {};
            
            services.ConfigureDi(configuration, ref mapperConfigurationChain);
            
            var mapperConfig = new MapperConfiguration(mapperConfigurationChain);
            
            mapperConfig.AssertConfigurationIsValid();
        }   
    }
}