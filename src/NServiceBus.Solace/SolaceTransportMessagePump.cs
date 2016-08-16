using System;
using System.Threading.Tasks;

namespace NServiceBus.Transports.Solace.Connection
{
    internal class SolaceTransportMessagePump : IPushMessages
    {
        public Task Init(Func<PushContext, Task> pipe, CriticalError criticalError, PushSettings settings)
        {
            throw new NotImplementedException();
        }

        public void Start(PushRuntimeSettings limitations)
        {
            throw new NotImplementedException();
        }

        public Task Stop()
        {
            throw new NotImplementedException();
        }
    }
}