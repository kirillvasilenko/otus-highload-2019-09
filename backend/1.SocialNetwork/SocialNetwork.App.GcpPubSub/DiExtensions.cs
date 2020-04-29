using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SocialNetwork.App.GcpPubSub
{
    public static class DiExtensions
    {
        public static IServiceCollection AddSocialNetworkAppPubSub(this IServiceCollection services, IConfigurationSection pubSubConfig)
        {
            var projectId = pubSubConfig["ProjectId"];
            var topicId = pubSubConfig["TopicId"];
            
            return services
                .AddScoped<INotificationSender>(c =>
                {
                    var logger = c.GetService<ILogger<NotificationSenderPubSub>>();
                    return new NotificationSenderPubSub(logger, projectId, topicId);
                });
        }
    }
}