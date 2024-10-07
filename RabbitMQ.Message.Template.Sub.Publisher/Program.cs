using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Message.Template.Sub.Publisher
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ConnectionFactory factory = new();
			factory.Uri = new Uri("amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf");

			using IConnection connection = factory.CreateConnection();
			using IModel channel = connection.CreateModel();


			string exchangeName = "example-pub-sub-exchange";

			channel.ExchangeDeclare(
				exchange: exchangeName,
				type: ExchangeType.Fanout
				);

			for (int i = 0; i < 100; i++)
			{
				byte[] message = Encoding.UTF8.GetBytes("merhaba" + i);

				channel.BasicPublish(
					exchange: exchangeName,
					routingKey: string.Empty,
					body: message);

			}



			Console.Read();
		}
		
	}
}
