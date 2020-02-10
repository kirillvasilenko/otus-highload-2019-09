using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.App.Dtos;
using SocialNetwork.Model;

namespace SocialNetwork.App
{
    public class UsersService:IUsersService
    {
        private readonly IDbConnectionController connectionController;
        private readonly IUsersRepo repo;
        private readonly IMapper mapper;

        public UsersService(IDbConnectionController connectionController, IUsersRepo repo, IMapper mapper)
        {
            this.connectionController = connectionController;
            this.repo = repo;
            this.mapper = mapper;
        }
        public async Task<UserDto> GetUser(long userId)
        {
            await using var _ = await connectionController.OpenConnectionAsync();
            return (await repo.GetUser(userId)).ToDto(mapper);
        }

        public async Task<UserDto> UpdateUser(long userId, UpdateUserData userData)
        {
            await using var tmp = await connectionController.OpenConnectionAsync();

            var user = await repo.GetUser(userId);

            if (user.Email != userData.Email)
            {
                user.EmailVerified = false;
            }
            
            userData.MapTo(user, mapper);

            user = await repo.UpdateUser(user);

            return user.ToDto(mapper);
        }

        public async Task<int> GetUsersCount(GetUsersQuery query)
        {
            await using var _ = await connectionController.OpenConnectionAsync();
            return await repo.GetUsersCount(query);
        }

        public async Task<IEnumerable<UserDto>> GetUsers(GetUsersQuery query, int skip, int take)
        {
            await using var _ = await connectionController.OpenConnectionAsync();
            return (await repo.GetUsers(query, skip, take))
                .Select(x => x.ToDto(mapper));
        }
    }
}