using Amursoft.PasswordHasher;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using SocialNetwork.Model;

namespace SocialNetwork.App.RabbitMq
{
    public static class DiExtensions
    {
        public static IServiceCollection AddSocialNetworkAppRabbitMq(this IServiceCollection services, IConfigurationSection rabbitMqSection)
        {
            return services
                .Configure<NotificationSenderRabbitMqOptions>(rabbitMqSection)
                .AddScoped<INotificationSender, NotificationSenderRabbitMq>()
                .AddHostedService<NotificationHandlerService>();
        }
    }
}