using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://qjgqxgdo:m_h6n4U-1ZKF9RnrBOOwbyJU91y21A14@rattlesnake.rmq.cloudamqp.com/qjgqxgdo");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "example-PubSub-Exchange", type: ExchangeType.Fanout);

string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue: queueName, exchange: "example-PubSub-Exchange", routingKey: string.Empty);


channel.BasicQos(prefetchCount: 1, prefetchSize: 0, global: false);
//Ölçeklendirme BasicQos üzerinden yapılıyor.

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queue:queueName, autoAck:false,consumer:consumer);

consumer.Received += (sender, args) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(args.Body.Span));
};