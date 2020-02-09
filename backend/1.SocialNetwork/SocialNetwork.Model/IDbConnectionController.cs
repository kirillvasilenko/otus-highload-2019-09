using System;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{
    public interface IDbConnectionController:IDbConnectionProvider, IDisposable, IAsyncDisposable
    {
        Task<IDbConnectionController> OpenConnectionAsync();
        
    }
}