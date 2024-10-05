using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Publisher
{
	public class Program
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
			channel.QueueDeclare(queue: "example-queue",exclusive:false,durable:true);

			// send message Queue

			IBasicProperties properties=channel.CreateBasicProperties();    // durable : configuretion
			properties.Persistent = true;                                   //

			byte[] message = Encoding.UTF8.GetBytes("Merhaba");
			channel.BasicPublish(exchange: "", "example-queue", body: message,basicProperties:properties);

			
		}
	}
}
