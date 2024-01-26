using AzureServiceBus.RestSDK.Core.Interface;

namespace AzureServiceBus.RestSDK
{
    public interface IAzureServiceBusAuthorizationBuilder
    {
        IAzureServiceBusFactory WithAuthorization(string keyName, string key);
    }
}
