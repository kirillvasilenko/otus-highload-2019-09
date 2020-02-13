using Microsoft.Extensions.DependencyInjection;

namespace SocialNetwork.App.InMemory
{
    public static class DiExtensions
    {
        public static IServiceCollection AddSocialNetworkAppInMemory(this IServiceCollection services)
        {
            

            return services.AddScoped<INotificationSender, NotificationSenderInMemory>();
        }
    }
}