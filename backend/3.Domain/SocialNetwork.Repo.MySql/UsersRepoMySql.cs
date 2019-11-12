using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Security;
using SocialNetwork.Model;

namespace SocialNetwork.Repo.MySql
{
    
    public class UsersRepoMySql:IUsersRepo
    {
        private readonly string connectionString;
        private readonly ILogger<UsersRepoMySql> logger;

        public UsersRepoMySql(string connectionString, ILogger<UsersRepoMySql> logger)
        {
            this.connectionString = connectionString;
            this.logger = logger;
        }
        
        public async Task<User> Get(long id)
        {
            await using var connection = await CreateAndOpenConnection();

            const string getSql = @"
select id, email, password, name, lastname, age, city, interests 
from user
where id=@id;";
            
            var result = await connection.QuerySingleOrDefaultAsync<User>(getSql, new {id});

            if (result == null)
            {
                throw new ItemNotFoundException(id, nameof(User));
            }

            return result;
        }

        public async Task<User> GetByEmail(string email)
        {
            await using var connection = await CreateAndOpenConnection();

            const string getSql = @"
select id, email, password, name, lastname, age, city, interests 
from user
where email=@email;";
            
            var result = await connection.QuerySingleOrDefaultAsync<User>(getSql, new {email});

            if (result == null)
            {
                throw new ItemNotFoundException(email, nameof(User));
            }

            return result;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            await using var connection = await CreateAndOpenConnection();

            const string getSql = @"
select id, email, password, name, lastname, age, city, interests 
from user;";
            
            return await connection.QueryAsync<User>(getSql);
        }
        
        
        public async Task<User> Add(User user)
        {
            await using var connection = await CreateAndOpenConnection();

            const string insertSql = @"
insert into user
(email, password, name, lastname, age, city, interests)
values (@email, @password, @name, @lastname, @age, @city, @interests);

SELECT LAST_INSERT_ID();";
            
            var addedUserId = await connection.QuerySingleAsync<long>(insertSql, new
            {
                email = user.Email,
                password = user.Password,
                name = user.Name,
                lastname = user.LastName,
                age = user.Age,
                city = user.City,
                interests = user.Interests
            });
            user.Id = addedUserId;
            return user;
        }

        public Task<User> Delete(long id)
        {
            throw new NotImplementedException();
        }

        private async Task<MySqlConnection> CreateAndOpenConnection()
        {
            var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}