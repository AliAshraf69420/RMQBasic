using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };
 var connection = factory.CreateConnection();
 var channel = connection.CreateModel();

// Delete the existing queue if it exists

var queueArgs = new Dictionary<string, object>
{
    { "x-single-active-consumer", true } // Enable Single Active Consumer mode
};

channel.QueueDeclare(queue: "hello",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: queueArgs);
const string message = "Hello World!";
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: string.Empty,
                     routingKey: "hello",
                     basicProperties: null,
                     body: body);
Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();


