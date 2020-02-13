namespace SocialNetwork.App.RabbitMq
{
    public class NotificationSenderRabbitMqOptions
    {
        public string HostName { get; set; } = "localhost";

        public int Port { get; set; } = 5672;
        
        public string UserName { get; set; } = "guest";

        public string Password { get; set; } = "guest";

        public string UserRegisteredQueue { get; set; } = "user_registered";
    }
}