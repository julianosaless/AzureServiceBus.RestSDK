using AzureServiceBus.RestSDK.Core.Batch;

namespace AzureServiceBus.RestSDK.Core.Interface.Batch
{
    public interface IAzureServiceBusTopicMessageBatchBuilder
    {
        IAzureServiceBusTopicBuilderBatch WithTopic(AzureServiceBusTopicMessageBatch topic);
    }
}
