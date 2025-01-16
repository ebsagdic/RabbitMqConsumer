using MassTransit;
using RabbitMq.ESB.MassTransit.WorkerService.Consumer;
using RabbitMq.ESB.MassTransit.WorkerService.Consumer.Consumers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<ExampleMessageConsumer>();

    configurator.UsingRabbitMq((context, _configurator) =>
    {
        _configurator.Host("amqps://qjgqxgdo:m_h6n4U-1ZKF9RnrBOOwbyJU91y21A14@rattlesnake.rmq.cloudamqp.com/qjgqxgdo");

        _configurator.ReceiveEndpoint("example-message-queue", e =>
        {
            e.ConfigureConsumer<ExampleMessageConsumer>(context);
        });
    });
});


var host = builder.Build();
await host.RunAsync();
