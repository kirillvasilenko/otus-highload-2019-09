using SocialNetwork.Model;

namespace SocialNetwork.App
{
    public interface INotificationSender
    {
        void OnUserRegistered(UserDto user);
    }
}