using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://qjgqxgdo:m_h6n4U-1ZKF9RnrBOOwbyJU91y21A14@rattlesnake.rmq.cloudamqp.com/qjgqxgdo");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "fanout-exchange", type: ExchangeType.Fanout);

Console.WriteLine("Kuyruk adı girin: ");
string queueName = Console.ReadLine();

channel.QueueDeclare(queueName,exclusive:false);

channel.QueueBind(queue: queueName,
    exchange: "fanout-exchange",
    routingKey: String.Empty);

EventingBasicConsumer consumer = new(channel);


channel.BasicConsume(queue: queueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, args) =>
{
    string message = Encoding.UTF8.GetString(args.Body.Span);
    Console.WriteLine(message); 
};