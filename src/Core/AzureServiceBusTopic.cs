namespace AzureServiceBus.RestSDK.Core
{
    public class AzureServiceBusTopic
    {
        public string Name { get; }
        public AzureServiceBusMessage Message { get; }

        public AzureServiceBusTopic(string name, AzureServiceBusMessage message)
        {
            Name = name;
            Message = message;
        }
    }
}
