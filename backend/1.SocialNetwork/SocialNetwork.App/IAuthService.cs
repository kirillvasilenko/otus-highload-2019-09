using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.App.Dtos;
using SocialNetwork.Model;

namespace SocialNetwork.App
{
    public interface IUsersService
    {
        Task<UserDto> GetUser(long userId);

        Task<int> GetUsersCount(GetUsersQuery query);
    }

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

        public async Task<int> GetUsersCount(GetUsersQuery query)
        {
            await using var _ = await connectionController.OpenConnectionAsync();
            return await repo.GetUsersCount(query);
        }
    }
    
    public interface IAuthService
    {
        Task<TokenDto> IssueToken(string email, string password);

        Task<TokenDto> RefreshToken(string refreshToken);

        Task ResetToken(long userId, string refreshToken);

        Task ResetAllTokens(long userId);

    }
}