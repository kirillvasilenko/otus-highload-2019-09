using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersService.Model;

namespace UsersService
{
    public class UsersService:IUsersService
    {
        private readonly IUsersRepo repo;

        public UsersService(IUsersRepo repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<User>> GetUsers(int skip, int take)
        {
            return await repo.GetUsers(skip,take);
        }

        public async Task<User> GetUser(long id)
        {
            throw new InvalidOperationException("Ept");
            return await repo.Get(id);
        }
    }
}