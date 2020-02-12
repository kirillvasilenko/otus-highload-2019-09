using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SocialNetwork.Model;

namespace SocialNetwork.App.RabbitMq
{
	public class NotificationHandlerService : IHostedService, IDisposable
	{
		private readonly NotificationSenderRabbitMqOptions opts;
		private readonly ILogger logger;

		private readonly CancellationTokenSource cts;
		private Task workingTask;

		public NotificationHandlerService(
			ILogger<NotificationHandlerService> logger,
			IOptions<NotificationSenderRabbitMqOptions> opts)
		{
			this.logger = logger;
			this.opts = opts.Value;
			cts = new CancellationTokenSource();
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			logger.LogInformation("NotificationHandlerService is starting...");

			workingTask = Task.Factory.StartNew(DoWork, cancellationToken);

			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			logger.LogInformation("NotificationHandlerService is stopping...");

			StopWork();

			return Task.CompletedTask;
		}

		public void Dispose()
		{
			// На всякий случай, StopAsync может не вызваться.
			StopWork();
			cts.Dispose();
		}

		private async Task DoWork()
		{
			logger.LogInformation("NotificationHandlerService is started.");
			while (true)
			{
				try
				{
					cts.Token.ThrowIfCancellationRequested();

					await StartHandlingNotification();
				}
				catch (OperationCanceledException)
				{
					break;
				}
				catch (Exception ex)
				{
					logger.LogError(ex, $"Background Service error occured. {ex.Message}" );
					await Task.Delay(1000);
				}
			}
			logger.LogInformation("NotificationHandlerService is stopped.");
		}

		private async Task StartHandlingNotification()
		{
			var factory = new ConnectionFactory
			{
				HostName = opts.HostName,
				Port = opts.Port,
                UserName = opts.UserName,
				Password = opts.Password,
				
				DispatchConsumersAsync = true
			};
			using(var connection = factory.CreateConnection())
			using(var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: opts.UserRegisteredQueue,
					durable: true,
					exclusive: false,
					autoDelete: false,
					arguments: null);

				channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

				logger.LogDebug("[*] Waiting for messages.");

				var consumer = new AsyncEventingBasicConsumer(channel);

				consumer.Received += HandleMessage;
				
				channel.BasicConsume(queue: opts.UserRegisteredQueue,
					autoAck: false,
					consumer: consumer);

				await Task.Delay(TimeSpan.FromMinutes(10), cts.Token);

			}
		}

		private async Task HandleMessage(object model, BasicDeliverEventArgs args)
		{
			var channel = ((AsyncEventingBasicConsumer) model).Model;
			try
			{
				var body = args.Body;
				var message = Encoding.UTF8.GetString(body);
				JsonSerializer.Deserialize<UserDto>(message);
					
				logger.LogDebug("[x] Received {0}", message);
				
				await Task.Delay(1);
					
				logger.LogDebug("[x] Done");

				channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
			}
			catch (Exception e)
			{
				logger.LogError(e, e.Message);
				channel.BasicNack(deliveryTag: args.DeliveryTag, multiple: false, true);
			}
		}
		
		private void StopWork()
		{
			cts.Cancel();
			try
			{
				workingTask?.Wait();
			}
			catch (OperationCanceledException)
			{
			}
			catch (Exception e)
			{
				logger.LogError(e, e.Message);
			}
		}
	}
}