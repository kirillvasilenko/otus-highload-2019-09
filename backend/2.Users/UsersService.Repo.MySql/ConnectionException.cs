using System;

namespace UsersService.Repo.MySql
{
    public class ConnectionException:Exception
    {
        public ConnectionException(Exception inner):base(inner.Message, inner){}
    }
}