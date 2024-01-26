using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Newtonsoft.Json;

using AzureServiceBus.RestSDK.Core.Http;
using AzureServiceBus.RestSDK.Core.Interface.Batch;
using AzureServiceBus.RestSDK.Core.Interface.Http;
using AzureServiceBus.RestSDK.Core.Interface.Settings;

namespace AzureServiceBus.RestSDK.Core.Batch
{
    internal class AzureServiceBusTopicMessageBatchBuilder : AzureServiceBusTopicBuilderBase, IAzureServiceBusTopicBuilderBatch, IAzureServiceBusSendBatchRequest
    {
        private readonly AzureServiceBusTopicMessageBatch Topic;
        private IAzureServiceBusHttpRequest AzureServiceBusBaseHttpRequest;
        private readonly Dictionary<string, string> AzureServiceBusHeaders;
        private readonly IAzureServiceBusSettings Settings;

        public AzureServiceBusTopicMessageBatchBuilder(IAzureServiceBusSettings settins, AzureServiceBusTopicMessageBatch topic, string @namespace, string keyName, string key) : base(@namespace, keyName, key)
        {
            Settings = settins;
            Topic = topic;
            AzureServiceBusBaseHttpRequest = new AzureServiceBusHttpJsonDefaultBatchWebRequest();
            AzureServiceBusHeaders = new Dictionary<string, string>();
        }

        public IAzureServiceBusSendBatchRequest Build()
        {
            return this;
        }

        public AzureServiceBusTry<Exception, string> Send(int timeout = 60)
        {
            var entity = new AzureServiceBusEntity(Namespace, CreateServiceBusToken(), Topic);

            var response = JsonConvert.SerializeObject(entity.TopicMessageBatch.Messages.Select(message => CreateMessageBody(message)));
            var body = Encoding.UTF8.GetBytes(response);

            AzureServiceBusHeaders.Add("Authorization", entity.Token);
            return AzureServiceBusBaseHttpRequest.Post($"{entity.Namespace}/{Topic.Name}/messages?timeout={timeout}", AzureServiceBusHeaders, body);
        }

        public AzureServiceBusTry<Exception, string> SendWith(Dictionary<string, string> headers, int timeout = 60)
        {
            foreach (var header in headers)
            {
                if (AzureServiceBusHeaders.ContainsKey(header.Key)) continue;
                AzureServiceBusHeaders.Add(header.Key, header.Value);
            }
            return Send(timeout);
        }

        public AzureServiceBusTry<Exception, string> SendWith(IAzureServiceBusHttpRequest azureServiceBusHttpRequest, int timeout = 60)
        {
            AzureServiceBusBaseHttpRequest = azureServiceBusHttpRequest;
            return Send(timeout);
        }

        private Dictionary<string, object> CreateMessageBody(AzureServiceBusMessageBatch message)
        {
            AddBrokerProperties(message);
            AddUserProperties(message);
            return message.Body;
        }

        private void AddBrokerProperties(AzureServiceBusMessageBatch message)
        {
            if (Settings.RequiresSession)
            {
                if (!message.BrokerProperties.ContainsKey("SessionId"))
                {
                    message.BrokerProperties.Add("SessionId", Settings.CreateSessionId());
                }
            }
            message.Body.Add("BrokerProperties", message.BrokerProperties);
        }

        private void AddUserProperties(AzureServiceBusMessageBatch message)
        {
            message.Body.Add("UserProperties", message.UserProperties);
        }
    }
}
