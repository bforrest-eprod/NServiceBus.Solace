using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Logging;
using NServiceBus.Persistence.Legacy;
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
            Console.Title = "Solace Publisher";
             
            Console.WriteLine("Ta-da");
            StartBus();

            var work = new Task[5];

            for(var i = 0; i < 5; i++)
            {
                var msg = new MessageA($"subject {i}", $"body of message {1}");
                work[i] = endpointInstance.Publish(msg);
            }

            Task.WaitAll(work);
            Console.WriteLine("Work completed");
            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }

        static async void StartBus()
        {
            // Create a solace queue?
            var endpointConfiguration = new EndpointConfiguration("Solace.Enpoint");
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");

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
    
}
