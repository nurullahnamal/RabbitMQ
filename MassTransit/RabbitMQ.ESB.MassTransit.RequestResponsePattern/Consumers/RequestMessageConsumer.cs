using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.RequestResponseMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.ESB.MassTransit.RequestResponsePattern.Consumer.Consumers
{
	public class RequestMessageConsumer : IConsumer<RequestMessage>
	{
		public Task Consume(ConsumeContext<RequestMessage> context)
		{
            Console.WriteLine($"gelen mesaj : {context.Message.Text}");
			context.RespondAsync<ResponseMessage>(new() { Text = $"{context.Message.MessageNo}. response to request " });
			return Task.CompletedTask;	
		}
	}
}
