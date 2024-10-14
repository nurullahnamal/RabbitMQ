using MassTransit;
using RabbitMQ.ESB.MassTransit.RequestResponsePattern.Consumer.Consumers;

string rabbitMQUri = "amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf";

string requestQueue = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
	factory.Host(rabbitMQUri);

	factory.ReceiveEndpoint(requestQueue, endpoint =>
	{
		endpoint.Consumer<RequestMessageConsumer>();
	});
});


await bus.StartAsync();

Console.Read();