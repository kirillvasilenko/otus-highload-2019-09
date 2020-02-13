using System.Text.Json;
using System.Threading.Tasks;
using SocialNetwork.Model;

namespace SocialNetwork.App.InMemory
{
    public class NotificationSenderInMemory: INotificationSender
    {
        
        public Task OnUserRegistered(UserDto user)
        {
            return Task.CompletedTask;
        }
    }
}