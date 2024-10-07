using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Topic.Exchange.Publisher
{
	internal class Program
	{
		static  void Main(string[] args)
		{
			ConnectionFactory factory = new();
			factory.Uri = new("amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf");

			IConnection connection = factory.CreateConnection();
			IModel channel = connection.CreateModel();

			channel.ExchangeDeclare(exchange: "Topic-Exchange-example",
				type: ExchangeType.Topic);

			for (int i = 0; i < 100; i++)
			{
				 Task.Delay(100);
				byte[] message = Encoding.UTF8.GetBytes($"merhaba{i}");

                Console.Write("Topic formatı giriniz");

				string topic=Console.ReadLine();
				channel.BasicPublish(
					exchange: "Topic-Exchange-example",
					routingKey: topic,
					body: message);
			}
			Console.Read();
		}
	}
}
