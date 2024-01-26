using System;
using System.Text;
using System.Collections.Generic;

using Newtonsoft.Json;

using AzureServiceBus.RestSDK.Core.Http;
using AzureServiceBus.RestSDK.Core.Log;
using AzureServiceBus.RestSDK.Core.Interface;
using AzureServiceBus.RestSDK.Core.Interface.Log;
using AzureServiceBus.RestSDK.Core.Interface.Http;
using AzureServiceBus.RestSDK.Core.Interface.Settings;


namespace AzureServiceBus.RestSDK.Core
{
    internal class AzureServiceBusTopicBuilder : AzureServiceBusTopicBuilderBase, IAzureServiceBusBuilder, IAzureServiceBusSendRequest
    {
        private readonly AzureServiceBusTopic Topic;
        private IAzureServiceBusHttpRequest AzureServiceBusBaseHttpRequest;
        private readonly Dictionary<string, string> AzureServiceBusHeaders;
        private readonly Dictionary<string, string> BrokerProperties;
        private readonly IAzureServiceBusSettings Settings;
        private readonly IAzureServiceBusLogRequest LogRequest;

        public AzureServiceBusTopicBuilder(IAzureServiceBusSettings settins, AzureServiceBusTopic topic, string @namespace, string keyName, string key) : base(@namespace, keyName, key)
        {
            Settings = settins;
            Topic = topic;
            AzureServiceBusBaseHttpRequest = new AzureServiceBusHttpJsonDefaultWebRequest();
            AzureServiceBusHeaders = new Dictionary<string, string>();
            BrokerProperties = new Dictionary<string, string>();
            LogRequest = new AzureServiceBusDefaultLogRequest();
        }

        public IAzureServiceBusSendRequest Build()
        {
            return this;
        }

        public AzureServiceBusTry<Exception, string> Send()
        {
            var entity = new AzureServiceBusEntity(Namespace, CreateServiceBusToken(), Topic);
            var body = Encoding.UTF8.GetBytes(entity.Topic.Message.Body);

            AzureServiceBusHeaders.Add("Authorization", entity.Token);
            AddBrokerProperties(entity);
            AddUserProperties(entity);
            var response = AzureServiceBusBaseHttpRequest.Post($"{entity.Namespace}/{Topic.Name}/messages", AzureServiceBusHeaders, body);
            CreateLogRequest(Topic, response);
            return response;
        }


        public AzureServiceBusTry<Exception, string> SendWith(Dictionary<string, string> headers)
        {
            AddHeader(headers);
            var response = Send();
            CreateLogRequest(Topic, response);
            return response;
        }

        public AzureServiceBusTry<Exception, string> SendWith(IAzureServiceBusHttpRequest azureServiceBusHttpRequest)
        {
            AzureServiceBusBaseHttpRequest = azureServiceBusHttpRequest;
            return Send();
        }

        private void AddBrokerProperties(AzureServiceBusEntity entity)
        {
            BrokerProperties.Add("MessageId", entity.Topic.Message.Id);
            if (Settings.RequiresSession)
            {
                BrokerProperties.Add("SessionId", Settings.CreateSessionId());
            }
            AzureServiceBusHeaders.Add("BrokerProperties", JsonConvert.SerializeObject(BrokerProperties));
        }

        private void AddUserProperties(AzureServiceBusEntity entity)
        {
            AddHeader(entity.Topic.Message.UserProperties);
        }

        private void AddHeader(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                if (AzureServiceBusHeaders.ContainsKey(header.Key)) continue;
                AzureServiceBusHeaders.Add(header.Key, header.Value);
            }
        }

        private void CreateLogRequest(AzureServiceBusTopic topic, AzureServiceBusTry<Exception, string> response)
        {
            if (!Settings.LogRequest) return;
            LogRequest.Send(topic, response);
        }
    }
}
