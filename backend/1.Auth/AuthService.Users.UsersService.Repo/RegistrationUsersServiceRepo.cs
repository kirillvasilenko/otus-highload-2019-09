using System;
using System.Threading.Tasks;
using Amursoft.PasswordHasher;
using AuthService.Users.Dtos;
using AutoMapper;
using MySql.Data.MySqlClient;
using UsersService.Model;

namespace AuthService.Users.UsersService.Repo
{
    public class RegistrationUsersServiceRepo : IRegistrationUsersService
    {
        private readonly IUsersRepo repo;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMapper mapper;

        public RegistrationUsersServiceRepo(IUsersRepo repo, IPasswordHasher passwordHasher, IMapper mapper)
        {
            this.repo = repo;
            this.passwordHasher = passwordHasher;
            this.mapper = mapper;
        }
        
        public async Task<AuthUser> RegisterUser(RegisterUserData newUser)
        {
            ValidatePassword(newUser);

            var user = mapper.Map<User>(newUser);
            user.Password = passwordHasher.HashPassword(newUser.Password);
            try
            {
                user = await repo.Add(user);
            }
            catch (MySqlException e)
            {
                throw new UserRegistrationException(e.Message, e);
            }
            return mapper.Map<AuthUser>(user);
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