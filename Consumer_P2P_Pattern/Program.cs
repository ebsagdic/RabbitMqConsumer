using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://qjgqxgdo:m_h6n4U-1ZKF9RnrBOOwbyJU91y21A14@rattlesnake.rmq.cloudamqp.com/qjgqxgdo");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare(queue: "example-P2P-queue", autoDelete: false, exclusive: false, durable: true);

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queue: "example-P2P-queue",autoAck: false, consumer:consumer);

consumer.Received += (sender, args) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(args.Body.Span));
};