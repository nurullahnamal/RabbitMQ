using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Message.Template.WorkQueue.Publisher
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ConnectionFactory factory = new();
			factory.Uri = new Uri("amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf");

			using IConnection connection = factory.CreateConnection();
			using IModel channel = connection.CreateModel();

			string queueName = "example-work-queue";

			channel.QueueDeclare(
			queue: queueName,
			durable: false,
			exclusive: false,
			autoDelete: false);

			for (int i = 0; i < 1000; i++)
			{
				Task.Delay(100);

				byte[] message = Encoding.UTF8.GetBytes("Merhaba");

				channel.BasicPublish(
				   exchange: string.Empty,
				   routingKey: queueName,
				   body: message
					);

				Console.Read();


			}
		}
	}
}
