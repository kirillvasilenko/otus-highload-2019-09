using System.Threading.Tasks;
using SocialNetwork.Model;

namespace SocialNetwork.App
{
    public interface INotificationSender
    {
        Task OnUserRegistered(UserDto user);
    }
}