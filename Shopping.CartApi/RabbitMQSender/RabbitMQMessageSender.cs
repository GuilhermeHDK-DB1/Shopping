using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Shopping.CartApi.Messages;
using Shopping.MessageBus;

namespace Shopping.CartApi.RabbitMQSender;

public class RabbitMQMessageSender : IRabbitMQMessageSender
{
    private readonly string _hostName;
    private readonly string _password;
    private readonly string _userName;
    private IConnection _connection;

    public RabbitMQMessageSender()
    {
        _hostName = "localhost";
        _password = "guest";
        _userName = "guest";
    }
    public void Send(BaseMessage message, string queueName)
    {
        var factory = new ConnectionFactory
        {
            HostName = _hostName,
            UserName = _userName,
            Password = _password
        };
        
        _connection = factory.CreateConnection();
        using var channel = _connection.CreateModel();
        
        channel.QueueDeclare(queue: queueName, false, false, false);
        byte[] body = GetMessageAsByteArray(message);
        channel.BasicPublish(
            exchange: "", 
            routingKey: queueName, 
            basicProperties: null, 
            body: body);
    }

    private byte[] GetMessageAsByteArray(BaseMessage message)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        var json = JsonSerializer.Serialize<CheckoutHeaderVo>((CheckoutHeaderVo)message, options);
        var body = Encoding.UTF8.GetBytes(json);
        return body;
    }
}