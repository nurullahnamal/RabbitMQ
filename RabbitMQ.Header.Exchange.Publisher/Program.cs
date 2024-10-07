using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Header.Exchange.Publisher
{
	internal class Program
	{
		static  void Main(string[] args)
		{
			ConnectionFactory factory = new ();
			factory.Uri = new Uri("amqps://xhdvzihf:km496JVr3_W5KgaWbdxr9PKB3lJ2ULpD@shrimp.rmq.cloudamqp.com/xhdvzihf");
			using IConnection connection = factory.CreateConnection();
			using IModel channel = connection.CreateModel();

			channel.ExchangeDeclare(
				exchange:"header-exchange-example",
				type:ExchangeType.Headers);

			for (int i = 0; i < 10; i++) { 
				Task.Delay(100);
				byte[] message = Encoding.UTF8.GetBytes($"Mesaj {i}");
                Console.WriteLine(	"edaer value giriniz");
				string value = Console.ReadLine();
				IBasicProperties basicProperties =channel.CreateBasicProperties();
				basicProperties.Headers=new Dictionary<string, object>
				{
					["no"]=value
				};

				channel.BasicPublish(
					exchange: "header-exchange-example",
					routingKey:string.Empty,
					body:message,
					basicProperties:basicProperties);
			}
			Console.Read();
		}

	}
}
