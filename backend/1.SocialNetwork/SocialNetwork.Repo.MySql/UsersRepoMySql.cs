using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using SocialNetwork.Model;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace SocialNetwork.Repo.MySql
{
    public class UsersRepoMySql:IUsersRepo
    {
        static UsersRepoMySql()
        {
            SqlMapper.SetTypeMap(typeof(User), UserMapper.GetMapper());
        }
        
        private readonly IDbConnectionProvider connectionProvider;
        private readonly ILogger<UsersRepoMySql> logger;

        public UsersRepoMySql(IDbConnectionProvider connectionProvider, ILogger<UsersRepoMySql> logger)
        {
            this.connectionProvider = connectionProvider;
            this.logger = logger;
        }
        
        public async Task<User> GetUser(long id, bool throwExceptionIfNotFound = true)
        {
            var connection = connectionProvider.GetOpenedConnection();

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
            var connection = connectionProvider.GetOpenedConnection();

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

        public async Task<int> GetUsersCount(GetUsersQuery queryParams)
        {
            var query = MakeGetUsersQuery(queryParams).AsCount();
            
            return await query.FirstAsync<int>();
        }

        private Query MakeGetUsersQuery(GetUsersQuery queryParams)
        {
            var query = Query("user");
            if (queryParams.City.HasValue())
            {
                query.WhereRaw("lower(city)=?", queryParams.City.ToLower());
            }
            if (queryParams.FamilyName.HasValue())
            {
                query.WhereRaw("lower(family_name)=?", queryParams.FamilyName.ToLower());
            }
            if (queryParams.GivenName.HasValue())
            {
                query.WhereRaw("lower(given_name)=?", queryParams.GivenName.ToLower());
            }
            if (queryParams.Interests.HasValue())
            {
                query.WhereContains("interests", queryParams.Interests);
            }
            if (queryParams.MinAge.HasValue)
            {
                query.Where("age", ">=", queryParams.MinAge.Value);
            }
            if (queryParams.MaxAge.HasValue)
            {
                query.Where("age", "<=", queryParams.MaxAge.Value);
            }

            return query;
        }

        private Query Query(string tableName)
        {
            var connection = connectionProvider.GetOpenedConnection();
            var db = new QueryFactory(connection, new MySqlCompiler())
            {
                Logger = LogQuery
            };
            return db.Query(tableName);
        }

        private void LogQuery(SqlResult result)
        {
            logger.LogDebug(result.Sql);
        }
        

        public async Task<IEnumerable<User>> GetUsers(int skip, int take)
        {
            var connection = connectionProvider.GetOpenedConnection();

            const string getSql = @"
select id, email, email_verified, password, given_name, family_name, age, city, interests, is_active 
from user
limit @skip,@take;";
           
            return await connection.QueryAsync<User>(getSql, new { skip, take });
        }
        
        
        public async Task<User> AddUser(User user)
        {
            var connection = connectionProvider.GetOpenedConnection();

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

        public async Task DeleteUser(long userId)
        {
            var connection = connectionProvider.GetOpenedConnection();

            const string deleteSql = @"
delete from user 
where id=@userId;";
            
            await connection.ExecuteAsync(deleteSql, new {userId});
        }

    }
}