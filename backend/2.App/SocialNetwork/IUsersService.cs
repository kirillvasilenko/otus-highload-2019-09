using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SocialNetwork.Model;

namespace SocialNetwork
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetUsers();
        
        Task<User> GetUser(long id);
    }
}