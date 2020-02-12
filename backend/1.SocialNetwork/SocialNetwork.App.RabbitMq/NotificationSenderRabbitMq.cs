using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SocialNetwork.Model;

namespace SocialNetwork.App.RabbitMq
{
    public class NotificationSenderRabbitMq:INotificationSender
    {
        private NotificationSenderRabbitMqOptions opts;
        
        public NotificationSenderRabbitMq(IOptions<NotificationSenderRabbitMqOptions> opts)
        {
            this.opts = opts.Value;
        }
        
        public void OnUserRegistered(UserDto user)
        {
            var factory = new ConnectionFactory
            {
                HostName = opts.HostName,
                Port = opts.Port,
                
                UserName = opts.UserName,
                Password = opts.Password
            };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: opts.UserRegisteredQueue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = JsonSerializer.SerializeToUtf8Bytes(user);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                    routingKey: opts.UserRegisteredQueue,
                    basicProperties: properties,
                    body: body);
            }
        }

    }
}