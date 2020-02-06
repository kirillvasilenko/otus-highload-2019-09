using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{
    public class Transaction:ITransaction
    {
        private readonly IDbConnectionProvider connectionProvider;
        public Transaction(IDbConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
        }
        
        public async Task<T> Run<T>(Func<Task<T>> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var connection = connectionProvider.GetOpenedConnection();
            
            await using DbTransaction transaction = await connection.BeginTransactionAsync(isolationLevel);

            var result = await action();

            await transaction.CommitAsync();

            return result;
        }
    }
}