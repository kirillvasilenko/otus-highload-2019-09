using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SocialNetwork.Model;

namespace SocialNetwork.Repo.MySql
{
    public class DbConnectionControllerMySql : IDbConnectionController
    {
        private readonly string connectionString;
        private MySqlConnection connection;

        private bool disposed;

        public DbConnectionControllerMySql(string connectionString)
        {
            this.connectionString = connectionString;
        }
        
        public void Dispose()
        {
            if (disposed) return;
            connection?.Dispose();
            disposed = true;
        }

        public async ValueTask DisposeAsync()
        {
            if (disposed) return;
            if (connection != null)
            {
                await connection.DisposeAsync();    
            }
            disposed = true;
        }

        public async Task<IDbConnectionController> OpenConnectionAsync()
        {
            CheckNotDisposed();

            if (connection == null)
            {
                connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();    
            }
                
            return this;
        }

        private void CheckNotDisposed()
        {
            if(disposed) throw new ObjectDisposedException(nameof(DbConnectionControllerMySql));
        }

        public DbConnection GetOpenedConnection()
        {
            return connection;
        }
    }
}