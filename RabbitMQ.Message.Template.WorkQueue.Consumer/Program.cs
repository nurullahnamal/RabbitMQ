using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Message.Template.WorkQueue.Consumer
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

			EventingBasicConsumer consumer = new(channel);

			channel.BasicConsume(
				queue:queueName,
				autoAck:true,
				consumer:consumer);

			channel.BasicQos(
				prefetchCount:1,
				prefetchSize:0,
				global:false
				);

			consumer.Received += (sender, e) =>
			{
                Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
			};
			Console.Read();
		}
	}
}
