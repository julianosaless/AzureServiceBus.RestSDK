using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using AzureServiceBus.RestSDK.Core;
using AzureServiceBus.RestSDK.Template;
using AzureServiceBus.RestSDK.Core.Interface;

namespace AzureServiceBus.RestSDK.Doc
{
    internal class DemoServiceBusIntegration
    {
        protected readonly IAzureServiceBusFactory Factory;

        public DemoServiceBusIntegration()
        {
            Factory = AzureServiceBusFactory
                   .CreateNew()
                   .WithNamespace("")
                   .WithAuthorization("", "");
        }

        public AzureServiceBusTry<Exception, string> OnCreate(DemoTemplate template)
        {
            return Factory
                   .WithTopic(new AzureServiceBusTopic("topic", new AzureServiceBusMessage(ToJson(template), filter: new Dictionary<string, string> { { "status", "create" } })))
                   .Build()
                   .Send();
        }

        public AzureServiceBusTry<Exception, string> OnUpdate(DemoTemplate template, int hashCode)
        {
            if (CreateHashCode(template) == hashCode)
            {
                return string.Empty;
            }

            return Factory
                   .WithTopic(new AzureServiceBusTopic("topic", new AzureServiceBusMessage(ToJson(template), filter: new Dictionary<string, string> { { "status", "update" } })))
                   .Build()
                   .Send();
        }

        static string ToJson<T>(T value)
        {
            return JsonConvert.SerializeObject(new AzureServiceBusTemplateBase<T>("my_app", value), Formatting.Indented);
        }

        public int CreateHashCode<T>(T value)
        {
            var token = JToken.FromObject(value);
            var comparer = new JTokenEqualityComparer();
            return comparer.GetHashCode(token);
        }
    }
}
