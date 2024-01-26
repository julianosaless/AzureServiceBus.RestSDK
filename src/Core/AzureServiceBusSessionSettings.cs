using System;

using AzureServiceBus.RestSDK.Core.Interface.Settings;

namespace AzureServiceBus.RestSDK.Core
{
    public class AzureServiceBusSessionSettings : IAzureServiceBusSettings
    {
        public bool RequiresSession { get; private set; }
        public bool LogRequest { get; private set; }

        public AzureServiceBusSessionSettings(bool requiresSession = true, bool logRequest = true)
        {
            RequiresSession = requiresSession;
            LogRequest = logRequest;
        }

        public string CreateSessionId() => Guid.NewGuid().ToString();

    }
}
