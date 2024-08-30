
using System.Runtime.InteropServices;
using System.Text;
using ProductService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProductService.AsyncDataServices;

public class MessageBusSubscriber : BackgroundService
{
    private readonly IConfiguration _configuration;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;
    public IEventProcessor _eventProcessor;

    public MessageBusSubscriber(IEventProcessor eventProcessor, IConfiguration configuration)
    {
        _configuration = configuration;
        _eventProcessor = eventProcessor;
        InitializeRabbitMQ();
    }

    private void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"])
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: string.Empty);

        Console.WriteLine("🌐===> Listenting on the Message Bus...");

        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    }

    private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        Console.WriteLine("🌐===> Shutting down the rabbit mq connection");

    }

    public override void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
        base.Dispose();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            Console.WriteLine("⚡⚡===> Event Received!");
            byte[] body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            // sending message
            _eventProcessor.ProcessEvent(message);

        };

        _channel.BasicConsume(queue: _queueName,
                     autoAck: true,
                     consumer: consumer);
        return Task.CompletedTask;
    }
}
