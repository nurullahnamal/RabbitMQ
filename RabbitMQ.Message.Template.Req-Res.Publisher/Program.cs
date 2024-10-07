using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Message.Template.Req_Res.Publisher
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ConnectionFactory factory = new();
			factory.Uri = new Uri("amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf");
			using IConnection connection = factory.CreateConnection();
			using IModel channel = connection.CreateModel();

			string requestQueueName = "example-request-response-queue";

			channel.QueueDeclare(
				queue: requestQueueName,
				durable: false,
				exclusive: false,
				autoDelete: false);

			string replyQueueName = channel.QueueDeclare().QueueName;

			string correlationId = Guid.NewGuid().ToString();

			IBasicProperties properties = channel.CreateBasicProperties();
			properties.CorrelationId = correlationId;
			properties.ReplyTo = replyQueueName;

			for (int i = 0; i < 55; i++)
			{
				byte[] message = Encoding.UTF8.GetBytes("Mesaj " + i);
				channel.BasicPublish(
					exchange: string.Empty,
					routingKey: requestQueueName,
					body: message,
					basicProperties: properties
					);

				EventingBasicConsumer consumer = new(channel);

				channel.BasicConsume(
					queue: replyQueueName,
					autoAck: true,
					consumer: consumer);

				consumer.Received += (sender, e) =>
				{

					if (e.BasicProperties.CorrelationId == correlationId)
					{
						Console.WriteLine($"response :  +{Encoding.UTF8.GetString(e.Body.Span)}");

					};
					Console.Read();
				};
		}
		}
		}
	}

