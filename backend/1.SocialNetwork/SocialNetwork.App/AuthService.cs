using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amursoft.PasswordHasher;
using AutoMapper;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;
using SocialNetwork.App.Dtos;
using SocialNetwork.Model;

namespace SocialNetwork.App
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message):base(message){}
    }
    
    public class AuthService:IAuthService
    {
        private readonly IDbConnectionController connectionController;
        private readonly ITransaction transaction;
        private readonly IUsersRepo usersRepo;
        private readonly ITokenRepo tokenRepo;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMapper mapper;

        private readonly ITokenMaker tokenMaker;

        
        public AuthService(
            IDbConnectionController connectionController,
            ITransaction transaction,
            IUsersRepo usersRepo,
            ITokenRepo tokenRepo,
            ITokenMaker tokenMaker,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            this.connectionController = connectionController;
            this.transaction = transaction;
            this.usersRepo = usersRepo;
            this.tokenRepo = tokenRepo;
            this.passwordHasher = passwordHasher;
            this.mapper = mapper;

            this.tokenMaker = tokenMaker;
        }

        public async Task<TokenDto> IssueToken(string email, string password)
        {
            await using var _ = await connectionController.OpenConnectionAsync();
            
            var user = await usersRepo.GetUserByEmail(email, throwExceptionIfNotFound:false);
            if (user == null
                || passwordHasher.VerifyHashedPassword(user.Password, password) != PasswordVerificationResult.Success)
            {
                throw new AuthenticationException("Wrong email or password.");
            }

            return await IssueTokenImpl(user.Id);
        }

        public async Task<TokenDto> RefreshToken(string refreshToken)
        {
            await using var _ = await connectionController.OpenConnectionAsync();
            
            return await transaction.Run(async () =>
            {
                var refreshTokenToDelete = await tokenRepo.GetRefreshToken(refreshToken);
                await tokenRepo.DeleteRefreshToken(refreshTokenToDelete.Id);
                return await IssueTokenImpl(refreshTokenToDelete.UserId);
            });
        }

        public async Task ResetToken(long userId, string refreshToken)
        {
            await using var _ = await connectionController.OpenConnectionAsync();
            
            await tokenRepo.DeleteRefreshToken(userId, refreshToken);
        }

        public async Task ResetAllTokens(long userId)
        {
            await using var _ = await connectionController.OpenConnectionAsync();
            
            await tokenRepo.DeleteAllRefreshTokens(userId);
        }
        
        private async Task<TokenDto> IssueTokenImpl(long userId)
        {
            var token = tokenMaker.MakeToken(userId);
            await tokenRepo.AddRefreshToken(token.RefreshToken);
            return token.ToDto(mapper);
        }
    }
}