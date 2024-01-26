namespace AzureServiceBus.RestSDK.Core.Interface.Batch
{
    public interface IAzureServiceBusTopicBuilderBatch
    {
        IAzureServiceBusSendBatchRequest Build();
    }
}
