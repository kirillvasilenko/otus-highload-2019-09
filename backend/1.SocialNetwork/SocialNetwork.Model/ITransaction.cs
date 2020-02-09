using System;
using System.Data;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{
    public interface ITransaction
    {
        Task<T> Run<T>(Func<Task<T>> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}