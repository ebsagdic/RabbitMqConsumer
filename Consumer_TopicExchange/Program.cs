using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://qjgqxgdo:m_h6n4U-1ZKF9RnrBOOwbyJU91y21A14@rattlesnake.rmq.cloudamqp.com/qjgqxgdo");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exchanges", type: ExchangeType.Topic);

Console.Write("Dinlenecek topic türünüe belirtiniz : ");

var topic = Console.ReadLine();

var queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queue: queueName, exchange: "topic-exchange", routingKey: topic);

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queueName, true, consumer);

consumer.Received += (sender, args) =>
{
    string message = Encoding.UTF8.GetString(args.Body.Span);
    Console.WriteLine(message);
};

Console.Read();