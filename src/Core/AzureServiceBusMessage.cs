using System;
using System.Text;
using System.Collections.Generic;

namespace AzureServiceBus.RestSDK.Core
{
    public class AzureServiceBusMessage
    {
        public string Id { get; }
        public string Body { get; }
        public Dictionary<string, string> UserProperties = new Dictionary<string, string>();

        public AzureServiceBusMessage(string body)
        {
            Id = CreateMD5Hash(body);
            Body = body;
        }

        public AzureServiceBusMessage(string body, Dictionary<string, string> filter)
        {
            Id = CreateMD5Hash(body);
            Body = body;
            AddUserProperties(filter);
        }

        public AzureServiceBusMessage(string id, string body)
        {
            Id = id;
            Body = body;
        }

        public AzureServiceBusMessage(string id, string body, Dictionary<string, string> filter)
        {
            Id = id;
            Body = body;
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
            return string.Format("{0}{1}", sb.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}
