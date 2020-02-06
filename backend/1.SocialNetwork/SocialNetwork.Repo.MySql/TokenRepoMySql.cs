using System;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using SocialNetwork.Model;

namespace SocialNetwork.Repo.MySql
{
    public class TokenRepoMySql : ITokenRepo
    {
        static TokenRepoMySql()
        {
            SqlMapper.SetTypeMap(typeof(RefreshToken), RefreshTokenMapper.GetMapper());
        }


        private readonly IDbConnectionProvider connectionProvider;
        private readonly ILogger<UsersRepoMySql> logger;

        
        public TokenRepoMySql(IDbConnectionProvider connectionProvider, ILogger<UsersRepoMySql> logger)
        {
            this.connectionProvider = connectionProvider;
            this.logger = logger;
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken token)
        {
            var connection = connectionProvider.GetOpenedConnection();
            
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
        
    }
}