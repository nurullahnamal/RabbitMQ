using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Consumer
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Create Connection
			ConnectionFactory factory = new();

			factory.Uri = new("amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf");

			// Connection Open 
			using IConnection connection = factory.CreateConnection();

			// Create Channel

			using IModel channel = connection.CreateModel();


			// create queue 
			channel.QueueDeclare(queue: "example-queue", exclusive: false);

			// Read Message

			EventingBasicConsumer consumer = new(channel);

			channel.BasicConsume(queue: "example-queue", false, consumer);

			consumer.Received += (sender, e) =>
			{
				Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
			};
			Console.Read();
		}
	}
}
