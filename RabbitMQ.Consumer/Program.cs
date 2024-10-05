using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace RabbitMQ.Consumer
{
	internal class Program
	{

		static void Main(string[] args)
		{
			ConnectionFactory factory = new();

			factory.Uri = new("amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf");

			// Connection Open 
			using IConnection connection = factory.CreateConnection();

			// Create Channel

			using IModel channel = connection.CreateModel();


			// create queue 
			channel.QueueDeclare(queue: "example-queue", exclusive: false,durable:true);

			// Read Message

			EventingBasicConsumer consumer = new(channel);

			channel.BasicConsume(queue: "example-queue",autoAck: false, consumer);
			channel.BasicQos(0, 1, false);




			/* BasicCancel

			var consumerTag = channel.BasicConsume(queue: "example-queue", autoAck: false, consumer: consumer);
			channel.BasicCancel(consumerTag);

			*/

			consumer.Received += (sender, e) =>
			{
				Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

				// if success  message  - delete to message 
				// multiple = false (just this message )
				//channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);

				// if message failed  - return queue 
				//channel.BasicNack(deliveryTag: e.DeliveryTag, multiple: false,requeue:true);


				channel.BasicNack(deliveryTag: e.DeliveryTag, multiple: false,requeue:true);


			};
			Console.Read();
		}

	
	}
}
