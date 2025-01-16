using MassTransit;
using RabbitMq.ESB.MassTransit.Shared;

string rabbitMqUri = "amqps://qjgqxgdo:m_h6n4U-1ZKF9RnrBOOwbyJU91y21A14@rattlesnake.rmq.cloudamqp.com/qjgqxgdo";

string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri);
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitMqUri}/{queueName}"));

Console.WriteLine("Gönderilecek mesaj:");
string message = Console.ReadLine();

await sendEndpoint.Send<IMessage>(new ExampleMessage() { Text = message});

//Send ise Command tabanlıdır, hangi kuyruğa mesaj göndericeğimizi biliriz.

Console.Read();