using System;
using System.Collections.Generic;
using NServiceBus.Routing;
using NServiceBus.Settings;

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
        protected override TransportInfrastructure Initialize(SettingsHolder settings, string connectionString)
        {
            return new SolaceTransportInfrastructure(settings, connectionString);
        }

        public override string ExampleConnectionStringForErrorMessage
        {
            get
            {
                throw new NotImplementedException();
            }
        }
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

        public override IEnumerable<Type> DeliveryConstraints
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override OutboundRoutingPolicy OutboundRoutingPolicy
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override TransportTransactionMode TransactionMode
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override EndpointInstance BindToLocalEndpoint(EndpointInstance instance)
        {
            throw new NotImplementedException();
        }

        public override TransportReceiveInfrastructure ConfigureReceiveInfrastructure()
        {
            throw new NotImplementedException();
        }

        public override TransportSendInfrastructure ConfigureSendInfrastructure()
        {
            throw new NotImplementedException();
        }

        public override TransportSubscriptionInfrastructure ConfigureSubscriptionInfrastructure()
        {
            throw new NotImplementedException();
        }

        public override string ToTransportAddress(LogicalAddress logicalAddress)
        {
            throw new NotImplementedException();
        }
    }
}
