﻿using MassTransit;
using RabbitMq.ESB.MassTransit.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.ESB.MassTransit.Consumer.Consumers
{
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Gelen Mesaj: {context.Message.Text}");
            return Task.CompletedTask;
        }
    }
}
