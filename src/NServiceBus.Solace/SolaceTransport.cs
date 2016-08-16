using System;
using System.Collections.Generic;
using NServiceBus.Routing;
using NServiceBus.Settings;
using NServiceBus.Logging;
using System.Threading.Tasks;
using NServiceBus.Performance.TimeToBeReceived;
using NServiceBus.Transports.Solace.Administration;

namespace NServiceBus.Transports.Solace.Connection
{
    /// <summary>
    /// Initializes all the factories and supported features for the transport.
    /// http://docs.particular.net/samples/custom-transport/
    /// </summary>
    /// <param name="settings">An instance of the current settings.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <returns>The supported factories.</returns>
    public class SolaceTransport : TransportDefinition
    {
        static ILog log = LogManager.GetLogger<SolaceTransport>();

        protected override TransportInfrastructure Initialize(SettingsHolder settings, string connectionString)
        {
            return new SolaceTransportInfrastructure(settings, connectionString);
        }

        public override string ExampleConnectionStringForErrorMessage { get; } = "";
    }

    public class SolaceTransportInfrastructure : TransportInfrastructure
    {
        private string connectionString;
        private SettingsHolder settings;

        /*
        ContextFactoryProperties cfp = new ContextFactoryProperties()
        {
        SolClientLogLevel = SolLogLevel.Warning
        };
        cfp.LogToConsoleError();
        ContextFactory.Instance.Init(cfp);
        */

        public SolaceTransportInfrastructure(SettingsHolder settings, string connectionString)
        {
            this.settings = settings;
            this.connectionString = connectionString;
        }

        public override TransportReceiveInfrastructure ConfigureReceiveInfrastructure()
        {
            return new TransportReceiveInfrastructure(
                messagePumpFactory: () => new SolaceTransportMessagePump(),
                queueCreatorFactory: () => new QueueCreator(),
                preStartupCheck: () => Task.FromResult(StartupCheckResult.Success));
        }

        public override TransportSendInfrastructure ConfigureSendInfrastructure()
        {
            return new TransportSendInfrastructure(
                dispatcherFactory: () => new Dispatcher(),
                preStartupCheck: () => Task.FromResult(StartupCheckResult.Success));
        }

        public override TransportSubscriptionInfrastructure ConfigureSubscriptionInfrastructure()
        {
            throw new NotImplementedException();
        }

        public override EndpointInstance BindToLocalEndpoint(EndpointInstance instance)
        {
            return instance;
        }

        public override string ToTransportAddress(LogicalAddress logicalAddress)
        {
            var endpointInstance = logicalAddress.EndpointInstance;
            var discriminator = endpointInstance.Discriminator ?? "";
            var qualifier = logicalAddress.Qualifier ?? "";
            return string.Concat(endpointInstance.Endpoint, discriminator, qualifier);
        }

        public override IEnumerable<Type> DeliveryConstraints
        {
            get
            {
                yield return typeof(DiscardIfNotReceivedBefore);
            }
        }

        public override TransportTransactionMode TransactionMode
        {
            get
            {
                return TransportTransactionMode.SendsAtomicWithReceive;
            }
        }

        public override OutboundRoutingPolicy OutboundRoutingPolicy
        {
            get
            {
                return new OutboundRoutingPolicy(
                    sends: OutboundRoutingType.Unicast,
                    publishes: OutboundRoutingType.Unicast,
                    replies: OutboundRoutingType.Unicast);
            }
        }
    }
}
