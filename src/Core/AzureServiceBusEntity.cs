
using AzureServiceBus.RestSDK.Core.Batch;

namespace AzureServiceBus.RestSDK.Core
{
    internal class AzureServiceBusEntity
    {
        public string Namespace { get; }
        public string Token { get; }
        public AzureServiceBusTopic Topic { get; }
        public AzureServiceBusTopicMessageBatch TopicMessageBatch { get; }

        public AzureServiceBusEntity(string @namespace, string token, AzureServiceBusTopic topic)
        {
            Namespace = @namespace;
            Token = token;
            Topic = topic;
        }

        public AzureServiceBusEntity(string @namespace, string token, AzureServiceBusTopicMessageBatch topic)
        {
            Namespace = @namespace;
            Token = token;
            TopicMessageBatch = topic;
        }
    }
}
