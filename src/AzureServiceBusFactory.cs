using AzureServiceBus.RestSDK.Core;
using AzureServiceBus.RestSDK.Core.Batch;
using AzureServiceBus.RestSDK.Core.Interface;
using AzureServiceBus.RestSDK.Core.Interface.Batch;
using AzureServiceBus.RestSDK.Core.Interface.Settings;

namespace AzureServiceBus.RestSDK
{
    public sealed class AzureServiceBusFactory :
        IAzureServiceBusNamespaceBuilder,
        IAzureServiceBusAuthorizationBuilder,
        IAzureServiceBusTopicBuilder,
        IAzureServiceBusTopicMessageBatchBuilder,
        IAzureServiceBusFactory
    {
        private readonly IAzureServiceBusSettings SessionSettings;

        private string Namespace;
        private string KeyName;
        private string Key;

        internal AzureServiceBusFactory(IAzureServiceBusSettings settings = null)
        {
            SessionSettings = settings ?? new AzureServiceBusSessionSettings(requiresSession: true);
        }

        public static IAzureServiceBusNamespaceBuilder CreateNew()
        {
            return new AzureServiceBusFactory();
        }

        public static IAzureServiceBusNamespaceBuilder CreateNew(IAzureServiceBusSettings settings)
        {
            return new AzureServiceBusFactory(settings);
        }

        public IAzureServiceBusFactory WithAuthorization(string keyName, string key)
        {
            KeyName = keyName;
            Key = key;
            return this;
        }

        public IAzureServiceBusAuthorizationBuilder WithNamespace(string url)
        {
            Namespace = url;
            return this;
        }

        public IAzureServiceBusBuilder WithTopic(AzureServiceBusTopic topic)
        {
            return new AzureServiceBusTopicBuilder(SessionSettings, topic, Namespace, KeyName, Key);
        }

        public IAzureServiceBusTopicBuilderBatch WithTopic(AzureServiceBusTopicMessageBatch topic)
        {
            return new AzureServiceBusTopicMessageBatchBuilder(SessionSettings, topic, Namespace, KeyName, Key);
        }
    }
}
