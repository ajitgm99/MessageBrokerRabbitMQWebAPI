using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MessageBrokerRabbitMQWebAPI.Services
{
    public class MessageService : IMessageService
    {
        public void SendMessages<T>(T message)
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

            channel.QueueDeclare("Registration1",false,false,false,null);

            var jsonstring=JsonSerializer.Serialize(message);

            var body=Encoding.UTF8.GetBytes(jsonstring);


            channel.BasicPublish("", "Registration1",null,body:body);
        }
    }
}
