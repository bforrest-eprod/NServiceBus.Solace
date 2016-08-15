using NServiceBus;
using NServiceBus.Transports.Solace.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPublish
{
    class Program
    {
        private static IEndpointInstance endpointInstance = null;

        static void Main(string[] args)
        {    
            Console.WriteLine("Ta-da");
            StartBus();
            var msg = new MessageA("subject", "body of message");

            endpointInstance.Publish(msg);
        }

        static async void StartBus()
        {
            // Create a solace queue?
            var endpointConfiguration = new EndpointConfiguration("Samples.RabbitMQ.Simple");
            endpointConfiguration.SendOnly();

            var transport = endpointConfiguration.UseTransport<SolaceTransport>();
            transport.ConnectionString("host=ec2-54-227-230-129.compute-1.amazonaws.com");

            var startableEndpoint = await Endpoint.Create(endpointConfiguration)
                .ConfigureAwait(false);

            endpointInstance = await startableEndpoint.Start().ConfigureAwait(false);
        }

        static async Task StopBus()
        {
            await endpointInstance.Stop();
        }
    }
    public class MessageA
    {
        public MessageA(string subject, string body)
        {
            Subject = subject;
            Body = body;
        }
        public string Subject { get; private set; }

        public string Body { get; private set; }
    }
}
