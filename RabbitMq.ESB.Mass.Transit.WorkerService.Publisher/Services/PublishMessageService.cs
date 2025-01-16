using MassTransit;
using RabbitMq.ESB.MassTransit.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.ESB.Mass.Transit.WorkerService.Publisher.Services
{
    public class PublishMessageService : BackgroundService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishMessageService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 0;
            while (true)
            {
                ExampleMessage message = new()
                {
                    Text = $"{i}. mesaj"
                }; 
                await _publishEndpoint.Publish(message);
                //Burdaki publish özünde publish/subscribe patternini uygulari evente subscribe olan tüm queuelara mesaj iletilir.
            }
        }
    }
}
