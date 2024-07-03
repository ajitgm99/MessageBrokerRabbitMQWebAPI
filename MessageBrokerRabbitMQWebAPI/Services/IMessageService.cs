namespace MessageBrokerRabbitMQWebAPI.Services
{
    public interface IMessageService
    {
        public void SendMessages<T> (T message);
    }
}
