using System;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.Dtos;
using SocialNetwork.Model;
using SocialNetwork.Repo;

namespace SocialNetwork
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUsersRepo repo;
        private readonly IMapper mapper;

        public RegistrationService(IUsersRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        
        public async Task<User> RegisterUser(RegisterUserData data)
        {
            if (string.IsNullOrWhiteSpace(data.Password)
                || data.Password != data.RepeatedPassword)
            {
                throw new InvalidOperationException("Repeated password must be equal to password.");
            }
            
            return await repo.Add(data.ToDomain(mapper));
        }
    }
}