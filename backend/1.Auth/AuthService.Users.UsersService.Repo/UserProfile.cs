using AutoMapper;
using UsersService.Model;

namespace AuthService.Users.UsersService.Repo
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, AuthUser>();
        }
    }
}