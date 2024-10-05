using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Direct.Exchange.Consumer
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

			// create queue 
			channel.ExchangeDeclare(exchange: "direct-exchange-example", type:ExchangeType.Direct);

			// send message Queue

			while (true) {

                Console.WriteLine("Mesaj :");

				string message =Console.ReadLine();
				byte[] byteMessage=Encoding.UTF8.GetBytes(message);

				channel.BasicPublish(
					exchange: "direct-exchange-example",
					routingKey: "direct-queue-example",
					body: byteMessage);
			}
			Console.Read();
		}
	}
}
