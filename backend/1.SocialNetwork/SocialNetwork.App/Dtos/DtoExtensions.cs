using Amursoft.PasswordHasher;
using AutoMapper;
using SocialNetwork.Model;

namespace SocialNetwork.App.Dtos
{
    public static class DtoExtensions
    {
        public static UserDto ToDto(this User user, IMapper mapper)
        {
            return mapper.Map<UserDto>(user);
        }
        
        public static TokenDto ToDto(this TokenBound token, IMapper mapper)
        {
            return mapper.Map<TokenDto>(token);
        }
        
        public static User ToModel(this RegisterUserData userData, IMapper mapper, IPasswordHasher passwordHasher)
        {
            var user = mapper.Map<User>(userData);
            user.Password = passwordHasher.HashPassword(userData.Password);
            return user;
        }
        
        public static User MapTo(this UpdateUserData source, User target, IMapper mapper)
        {
            mapper.Map(source, target);
            return target;
        }
    }
}