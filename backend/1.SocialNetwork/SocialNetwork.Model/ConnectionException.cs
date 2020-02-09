using System;

namespace SocialNetwork.Model
{
    public class ConnectionException:Exception
    {
        public ConnectionException(Exception inner):base(inner.Message, inner){}
    }
}