using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNetwork.App.AwsSqs
{
    public static class DiExtensions
    {
        public static IServiceCollection AddSocialNetworkAppSqs(this IServiceCollection services, IConfigurationSection sqsConfig)
        {
            var queueUrl = sqsConfig["QueueUrl"];
            var opts = sqsConfig.GetAWSOptions("");

            return services.AddDefaultAWSOptions(opts)
                .AddAWSService<IAmazonSQS>()
                .AddScoped<INotificationSender>(c =>
                {
                    var sqs = c.GetService<IAmazonSQS>();
                    return new NotificationSenderSqs(sqs, queueUrl);
                });
        }
    }
}