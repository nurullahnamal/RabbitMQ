using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

namespace RabbitMQ.Message.Templates.Publisher
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ConnectionFactory factory = new();
			factory.Uri = new Uri("amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf");

			using IConnection connection = factory.CreateConnection();
			using IModel channel = connection.CreateModel();


			string queueName = "example-p2p-queue";

			channel.QueueDeclare(
				queue: queueName,
				durable: false,
				exclusive: false,
				autoDelete: false
				);



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
