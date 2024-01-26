using AzureServiceBus.RestSDK.Core.Batch;
using AzureServiceBus.RestSDK.Core.Interface.Batch;

namespace AzureServiceBus.RestSDK.Core.Interface
{
    public interface IAzureServiceBusFactory
    {
        IAzureServiceBusBuilder WithTopic(AzureServiceBusTopic topic);
        IAzureServiceBusTopicBuilderBatch WithTopic(AzureServiceBusTopicMessageBatch topic);
    }
}
