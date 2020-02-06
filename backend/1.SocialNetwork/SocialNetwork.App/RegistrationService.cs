using System;
using System.Threading.Tasks;
using Amursoft.PasswordHasher;
using AutoMapper;
using SocialNetwork.App.Dtos;
using SocialNetwork.Model;

namespace SocialNetwork.App
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
        
        public static User ToModel(this RegisterUserData userData, IMapper mapper)
        {
            return mapper.Map<User>(userData);
        }
    }
    
    public class RegistrationService : IRegistrationService
    {
        private readonly IUsersRepo usersRepo;
        private readonly ITokenRepo tokenRepo;
        private readonly IPasswordHasher passwordHasher;
        private readonly ITokenMaker tokenMaker;
        private readonly IMapper mapper;

        public RegistrationService(
            IUsersRepo usersRepo,
            ITokenRepo tokenRepo,
            IPasswordHasher passwordHasher,
            ITokenMaker tokenMaker,
            IMapper mapper)
        {
            this.usersRepo = usersRepo;
            this.tokenRepo = tokenRepo;
            this.passwordHasher = passwordHasher;
            this.tokenMaker = tokenMaker;
            this.mapper = mapper;
        }
        
        public async Task<RegistrationUserResult> RegisterUser(RegisterUserData newUser)
        {
            ValidatePassword(newUser);

            var user = newUser.ToModel(mapper);
            user.Password = passwordHasher.HashPassword(newUser.Password);
            try
            {
                user = await usersRepo.AddUser(user);
            }
            catch (ConnectionException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UserRegistrationException(e.Message, e);
            }

            var token = await AuthenticateUser(user.Id);
            
            return new RegistrationUserResult
            {
                User = user.ToDto(mapper),
                Token = token
            };
        }
        
        private async Task<TokenDto> AuthenticateUser(long userId)
        {
            var token = tokenMaker.MakeToken(userId);
            token.RefreshToken = await tokenRepo.AddRefreshToken(token.RefreshToken);
            return token.ToDto(mapper);
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