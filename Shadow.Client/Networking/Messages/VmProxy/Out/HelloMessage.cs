using System.Text.Json.Serialization;

namespace Shadow.Client.Networking.Messages.VmProxy.Out
{
    public class HelloMessage : VmProxyMessageOut
    {
        
        [JsonPropertyName("agent")] public string Agent { get; set; }
        [JsonPropertyName("token")] public string Token { get; set; }

        public HelloMessage(int id, string agent, string token) : base("hello", id)
        {
            Agent = agent;
            Token = token;
        }
    }
}