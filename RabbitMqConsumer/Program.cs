using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new("amqps://qjgqxgdo:m_h6n4U-1ZKF9RnrBOOwbyJU91y21A14@rattlesnake.rmq.cloudamqp.com/qjgqxgdo");

using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare(queue: "example-cloudQueue", exclusive: false, durable: true);
var consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(queue: "example-cloudQueue",autoAck: false, consumer);
//Autoack RabbitMqda otomatik data silme ve mesaj onaylama sürecini aktifleştimek istiyosan bu
//parametre kullanılmalıdır
consumer.Received += (sender, args) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(args.Body.Span));
    //channel.BasicAck(args.DeliveryTag, multiple: false);
    //Bu taga sahip datayı işlediğini bildirirken bundan öncekilerin de işlendiğini belirtmemek
    //için multiple:false kullanılır.
};
Console.Read();