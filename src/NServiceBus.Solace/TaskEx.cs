namespace NServiceBus.Transports.Solace
{
    using System.Threading.Tasks;

    internal static class TaskEx
    {
        //TODO: remove when we update to 4.6 and can use Task.CompletedTask
        public static readonly Task CompletedTask = Task.FromResult(0);
    }
}