using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Topic.Exchange.Consumer
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ConnectionFactory factory = new();
			factory.Uri = new("amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf");

			IConnection connection = factory.CreateConnection();
			IModel channel = connection.CreateModel();

			channel.ExchangeDeclare(exchange: "Topic-Exchange-example",
				type: ExchangeType.Topic);

            Console.Write("dinlenecek topic formatını belirtiniz : ");

			string topic = Console.ReadLine();

			string queueName = channel.QueueDeclare().QueueName;

			channel.QueueBind(
				queue: queueName,
				exchange: "Topic-Exchange-example",
				routingKey: topic);

			EventingBasicConsumer consumer = new(channel);
			channel.BasicConsume(
				queue: queueName,
				autoAck: true,
				consumer);
			consumer.Received += (sender, e) =>
			{
				string message = Encoding.UTF8.GetString(e.Body.Span);
				Console.WriteLine(message);
			};
			Console.Read();
		}
	}
}
