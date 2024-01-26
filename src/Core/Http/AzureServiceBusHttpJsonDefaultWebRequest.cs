using System;
using System.IO;
using System.Net;

using System.Collections.Generic;

using AzureServiceBus.RestSDK.Core.Interface.Http;

namespace AzureServiceBus.RestSDK.Core.Http
{
    public class AzureServiceBusHttpJsonDefaultWebRequest : IAzureServiceBusHttpRequest
    {

        public AzureServiceBusTry<Exception, string> Post(string url, Dictionary<string, string> headers, byte[] body)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ProtocolVersion = HttpVersion.Version10;
                request.KeepAlive = false;
                request.Method = "POST";
                request.ContentType = "application/json; charset=UTF-8";
                request.Accept = "application/json";
                request.ContentLength = body.Length;

                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }

                var requestStream = request.GetRequestStream();

                requestStream.Write(body, 0, body.Length);
                requestStream.Close();

                var response = (HttpWebResponse)request.GetResponse();
                string result;
                using (var rdr = new StreamReader(response.GetResponseStream()))
                {
                    result = rdr.ReadToEnd();
                }
                return result;
            }

            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
