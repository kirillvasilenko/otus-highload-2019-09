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
        
        
        public TokenRepoMySql(IDbConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
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

        public async Task<RefreshToken> GetRefreshToken(string refreshToken)
        {
            var connection = connectionProvider.GetOpenedConnection();

            const string getSql = @"
select id, user_id, token, expiration_time 
from refresh_token
where token=@refreshToken;";
            
            var result = await connection.QuerySingleOrDefaultAsync<RefreshToken>(getSql, new {refreshToken});

            if (result == null)
            {
                throw new ItemNotFoundException(refreshToken, nameof(RefreshToken));
            }

            return result;
        }

        public async Task DeleteRefreshToken(long tokenId)
        {
            var connection = connectionProvider.GetOpenedConnection();

            const string deleteSql = @"
delete from refresh_token 
where id=@tokenId;";
            
            await connection.ExecuteAsync(deleteSql, new {tokenId});
        }

        public async Task DeleteRefreshToken(string refreshToken)
        {
            var connection = connectionProvider.GetOpenedConnection();

            const string deleteSql = @"
delete from refresh_token 
where token=@refreshToken;";
            
            await connection.ExecuteAsync(deleteSql, new {refreshToken});
        }

        public async Task DeleteAllRefreshTokens(long userId)
        {
            var connection = connectionProvider.GetOpenedConnection();

            const string deleteSql = @"
delete from refresh_token 
where user_id=@userId;";
            
            await connection.ExecuteAsync(deleteSql, new {userId});
        }

    }
}