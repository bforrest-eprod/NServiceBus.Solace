namespace NServiceBus.Transports.Solace.Administration
{
    using System;
    using System.Threading.Tasks;
    using SolaceSystems.Solclient.Messaging;
    using Transports;

    internal class QueueCreator : ICreateQueues
    {
        IContext context = null;
        ISession session = null;
        IBrowser browser = null;
        IQueue queue = null;
        ContextProperties contextProps = null;
        SessionProperties sessionProps = null;

        /// <summary>
        /// config section? https://msdn.microsoft.com/en-us/library/2tw134k3.aspx
        /// </summary>
        public QueueCreator()
        {
            ContextFactoryProperties cfp = new ContextFactoryProperties();
            cfp.SolClientLogLevel = SolLogLevel.Warning;
            cfp.LogToConsoleError();
            
            // Must init the API before using any of its artifacts.
            ContextFactory.Instance.Init(cfp);

            context = ContextFactory.Instance.CreateContext(contextProps, null);
            contextProps = new ContextProperties();
            sessionProps = new SessionProperties();

            sessionProps.Host = "ec2-52-91-172-113.compute-1.amazonaws.com";
            sessionProps.VPNName = "default";
            sessionProps.UserName = "default";

            session = context.CreateSession(sessionProps, null, null);
            session.Connect();
        }
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
            //throw new NotImplementedException();

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