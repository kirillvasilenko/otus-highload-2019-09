using AutoMapper;
using SocialNetwork.Model;

namespace SocialNetwork.App.Dtos
{
    public class UserDtoMapperProfile : Profile
    {
        public UserDtoMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.Password, opts => opts.MapFrom(m => "TOP_SECRET"));
        }
    }
}