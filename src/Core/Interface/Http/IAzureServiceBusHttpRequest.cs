using System;
using System.Collections.Generic;

namespace AzureServiceBus.RestSDK.Core.Interface.Http
{
    public interface IAzureServiceBusHttpRequest
    {
        AzureServiceBusTry<Exception, string> Post(string url, Dictionary<string, string> headers, byte[] body);
    }
}
