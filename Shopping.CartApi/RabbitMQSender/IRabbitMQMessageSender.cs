using Shopping.MessageBus;

namespace Shopping.CartApi.RabbitMQSender;

public interface IRabbitMQMessageSender
{
    void Send(BaseMessage message, string queueName);
}