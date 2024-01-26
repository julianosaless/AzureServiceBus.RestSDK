namespace AzureServiceBus.RestSDK.Core.Interface
{
    public interface IAzureServiceBusNamespaceBuilder
    {
        IAzureServiceBusAuthorizationBuilder WithNamespace(string url);
    }
}
