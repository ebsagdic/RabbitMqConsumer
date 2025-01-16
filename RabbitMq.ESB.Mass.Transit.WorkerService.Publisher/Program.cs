using RabbitMq.ESB.Mass.Transit.WorkerService.Publisher;
using MassTransit;
using RabbitMq.ESB.Mass.Transit.WorkerService.Publisher.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddMassTransit(configurator =>
    configurator.UsingRabbitMq((context, _configurator) =>
    {
        _configurator.Host("amqps://qjgqxgdo:m_h6n4U-1ZKF9RnrBOOwbyJU91y21A14@rattlesnake.rmq.cloudamqp.com/qjgqxgdo");
    }));

builder.Services.AddHostedService<PublishMessageService>(provider =>
{
    using IServiceScope scope = provider.CreateScope();
    IPublishEndpoint publishEndpoint =  scope.ServiceProvider.GetService<IPublishEndpoint>();
    return new(publishEndpoint);

});

var host = builder.Build();
host.Run();
