using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using twatter_userservice.DTO;

namespace twatter_API_gateway.Messaging
{
    public static class QueueProducer
    {
        public static void PublishMock(IModel channel, string mockvariable)
        {
            Console.WriteLine("Exchange wordt aangeroepen");
            channel.ExchangeDeclare("mock-direct-exchange", ExchangeType.Direct, arguments: null);
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = mockvariable };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("demo-direct-exchange", "account.init", null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
