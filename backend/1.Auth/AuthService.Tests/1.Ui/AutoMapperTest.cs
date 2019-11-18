using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.AspNet.Utils;
using Xunit;

namespace AuthService.Tests._1.Ui
{
    public class AutoMapperTests
    {
        
        [Fact]
        public void ConfigurationIsValid()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();
            
            services.ConfigureDi(configuration);
            var provider = services.BuildServiceProvider();
            var mapper = provider.GetService<IMapper>();
            
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }   
    }
}