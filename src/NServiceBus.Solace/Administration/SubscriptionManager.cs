﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus.Extensibility;

namespace NServiceBus.Transports.Solace.Administration
{
    class SubscriptionManager : IManageSubscriptions
    {
        public Task Subscribe(Type eventType, ContextBag context)
        {
            //throw new NotImplementedException();
            return Task.FromResult(0);
        }

        public Task Unsubscribe(Type eventType, ContextBag context)
        {
            //throw new NotImplementedException();
            return Task.FromResult(0);
        }
    }
}
