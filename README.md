# Project .NET Legacy Integration with Service Bus via REST

## Description
This project addresses the unique challenge of integrating a legacy application, developed over 17 years ago, with modern service bus architectures using RESTful APIs. The application is built on an older version of the .NET framework, which lacks support from some contemporary libraries. This integration facilitates seamless communication between the legacy system and newer service bus technologies, ensuring continued functionality and compatibility in a changing technological landscape.

## Features
- Integration with service bus using RESTful APIs.
- Compatibility with older .NET framework versions.
- Seamless communication between legacy applications and modern service infrastructure.
- Robust error handling and logging for easier maintenance.

## Getting Started
### Dependencies
- .NET Framework (net 3.0 or higher)

## Sdk Features

| Features    | Status |
| -------- | ------- |
| Topics and subscription  | ✅   |
| Batch | ✅   |
| Duplicate Detection   | ✅    |
| Scheduled Delivery    | ✅   |
| Filters and Actions   | ✅    |
| Namespaces   | ✅    |
| Senders   | ✅    |
| Receivers   | ❌    |
| Logs   | ✅    |

## Examples
```
src/Doc
```

```
﻿using System;
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
````



