using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Messages;
using System;
using System.Threading.Tasks;

namespace RabbitMQ.ESB.MassTransit.Publisher
{
	class Program
	{
		static async Task Main(string[] args)
		{
			string rabbitMQUri = "amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf";
			string queueName = "example-queue";

			var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
			{
				factory.Host(new Uri(rabbitMQUri));
			});

			ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new Uri($"{rabbitMQUri}/{queueName}"));

			Console.WriteLine("Gönderilecek mesaj: ");
			string message = Console.ReadLine();

			await sendEndpoint.Send<IMessage>(new ExampleMessage()
			{
				Text = message
			});

			Console.ReadLine();
		}
	}
}
