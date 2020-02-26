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
            var result = await Query()
                .SelectRaw("id, email, email_verified, password, given_name, family_name, age, city, interests, is_active")
                .Where("id", id)
                .FirstOrDefaultAsync<User>();
            
            if (result == null && throwExceptionIfNotFound)
            {
                throw new ItemNotFoundException(id, nameof(User));
            }

            return result;
        }

        public async Task<User> GetUserByEmail(string email, bool throwExceptionIfNotFound = true)
        {
            var result = await Query()
                .SelectRaw("id, email, email_verified, password, given_name, family_name, age, city, interests, is_active")
                .Where("email", email)
                .FirstOrDefaultAsync<User>();
            
            if (result == null && throwExceptionIfNotFound)
            {
                throw new ItemNotFoundException(email, nameof(User));
            }

            return result;
        }

        public async Task<int> GetUsersCount(GetUsersQuery queryParams)
        {
            return await MakeGetUsersQuery(queryParams)
                .AsCount()
                .FirstAsync<int>();
        }

        public async Task<IEnumerable<User>> GetUsers(GetUsersQuery queryParams, int skip, int take)
        {
            var query = MakeGetUsersQuery(queryParams);
            if (!string.IsNullOrEmpty(queryParams.Name))
            {
                query.OrderByDesc("score");
            }
            return await query    
                .OrderBy("id")
                .Skip(skip)
                .Take(take)
                .GetAsync<User>();
        }
        
        
        public async Task<User> AddUser(User user)
        {
            user.Id = await Query().InsertGetIdAsync<long>(new
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
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            await Query().Where("id", user.Id).UpdateAsync(new
            {
                email = user.Email,
                given_name = user.GivenName,
                family_name = user.FamilyName,
                age = user.Age,
                city = user.City,
                interests = user.Interests
            });
            return user;
        }

        public async Task DeleteUser(long userId)
        {
            await Query().Where("id", userId).DeleteAsync();
        }
        
        #region Private
        
        private Query MakeGetUsersQuery(GetUsersQuery queryParams)
        {
            var query = Query().Select("*");
            if (queryParams.City.HasValue())
            {
                query.WhereRaw("lower(city)=?", queryParams.City.ToLower());
            }
            if (queryParams.Name.HasValue())
            {
                var name = queryParams.Name.Trim();
                query.WhereRaw("MATCH(family_name,given_name) AGAINST (?)", name);
                query.SelectRaw("MATCH (family_name,given_name) AGAINST(?) AS score", name);
                
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

        private Query Query(string tableName = "user")
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
        
        #endregion 

    }
}