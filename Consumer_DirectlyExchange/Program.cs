using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new("amqps://qjgqxgdo:m_h6n4U-1ZKF9RnrBOOwbyJU91y21A14@rattlesnake.rmq.cloudamqp.com/qjgqxgdo");

using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();


//Publisher tarafındaki exchange ile aynı exchange tanımlanmalıdır.
channel.ExchangeDeclare(exchange: "directly-exchange", type: ExchangeType.Direct);

//Publisher tarafında oluşan kuyruğa gönderilen mesajları kendi kuyruğumuza yönlendirip tüketmek için kuruk oluşturulur.clear
string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue:queueName,
    exchange: "directly-exchange",
    routingKey: "example - cloudQueue");

EventingBasicConsumer consumer = new(channel);

consumer.Received += (sender, args) =>
{
    string message = Encoding.UTF8.GetString(args.Body.Span);
    Console.WriteLine(message);
};

channel.BasicConsume(queue:queueName,autoAck:true,consumer:consumer);

Console.Read();

