using Newtonsoft.Json;

namespace AzureServiceBus.RestSDK.Template
{
    public class AzureServiceBusTemplateBase<T>
    {
        [JsonProperty("store_id")]
        public string StoreId { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        public AzureServiceBusTemplateBase(string storeId, T data)
        {
            StoreId = storeId;
            Data = data;
        }
    }
}
