using System;
using System.Collections.Generic;
using AzureServiceBus.RestSDK.Core.Interface.Http;

namespace AzureServiceBus.RestSDK.Core.Interface.Batch
{
    public interface IAzureServiceBusSendBatchRequest
    {
        AzureServiceBusTry<Exception, string> Send(int timeout = 60);
        AzureServiceBusTry<Exception, string> SendWith(Dictionary<string, string> headers, int timeout = 60);
        AzureServiceBusTry<Exception, string> SendWith(IAzureServiceBusHttpRequest azureServiceBusHttpRequest, int timeout = 60);
    }
}
