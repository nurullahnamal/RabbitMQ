using MassTransit;
using RabbitMQ.ESB.MassTransit.WorkerService.Publisher;
using RabbitMQ.ESB.MassTransit.WorkerService.Publisher.Services;

IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices(services =>
	{
		services.AddMassTransit(configurator =>
		{
			configurator.UsingRabbitMq((context, _configurator) =>
			{
				_configurator.Host("amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf");
			});
		});
		services.AddHostedService<PublishMessageService>(provider =>
		{
			using IServiceScope scope = provider.CreateScope  ();
			IPublishEndpoint publishEndPoint= scope.ServiceProvider.GetService<IPublishEndpoint>();
			return new(publishEndPoint);
		});
	})
	.Build();

await host.RunAsync();
