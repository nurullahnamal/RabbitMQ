using MassTransit;
using RabbitMQ.ESB.MassTransit.WorkerService.Consumer;
using RabbitMQ.ESB.MassTransit.WorkerService.Consumer.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices(services =>
	{
		services.AddMassTransit(configurator =>
	   {
		   configurator.AddConsumer<ExampleMessageConsumer>();
		   configurator.UsingRabbitMq((context, _configurator) =>
		   {
			   _configurator.Host("amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf");
		   _configurator.ReceiveEndpoint("example-message-queue", e => e.ConfigureConsumer<ExampleMessageConsumer>(context));		  
		   });
	   });

	})
	.Build();

await host.RunAsync();
