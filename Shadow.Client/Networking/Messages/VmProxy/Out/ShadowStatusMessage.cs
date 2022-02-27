using System.Text.Json.Serialization;

namespace Shadow.Client.Networking.Messages.VmProxy.Out
{
    public class ShadowStatusMessage : VmProxyMessageOut
    {
        public class DataModel
        {
            [JsonPropertyName("type")] public string Type { get; set; }
            [JsonPropertyName("value")] public object Value { get; set; }
        }
        
        public ShadowStatusMessage(int id) : base("forward", id) { }

        [JsonPropertyName("data")] public DataModel Data { get; set; }
        [JsonPropertyName("service")] public string Service { get; set; }
    }
}