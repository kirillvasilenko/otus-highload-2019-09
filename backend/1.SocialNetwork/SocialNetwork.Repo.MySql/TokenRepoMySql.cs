using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using SocialNetwork.Model;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace SocialNetwork.Repo.MySql
{
    public class TokenRepoMySql : ITokenRepo
    {
        static TokenRepoMySql()
        {
            SqlMapper.SetTypeMap(typeof(RefreshToken), RefreshTokenMapper.GetMapper());
        }


        private readonly IDbConnectionProvider connectionProvider;
        private readonly ILogger<TokenRepoMySql> logger;


        public TokenRepoMySql(IDbConnectionProvider connectionProvider, ILogger<TokenRepoMySql> logger)
        {
            this.connectionProvider = connectionProvider;
            this.logger = logger;
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken token)
        {
            token.Id = await Query().InsertGetIdAsync<long>(new
            {
                user_id = token.UserId,
                token = token.Token,
                expiration_time = token.ExpirationTime
            });
            return token;
        }
        
        public async Task<RefreshToken> GetRefreshToken(string refreshToken)
        {
            var result = await Query()
                .SelectRaw("id, user_id, token, expiration_time")
                .Where("token", refreshToken)
                .FirstOrDefaultAsync<RefreshToken>();
            
            if (result == null)
            {
                throw new ItemNotFoundException(refreshToken, nameof(RefreshToken));
            }

            return result;
        }

        public async Task DeleteRefreshToken(long tokenId)
        {
            await Query().Where("id", tokenId).DeleteAsync();
        }

        public async Task DeleteRefreshToken(long userId, string refreshToken)
        {
            await Query()
                .Where("token", refreshToken)
                .Where("user_id", userId)
                .DeleteAsync();
        }

        public async Task DeleteAllRefreshTokens(long userId)
        {
            await Query()
                .Where("user_id", userId)
                .DeleteAsync();
        }
        
        private Query Query(string tableName = "refresh_token")
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

    }
}