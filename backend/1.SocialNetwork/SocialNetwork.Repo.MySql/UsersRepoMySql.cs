using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SocialNetwork.Model;

namespace SocialNetwork.Repo.MySql
{
    public class UsersRepoMySql:IUsersRepo
    {
        static UsersRepoMySql()
        {
            SqlMapper.SetTypeMap(typeof(User), UserMapper.GetMapper());
        }
        
        private readonly string connectionString;
        private readonly ILogger<UsersRepoMySql> logger;

        public UsersRepoMySql(string connectionString, ILogger<UsersRepoMySql> logger)
        {
            this.connectionString = connectionString;
            this.logger = logger;
        }
        
        public async Task<User> GetUser(long id, bool throwExceptionIfNotFound = true)
        {
            await using var connection = await CreateAndOpenConnection();

            const string getSql = @"
select id, email, email_verified, password, given_name, family_name, age, city, interests, is_active 
from user
where id=@id;";
            
            var result = await connection.QuerySingleOrDefaultAsync<User>(getSql, new {id});

            if (result == null && throwExceptionIfNotFound)
            {
                throw new ItemNotFoundException(id, nameof(User));
            }

            return result;
        }

        public async Task<User> GetUserByEmail(string email, bool throwExceptionIfNotFound = true)
        {
            await using var connection = await CreateAndOpenConnection();

            const string getSql = @"
select id, email, email_verified, password, given_name, family_name, age, city, interests, is_active 
from user
where email=@email;";
            
            var result = await connection.QuerySingleOrDefaultAsync<User>(getSql, new {email});

            if (result == null && throwExceptionIfNotFound)
            {
                throw new ItemNotFoundException(email, nameof(User));
            }

            return result;
        }

        public async Task<int> GetUsersCount()
        {
            await using var connection = await CreateAndOpenConnection();

            const string getSql = @"
select count(*) 
from user;";
           
            return await connection.QuerySingleAsync<int>(getSql);
        }

        public async Task<IEnumerable<User>> GetUsers(int skip, int take)
        {
            await using var connection = await CreateAndOpenConnection();

            const string getSql = @"
select id, email, email_verified, password, given_name, family_name, age, city, interests, is_active 
from user
limit @skip,@take;";
           
            return await connection.QueryAsync<User>(getSql, new { skip, take });
        }
        
        
        public async Task<User> AddUser(User user)
        {
            await using var connection = await CreateAndOpenConnection();

            const string insertSql = @"
insert into user
(email, email_verified, password, given_name, family_name, age, city, interests, is_active)
values (@email, @email_verified, @password, @given_name, @family_name, @age, @city, @interests, @is_active);

SELECT LAST_INSERT_ID();";
            
            var addedUserId = await connection.QuerySingleAsync<long>(insertSql, new
            {
                email = user.Email,
                email_verified = user.EmailVerified,
                password = user.Password,
                given_name = user.GivenName,
                family_name = user.FamilyName,
                age = user.Age,
                city = user.City,
                interests = user.Interests,
                is_active = user.IsActive
            });
            user.Id = addedUserId;
            return user;
        }

        public Task<User> DeleteUser(long id)
        {
            throw new NotImplementedException();
        }

        private async Task<MySqlConnection> CreateAndOpenConnection()
        {
            try
            {
                var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception e)
            {
                throw new ConnectionException(e);
            }
        }
    }
}