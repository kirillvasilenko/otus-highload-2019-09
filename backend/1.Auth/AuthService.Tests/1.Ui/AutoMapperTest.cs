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
            var provider = new ServiceCollection()
                .AddAll(string.Empty)
                .BuildServiceProvider();
            
            var mapper = provider.GetService<IMapper>();
            
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }   
    }
}