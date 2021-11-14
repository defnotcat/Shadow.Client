using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shadow.Client.Networking.Messages.VmProxy
{
    
    public class VmProxyMessageIn : IReadableMessage
    {
        
        [JsonPropertyName("cmd")] public string Cmd { get; set; }
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("resp")] public string Resp { get; set; }
        
        public VmProxyMessageIn(string cmd)
        {
            Cmd = cmd;
        }

        public object Read(BinaryReader reader)
            => JsonSerializer.Deserialize(reader.ReadString(), GetType());
    }
}