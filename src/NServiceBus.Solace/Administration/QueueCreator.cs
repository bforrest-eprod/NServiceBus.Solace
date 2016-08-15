    namespace NServiceBus.Transports.Solace.Administration
{
    using System;
    using System.Threading.Tasks;
    using SolaceSystems.Solclient.Messaging;
    using Transports;

    internal class QueueCreator : ICreateQueues
    {
        public Task CreateQueueIfNecessary(QueueBindings queueBindings, string identity)
        {
            foreach (var receivingAddress in queueBindings.ReceivingAddresses)
            {
                CreateQueueIfNecessary(receivingAddress);
            }

            foreach (var sendingAddress in queueBindings.SendingAddresses)
            {
                CreateQueueIfNecessary(sendingAddress);
            }

            return TaskEx.CompletedTask;
        }

        private void CreateQueueIfNecessary(string address)
        {
            throw new NotImplementedException();

            // *************************************************************************************
            // TL;DR - You should delete all but the first line of this method.
            // *************************************************************************************
            // This is some basic junk copied out of the Solace .Net Client Examples.
            // It doesn't do anything functional, and I'm not even sure this is the right approach.
            // I just wanted to reference some crap to force parsing of the referenced
            // SolaceSystems.Solclient.Messaging assembly during compilation.
            ContextFactory.Instance.Init(new ContextFactoryProperties());
            var context = ContextFactory.Instance.CreateContext(new ContextProperties(), null);

            using (var session = CreateSession(context))
            {
                if (session.Connect() == ReturnCode.SOLCLIENT_OK)
                {
                    Console.WriteLine("Session successfully connected");
                }
            }
        }

        private static ISession CreateSession(IContext context)
        {
            // *************************************************************************************
            // YOU SHOULD DELETE THIS METHOD.  IT ONLY EXISTS TO LET THE CALLER COMPILE.
            // *************************************************************************************
            return context.CreateSession(new SessionProperties(),
                (sender, args) => { },
                (sender, args) => { });
        }
    }
}