using System;
using System.Threading.Tasks;
using NServiceBus.Extensibility;

namespace NServiceBus.Transports.Solace.Connection
{
    internal class Dispatcher : IDispatchMessages
    {
        public Task Dispatch(TransportOperations outgoingMessages, ContextBag context)
        {
            throw new NotImplementedException();
        }
    }
}