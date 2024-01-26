using System;
using System.Collections.Generic;

using AzureServiceBus.RestSDK.Core.Interface.Http;

namespace AzureServiceBus.RestSDK.Core.Interface
{
    public interface IAzureServiceBusSendRequest
    {
        AzureServiceBusTry<Exception, string> Send();
        AzureServiceBusTry<Exception, string> SendWith(Dictionary<string, string> headers);
        AzureServiceBusTry<Exception, string> SendWith(IAzureServiceBusHttpRequest azureServiceBusHttpRequest);
    }
}
