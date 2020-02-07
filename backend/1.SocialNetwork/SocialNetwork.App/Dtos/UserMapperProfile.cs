using AutoMapper;
using SocialNetwork.Model;

namespace SocialNetwork.App.Dtos
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UpdateUserData, User>();
            CreateMap<RegisterUserData, User>()
                .ForMember(x => x.Id, opts => opts.MapFrom(m => 0))
                .ForMember(x => x.EmailVerified, opts => opts.MapFrom(m => false))
                .ForMember(x => x.IsActive, opts => opts.MapFrom(m => true));
        }
    }
}