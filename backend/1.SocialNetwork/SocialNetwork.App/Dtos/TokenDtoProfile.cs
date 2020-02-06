using AutoMapper;
using SocialNetwork.Model;

namespace SocialNetwork.App.Dtos
{
    public class TokenDtoProfile : Profile
    {
        public TokenDtoProfile()
        {
            CreateMap<TokenBound, TokenDto>()
                .ForMember(x => x.AccessToken, opts => opts.MapFrom(m => m.AccessToken.Token))
                .ForMember(x => x.AccessTokenExpiresIn, opts => opts.MapFrom(m => m.AccessToken.ExpirationTime))
                .ForMember(x => x.RefreshToken, opts => opts.MapFrom(m => m.RefreshToken.Token))
                .ForMember(x => x.RefreshTokenExpiresIn, opts => opts.MapFrom(m => m.RefreshToken.ExpirationTime));
        }
    }
}