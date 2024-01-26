using System;
using System.Text;
using System.Collections.Generic;

namespace AzureServiceBus.RestSDK.Core.Batch
{
    public class AzureServiceBusMessageBatch
    {
        public Dictionary<string, object> Body = new Dictionary<string, object>();
        public Dictionary<string, object> UserProperties = new Dictionary<string, object>();
        public readonly Dictionary<string, object> BrokerProperties = new Dictionary<string, object>();

        public AzureServiceBusMessageBatch(string body)
        {
            Body.Add("Body", body);
            AddBrokerProperties(new Dictionary<string, string> { { "MessageId", CreateMD5Hash(body) } });
        }

        public AzureServiceBusMessageBatch(string body, Dictionary<string, string> filter)
        {
            Body.Add("Body", body);
            AddBrokerProperties(new Dictionary<string, string> { { "MessageId", CreateMD5Hash(body)} });
            AddUserProperties(filter);
        }

        public AzureServiceBusMessageBatch(string id, string body)
        {
            Body.Add("Body", body);
            AddBrokerProperties(new Dictionary<string, string> { { "MessageId", id } });
        }

        public AzureServiceBusMessageBatch(string id, string body, Dictionary<string, string> filter)
        {
            Body.Add("Body", body);
            AddBrokerProperties(new Dictionary<string, string> { { "MessageId", id } });
            AddUserProperties(filter);
        }


        private void AddUserProperties(Dictionary<string, string> properties)
        {
            foreach (var item in properties)
            {
                if (UserProperties.ContainsKey(item.Key)) continue;
                UserProperties.Add(item.Key, item.Value);
            }
        }

        private void AddBrokerProperties(Dictionary<string, string> properties)
        {
            foreach (var item in properties)
            {
                if (BrokerProperties.ContainsKey(item.Key)) continue;
                BrokerProperties.Add(item.Key, item.Value);
            }
        }

        public string CreateMD5Hash(string input)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return string.Format("{0}{1}",sb.ToString(),DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}
