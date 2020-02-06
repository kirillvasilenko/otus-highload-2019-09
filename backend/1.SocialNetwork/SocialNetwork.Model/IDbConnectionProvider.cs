using System.Data;

namespace SocialNetwork.Model
{
    public interface IDbConnectionProvider
    {
        IDbConnection GetOpenedConnection();
    }
}