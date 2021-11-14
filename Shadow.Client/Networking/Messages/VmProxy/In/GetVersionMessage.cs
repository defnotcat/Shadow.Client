using System.Text.Json.Serialization;

namespace Shadow.Client.Networking.Messages.VmProxy.In
{
    public class GetVersionMessage : VmProxyMessageIn
    {
        public GetVersionMessage() : base("get-version") { }

        [JsonPropertyName("version")] public string Version { get; set; }
    }
}