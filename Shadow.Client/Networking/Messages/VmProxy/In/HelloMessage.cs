using System.Text.Json.Serialization;

namespace Shadow.Client.Networking.Messages.VmProxy.In
{
    public class HelloMessage : VmProxyMessageIn
    {
        public HelloMessage() : base("hello")
        {
        }

        [JsonPropertyName("err")] public int Error { get; set; }
        [JsonPropertyName("vmtoken")] public string VmToken { get; set; }
        [JsonPropertyName("vmtoken_expiry")] public long VmTokenExpiry { get; set; }
    }
}