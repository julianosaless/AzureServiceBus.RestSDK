using Newtonsoft.Json;

namespace AzureServiceBus.RestSDK.Doc
{
    internal sealed class DemoTemplate
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
