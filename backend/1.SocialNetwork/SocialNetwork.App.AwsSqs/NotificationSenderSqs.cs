using System.Text.Json;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using SocialNetwork.Model;

namespace SocialNetwork.App.AwsSqs
{
    public class NotificationSenderSqs: INotificationSender
    {
        private readonly IAmazonSQS sqs;

        private string queueUrl;
        
        public NotificationSenderSqs(IAmazonSQS sqs, string queueUrl)
        {
            this.sqs = sqs;
            this.queueUrl = queueUrl;
        }
        
        public async Task OnUserRegistered(UserDto user)
        {
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