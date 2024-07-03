using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

namespace MessageBrokerRabbitMQWebAPI.Consumer_Code
{
    public class Consumer
    {
        public void ReadMessages<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password",
                VirtualHost = "/"
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare("Registration1", false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, args) =>
            {
                var body = args.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
        }
    }
}
