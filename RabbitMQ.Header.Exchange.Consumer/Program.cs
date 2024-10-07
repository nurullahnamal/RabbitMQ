using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Header.Exchange.Consumer
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ConnectionFactory factory = new();
			factory.Uri = new Uri("amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf");
			using IConnection connection = factory.CreateConnection();
			using IModel channel = connection.CreateModel();

			channel.ExchangeDeclare(
				exchange: "header-exchange-example",
				type: ExchangeType.Headers);

			Console.WriteLine("lütfen header value giriniz");
			string value = Console.ReadLine();
			string queueName = channel.QueueDeclare().QueueName;

			channel.QueueBind(
				queue: queueName,
				exchange: "header-exchange-example",
				routingKey: string.Empty,
				new Dictionary<string, object>
				{
					["no"] = value
				}
				);

			EventingBasicConsumer consumer = new(channel);

			channel.BasicConsume(
				queue:queueName,
				autoAck:true,
				consumer:consumer
				);
			consumer.Received += (sender, e) =>
			{
				string message = Encoding.UTF8.GetString(e.Body.Span);
                Console.WriteLine(message);
			};
		}
	}
}
