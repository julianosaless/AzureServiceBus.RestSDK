namespace AzureServiceBus.RestSDK.Core.Interface.Settings
{
    public interface IAzureServiceBusSessionSettings
    {
        bool RequiresSession { get; }

        bool LogRequest { get; }

        string CreateSessionId();
    }
}
