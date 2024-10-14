// See https://aka.ms/new-console-template for more information


using MassTransit;
using RabbitMQ.ESB.MassTransit.Consumer.Consumers;

string rabbitMQUri = "amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf";
string queueName = "example-queue";
IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
	factory.Host(rabbitMQUri);

	factory.ReceiveEndpoint(queueName, endpoint =>
	{
		endpoint.Consumer<ExampleMessageConsumer>();
	});
});
await bus.StartAsync();
Console.Read();
