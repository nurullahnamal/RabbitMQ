using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Message.Template.Req_Res.Consumer
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

			EventingBasicConsumer consumer = new(channel);
			channel.BasicConsume(
				
				queue:requestQueueName,
				autoAck:true,
				consumer:consumer);

			consumer.Received += (sender, e) =>
			{
				string message = Encoding.UTF8.GetString(e.Body.Span);
				Console.WriteLine(message);


				byte[] responseMessage = Encoding.UTF8.GetBytes($"işlem tamamlandı :  {message}");
				IBasicProperties properties = channel.CreateBasicProperties();


				properties.CorrelationId = e.BasicProperties.CorrelationId;
				
				channel.BasicPublish
				(
					exchange: string.Empty,
					routingKey: e.BasicProperties.ReplyTo,
					basicProperties:properties,
					body:responseMessage
					);
				
			};

			Console.Read();
		}
	}
}
