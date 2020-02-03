using AutoMapper;
using SocialNetwork.Model;

namespace SocialNetwork.App.Dtos
{
    public class RegisterUserDataProfile : Profile
    {
        public RegisterUserDataProfile()
        {
            CreateMap<RegisterUserData, User>()
                .ForMember(x => x.Id, opts => opts.MapFrom(m => 0))
                .ForMember(x => x.EmailVerified, opts => opts.MapFrom(m => false))
                .ForMember(x => x.IsActive, opts => opts.MapFrom(m => true));
        }
    }
}