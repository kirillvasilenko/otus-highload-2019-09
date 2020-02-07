using System.Collections.Generic;
using System.Threading.Tasks;
using SocialNetwork.App.Dtos;
using SocialNetwork.Model;

namespace SocialNetwork.App
{
    public interface IUsersService
    {
        Task<UserDto> GetUser(long userId);

        Task<UserDto> UpdateUser(long userId, UpdateUserData userData);

        Task<int> GetUsersCount(GetUsersQuery query);
        
        Task<IEnumerable<User>> GetUsers(GetUsersQuery query, int skip, int take);

    }
}