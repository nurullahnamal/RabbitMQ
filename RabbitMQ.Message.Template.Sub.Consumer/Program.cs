using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Message.Template.Sub.Consumer
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
			string queueName = channel.QueueDeclare().QueueName;


			channel.QueueBind(
				queue: queueName,
				exchange:exchangeName,
				routingKey:string.Empty);
			EventingBasicConsumer consumer = new(channel);


			channel.BasicConsume(
				queue: queueName,
				autoAck: false,
				consumer: consumer);

			channel.BasicQos(
				prefetchCount:1,
				prefetchSize:0,
				global:false);



			channel.BasicConsume(queueName, false, consumer);

			consumer.Received += async (sender, e) =>
			{
				Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
			};
			Console.Read(); 
		}
		

	}
}
