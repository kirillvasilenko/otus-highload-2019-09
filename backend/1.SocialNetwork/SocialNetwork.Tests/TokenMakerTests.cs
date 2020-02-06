using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;
using SocialNetwork.Model;
using Xunit;

namespace SocialNetwork.Tests
{
    public class TokenMakerTests
    {
        private TokenMaker maker;
        
        public TokenMakerTests()
        {
            var opts = Options.Create(new TokenMakerOptions());
            maker = new TokenMaker(new SystemClock(), opts);
        }
        
        [Fact]
        public void Tmp()
        {
            var token = maker.MakeToken(300);
        }
    }
}