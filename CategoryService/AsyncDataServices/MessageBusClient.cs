using System.Text;
using System.Text.Json;
using CategoryService.DTOS;
using RabbitMQ.Client;

namespace CategoryService.AsyncDataServices;

public class MessageBusClient : IMessageBusClient
{

    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBusClient(IConfiguration configuration)
    {
        _configuration = configuration;
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"])
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    }

    public void PublishNewCategory(CategoryPublishedDTO category)
    {
        var message = JsonSerializer.Serialize(category);
        if (_connection.IsOpen == false)
        {
            Console.WriteLine("===> RabbitMQ connectionis closed, not sending");
            return;
        }
        Console.WriteLine("===> RabbitMQ Connection Open, sending message...");
        SendMessage(message);
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "trigger",
         routingKey: "",
         basicProperties: null,
         body: body
        );

        Console.WriteLine($"===> We have sent {message}");
    }
    public void Dispose()
    {
        Console.WriteLine("MessageBus Disposed");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }

    private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        Console.WriteLine("====> RabbitMQ Connection Shutdown");
    }
}
