namespace AzureServiceBus.RestSDK.Core.Interface
{
    public interface IAzureServiceBusTopicBuilder
    {
        IAzureServiceBusBuilder WithTopic(AzureServiceBusTopic topic);
    }
}
