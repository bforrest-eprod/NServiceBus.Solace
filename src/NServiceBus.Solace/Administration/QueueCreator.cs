namespace NServiceBus.Transports.Solace.Administration
{
    using System;
    using System.Configuration;
    using System.Threading.Tasks;
    using SolaceSystems.Solclient.Messaging;
    using Transports;
    using Logging;
    internal class QueueCreator : ICreateQueues, IDisposable
    {
        private static IContext context = null;
        private static ISession session = null;
        private static ContextProperties contextProps = null;
        private static SessionProperties sessionProps = null;
        static ILog log = LogManager.GetLogger<QueueCreator>();

        public Task CreateQueueIfNecessary(QueueBindings queueBindings, string identity)
        {
            Init();

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
            IQueue queue = ContextFactory.Instance.CreateQueue(address);
 
            EndpointProperties endpointProps = new EndpointProperties()
            {
                Permission = EndpointProperties.EndpointPermission.Consume,
                AccessType = EndpointProperties.EndpointAccessType.Exclusive
            };

            //session.Provision(queue, endpointProps,
            //    ProvisionFlag.IgnoreErrorIfEndpointAlreadyExists & ProvisionFlag.WaitForConfirm, null);


            using (var session = CreateSession())
            {
                try
                {
                    if (session.Connect() == ReturnCode.SOLCLIENT_OK)
                    {
                        log.Info("Session connected");
                        Console.WriteLine("Session successfully connected");

                        session.Provision(queue, endpointProps,
                            ProvisionFlag.IgnoreErrorIfEndpointAlreadyExists & ProvisionFlag.WaitForConfirm, null);

                        log.Info($"Queue created {queue}");
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    throw;
                }
            }
        }

        private void Init()
        {
            ContextFactoryProperties cfp = new ContextFactoryProperties();
            cfp.SolClientLogLevel = SolLogLevel.Warning;
            cfp.LogToConsoleError();

            contextProps = new ContextProperties();
            
            sessionProps = new SessionProperties
            {
                VPNName = "default",
                UserName = "default",
                Host = ConfigurationManager.AppSettings["Host"] ?? "107.21.83.82"
            };

            ContextFactory.Instance.Init(cfp);

            context = ContextFactory.Instance.CreateContext(contextProps, null);
        }

        private static ISession CreateSession()
        {
            session = context.CreateSession(sessionProps, null, null);
            return session;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            session.Dispose();
            context.Dispose();

            /*
            queue.Dispose();
            
            IBrowser browser = null;
            IQueue queue = null;
            ContextProperties contextProps = null;
            SessionProperties sessionProps = null;
            */

            disposed = true;
        }

        private bool disposed = false;
    }
}