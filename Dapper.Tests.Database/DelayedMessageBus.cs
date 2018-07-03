using Xunit.Sdk;

namespace Dapper.Tests.Database
{
    internal class DelayedMessageBus
    {
        private IMessageBus messageBus;

        public DelayedMessageBus(IMessageBus messageBus)
        {
            this.messageBus = messageBus;
        }
    }
}