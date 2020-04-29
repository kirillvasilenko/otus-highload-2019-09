using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using SocialNetwork.Model;

namespace SocialNetwork.App.GcpPubSub
{
    public class NotificationSenderPubSub: INotificationSender
    {
        private readonly ILogger<NotificationSenderPubSub> logger;

        private readonly TopicName topicName;
        
        public NotificationSenderPubSub(ILogger<NotificationSenderPubSub> logger, string projectId, string topicId)
        {
            this.logger = logger;
            topicName = new TopicName(projectId, topicId);
        }
        
        public async Task OnUserRegistered(UserDto user)
        {
            var tmp = new PublisherClient.ClientCreationSettings();
            
            var publisher = await PublisherClient.CreateAsync(topicName);
            
            var body = JsonSerializer.Serialize(user);
            
            string messageId = await publisher.PublishAsync(body);

            await publisher.ShutdownAsync(TimeSpan.FromSeconds(15));
        }
    }
}