using System;
using System.Threading.Tasks;
using Amursoft.PasswordHasher;
using AutoMapper;
using SocialNetwork.App.Dtos;
using SocialNetwork.Model;

namespace SocialNetwork.App
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IDbConnectionController connectionController;
        private readonly ITransaction transaction;
        private readonly IUsersRepo usersRepo;
        private readonly ITokenRepo tokenRepo;
        private readonly INotificationSender notificationSender;
        private readonly IPasswordHasher passwordHasher;
        private readonly ITokenMaker tokenMaker;
        private readonly IMapper mapper;

        public RegistrationService(
            IDbConnectionController connectionController,
            ITransaction transaction,
            IUsersRepo usersRepo,
            ITokenRepo tokenRepo,
            INotificationSender notificationSender,
            IPasswordHasher passwordHasher,
            ITokenMaker tokenMaker,
            IMapper mapper)
        {
            this.connectionController = connectionController;
            this.transaction = transaction;
            this.usersRepo = usersRepo;
            this.tokenRepo = tokenRepo;
            this.passwordHasher = passwordHasher;
            this.tokenMaker = tokenMaker;
            this.mapper = mapper;
            
            this.notificationSender = notificationSender;
        }
        
        public async Task<RegistrationUserResult> RegisterUser(RegisterUserData newUser)
        {
            ValidatePassword(newUser);
            var user = newUser.ToModel(mapper, passwordHasher);
            
            await using var tmp = await connectionController.OpenConnectionAsync();
            return await transaction.Run(async () =>
            {
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

                notificationSender.OnUserRegistered(user.ToDto(mapper));
                var token = await AuthenticateUser(user.Id);
                
                return new RegistrationUserResult
                {
                    User = user.ToDto(mapper),
                    Token = token
                };
            });
        }
        
        private async Task<TokenDto> AuthenticateUser(long userId)
        {
            var token = tokenMaker.MakeToken(userId);
            await tokenRepo.AddRefreshToken(token.RefreshToken);
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