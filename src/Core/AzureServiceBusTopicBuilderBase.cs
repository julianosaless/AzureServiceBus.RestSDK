using System;
using System.Web;
using System.Text;

using System.Globalization;
using System.Security.Cryptography;

namespace AzureServiceBus.RestSDK.Core
{
    public abstract class AzureServiceBusTopicBuilderBase
    {
        protected readonly string Namespace;
        protected readonly string KeyName;
        protected readonly string Key;

        protected AzureServiceBusTopicBuilderBase(string @namespace, string keyName, string key)
        {
            Namespace = @namespace;
            KeyName = keyName;
            Key = key;
        }

        protected string CreateServiceBusToken()
        {
            var sinceEpoch = DateTime.UtcNow - new DateTime(1970, 1, 1);
            const int week = 60 * 60 * 24 * 7;

            var expiry = Convert.ToString((int)sinceEpoch.TotalSeconds + week);
            var stringToSign = HttpUtility.UrlEncode(Namespace) + "\n" + expiry;
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(Key)))
            {
                var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));
                var sasToken = string.Format(CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}&skn={3}", HttpUtility.UrlEncode(Namespace), HttpUtility.UrlEncode(signature), expiry, KeyName);
                return sasToken;
            }
        }

    }
}
