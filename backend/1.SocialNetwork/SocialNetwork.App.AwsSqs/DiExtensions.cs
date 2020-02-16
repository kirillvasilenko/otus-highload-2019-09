using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
                    var logger = c.GetService<ILogger<NotificationSenderSqs>>();
                    return new NotificationSenderSqs(sqs, logger, queueUrl);
                });
        }
    }
}