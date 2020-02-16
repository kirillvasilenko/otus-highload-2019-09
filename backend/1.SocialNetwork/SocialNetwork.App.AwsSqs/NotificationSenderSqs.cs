using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using SocialNetwork.Model;

namespace SocialNetwork.App.AwsSqs
{
    public class NotificationSenderSqs: INotificationSender
    {
        private readonly IAmazonSQS sqs;
        private readonly ILogger<NotificationSenderSqs> logger;

        private string queueUrl;
        
        public NotificationSenderSqs(IAmazonSQS sqs, ILogger<NotificationSenderSqs> logger, string queueUrl)
        {
            this.sqs = sqs;
            this.logger = logger;
            this.queueUrl = queueUrl;
        }
        
        public async Task OnUserRegistered(UserDto user)
        {
            var str = JsonSerializer.Serialize(sqs.Config, new JsonSerializerOptions(){WriteIndented = true});
            var tmp = sqs.Config.DetermineServiceURL();
            logger.LogInformation(str);
            logger.LogInformation(tmp);    
            logger.LogInformation(queueUrl);
            var body = JsonSerializer.Serialize(user);
            var request = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = body
            };
            var response = await sqs.SendMessageAsync(request);
        }
    }
}