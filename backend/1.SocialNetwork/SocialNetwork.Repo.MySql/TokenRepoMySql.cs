using System;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SocialNetwork.Model;

namespace SocialNetwork.Repo.MySql
{
    public class TokenRepoMySql : ITokenRepo
    {
        static TokenRepoMySql()
        {
            SqlMapper.SetTypeMap(typeof(RefreshToken), RefreshTokenMapper.GetMapper());
        }
        
        private readonly string connectionString;
        private readonly ILogger<UsersRepoMySql> logger;

        
        public TokenRepoMySql(string connectionString, ILogger<UsersRepoMySql> logger)
        {
            this.connectionString = connectionString;
            this.logger = logger;
        }
        
        

        public async Task<RefreshToken> AddRefreshToken(RefreshToken token)
        {
            await using var connection = await CreateAndOpenConnection();
            
            const string insertSql = @"
insert into refresh_token
(user_id, token, expiration_time)
values (@user_id, @token, @expiration_time);

SELECT LAST_INSERT_ID();";
            
            var addedTokenId = await connection.QuerySingleAsync<long>(insertSql, new
            {
                user_id = token.UserId,
                token = token.Token,
                expiration_time = token.ExpirationTime
            });
            token.Id = addedTokenId;
            return token;
        }

        public Task<RefreshToken> GetRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task ResetRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task ResetAllRefreshTokens(long userId)
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