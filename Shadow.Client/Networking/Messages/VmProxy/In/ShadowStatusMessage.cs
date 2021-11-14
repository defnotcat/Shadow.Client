using System.Text.Json.Serialization;
using DataModel = Shadow.Client.Networking.Messages.VmProxy.Out.ShadowStatusMessage.DataModel;

namespace Shadow.Client.Networking.Messages.VmProxy.In
{
    public class ShadowStatusMessage : VmProxyMessageIn
    {
        
        [JsonPropertyName("err")] public int Error { get; set; }
        [JsonPropertyName("data")] public DataModel Data { get; set; }
        [JsonPropertyName("service")] public string Service { get; set; }

        public ShadowStatusMessage() : base("forward") { }
    }
}