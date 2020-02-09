using System.Data.Common;

namespace SocialNetwork.Model
{
    public interface IDbConnectionProvider
    {
        DbConnection GetOpenedConnection();
    }
}