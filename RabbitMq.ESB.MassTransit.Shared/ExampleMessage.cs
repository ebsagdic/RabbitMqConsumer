﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.ESB.MassTransit.Shared
{
    public class ExampleMessage : IMessage
    {
        public string Text { get; set; }
    }
}
