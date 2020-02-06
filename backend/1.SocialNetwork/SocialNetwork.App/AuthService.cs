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
        private readonly IUsersRepo usersRepo;
        private readonly ITokenRepo tokenRepo;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMapper mapper;

        private readonly ITokenMaker tokenMaker;

        private readonly List<UserDto> usersCache = new List<UserDto>();
        
        public AuthService(
            IUsersRepo usersRepo,
            ITokenRepo tokenRepo,
            ITokenMaker tokenMaker,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            this.usersRepo = usersRepo;
            this.tokenRepo = tokenRepo;
            this.passwordHasher = passwordHasher;
            this.mapper = mapper;

            this.tokenMaker = tokenMaker;
        }

        public async Task<TokenDto> AuthenticateUser(string email, string password)
        {
            var user = await usersRepo.GetUserByEmail(email, throwExceptionIfNotFound:false);
            if (user == null
                || passwordHasher.VerifyHashedPassword(user.Password, password) != PasswordVerificationResult.Success)
            {
                throw new AuthenticationException("Wrong email or password.");
            }

            return null;




        }

        public async Task<TokenDto> AuthenticateUser(UserDto user)
        {
            var token = tokenMaker.MakeToken(user.Id);
            token.RefreshToken = await tokenRepo.AddRefreshToken(token.RefreshToken);
            return mapper.Map<TokenDto>(token);
        }
        

        public async Task<bool> ValidateCredentials(string username, string password)
        {
            var user = await FindByUsername(username);
            if (user != null)
            {
                return passwordHasher.VerifyHashedPassword(user.Password, password) == PasswordVerificationResult.Success;
            }

            return false;
        }

        public async Task<UserDto> FindByUsername(string username)
        {
            var authUser = usersCache.FirstOrDefault(x => x.Email == username);
            if (authUser == null)
            {
                var user = await usersRepo.GetUserByEmail(username);
                authUser = mapper.Map<UserDto>(user);
                usersCache.Add(authUser);
            }
            return authUser;
        }

        public async Task<UserDto> FindById(long id)
        {
            var authUser = usersCache.FirstOrDefault(x => x.Id == id);
            if (authUser == null)
            {
                var user = await usersRepo.GetUser(id);
                authUser = mapper.Map<UserDto>(user);
                usersCache.Add(authUser);
            }
            return authUser;
        }
    }
}