using System;
using System.Threading.Tasks;
using Amursoft.PasswordHasher;
using AutoMapper;
using SocialNetwork.App.Dtos;
using SocialNetwork.Model;

namespace SocialNetwork.App
{
    public class RegistrationUsersService : IRegistrationUsersService
    {
        private readonly IUsersRepo repo;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMapper mapper;

        public RegistrationUsersService(IUsersRepo repo, IPasswordHasher passwordHasher, IMapper mapper)
        {
            this.repo = repo;
            this.passwordHasher = passwordHasher;
            this.mapper = mapper;
        }
        
        public async Task<UserDto> RegisterUser(RegisterUserData newUser)
        {
            ValidatePassword(newUser);

            var user = mapper.Map<User>(newUser);
            user.Password = passwordHasher.HashPassword(newUser.Password);
            try
            {
                user = await repo.AddUser(user);
            }
            catch (ConnectionException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UserRegistrationException(e.Message, e);
            }
            return mapper.Map<UserDto>(user);
        }

        private void ValidatePassword(RegisterUserData newUser)
        {
            if (string.IsNullOrEmpty(newUser.Password)
                || newUser.Password != newUser.RepeatedPassword)
            {
                throw new UserRegistrationException("Password must not be empty and must be equal to RepeatedPassword");
            }
        }
    }
}