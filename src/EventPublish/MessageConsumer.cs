using NServiceBus;
using NServiceBus.Logging;
using System.Threading.Tasks;

namespace EventPublish
{
    public class MessageConsumer : IHandleMessages<MessageA>
    {
        static ILog log = LogManager.GetLogger<MessageConsumer>();

        public Task Handle(MessageA message, IMessageHandlerContext context)
        {
            log.Info($"handled {message.Subject}");
            return Task.FromResult(0);
        }
    }
}
