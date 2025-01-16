using MassTransit;
using RabbitMq.ESB.MassTransit.Consumer.Consumers;
using RabbitMq.ESB.MassTransit.Shared;

string rabbitMqUri = "amqps://qjgqxgdo:m_h6n4U-1ZKF9RnrBOOwbyJU91y21A14@rattlesnake.rmq.cloudamqp.com/qjgqxgdo";

string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri);

    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<ExampleMessageConsumer>();
    });

});

await bus.StartAsync();

Console.Read();