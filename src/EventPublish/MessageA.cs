using NServiceBus;

namespace EventPublish
{
    public class MessageA : IMessage
    {
        public MessageA(string subject, string body)
        {
            Subject = subject;
            Body = body;
        }
        public string Subject { get; private set; }

        public string Body { get; private set; }
    }
}
