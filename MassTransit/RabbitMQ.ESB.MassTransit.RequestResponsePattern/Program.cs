
using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.RequestResponseMessages;

string rabbitMQUri = "amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf";

string requestQueue = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
	factory.Host(rabbitMQUri);
});

await bus.StartAsync();

var request = bus.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMQUri}/{requestQueue}"));
int i = 1;

while (true)
{
	await Task.Delay(100);

	var response = await request.GetResponse<ResponseMessage>(new() { MessageNo = i, Text = $"{++i}.request" });

    Console.WriteLine($"response recived :  {response.Message.Text}");
}
Console.Read();