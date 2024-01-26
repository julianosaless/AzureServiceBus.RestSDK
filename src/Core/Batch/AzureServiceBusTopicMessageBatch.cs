using System.Collections.Generic;

namespace AzureServiceBus.RestSDK.Core.Batch
{
    public class AzureServiceBusTopicMessageBatch
    {
        public string Name { get; }
        public IEnumerable<AzureServiceBusMessageBatch> Messages { get; }

        public AzureServiceBusTopicMessageBatch(string name, IEnumerable<AzureServiceBusMessageBatch> messages)
        {
            Name = name;
            Messages = messages;
        }
    }
}
