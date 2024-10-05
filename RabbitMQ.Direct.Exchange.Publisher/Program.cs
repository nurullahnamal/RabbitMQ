using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Direct.Exchange.Publisher
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Create Connection
			ConnectionFactory factory = new();

			factory.Uri = new("amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq." +
				"cloudamqp.com/xhdvzihf");

			// Connection Open 
			using IConnection connection = factory.CreateConnection();

			// Create Channel

			using IModel channel = connection.CreateModel();

			channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

			string queueName = channel.QueueDeclare().QueueName;


			channel.QueueBind(queue: queueName,
				exchange: "direct-exchange-example",
				routingKey: "direct-queue-example");

			EventingBasicConsumer consumer = new(channel);
			channel.BasicConsume(queue: queueName, autoAck: true, consumer:consumer);

			consumer.Received += (sender, e) =>
			{
				string message = Encoding.UTF8.GetString(e.Body.Span);
				Console.WriteLine(message);
			};

			Console.Read();




		}
	}
}
