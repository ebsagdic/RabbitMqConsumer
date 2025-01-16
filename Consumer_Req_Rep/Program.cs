using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://qjgqxgdo:m_h6n4U-1ZKF9RnrBOOwbyJU91y21A14@rattlesnake.rmq.cloudamqp.com/qjgqxgdo");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string requestQueueName = "example-req-rep-que";
channel.QueueDeclare(queue:requestQueueName,durable:false,exclusive:false,autoDelete:false);
// Durable =Sunucu yeniden başlatıldığında kuyruk korunsun mu?
// AutoDelete = Tüketiciler ayrıldığında kuyruk silinir mi?
//exclusive: true kullanıldığında, kuyruk yalnızca oluşturulduğu bağlantı tarafından kullanılabilir.Exclusive bir kuyruk, bağlantı kapandığında otomatik olarak silinir.
EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queue:requestQueueName,autoAck:false,consumer:consumer);

consumer.Received += (sender, args) =>
{
    string message = Encoding.UTF8.GetString(args.Body.Span);
    Console.WriteLine(message);

    byte[] responseMessage = Encoding.UTF8.GetBytes($"İşlem tamamlandı : {message}");
    IBasicProperties properties = channel.CreateBasicProperties();
    properties.CorrelationId = args.BasicProperties.CorrelationId;
    channel.BasicPublish(exchange: string.Empty, routingKey: args.BasicProperties.ReplyTo, basicProperties: properties, body: responseMessage);
};

Console.Read();