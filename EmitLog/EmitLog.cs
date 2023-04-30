using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory();

using var connectionFactory = factory.CreateConnection();
using var channel = connectionFactory.CreateModel();

channel.ExchangeDeclare(exchange: "logs"
                        ,type: ExchangeType.Fanout);

var message = GetMessage(args);
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "logs"
                    ,routingKey: string.Empty
                    ,basicProperties: null
                    ,body: body);

Console.WriteLine($" [x] Sent {message}");

static string GetMessage(string[] args)
{
    return ((args.Length > 0) ? string.Join(" ", args) : "info: ops!");
}