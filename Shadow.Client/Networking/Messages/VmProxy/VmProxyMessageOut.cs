using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shadow.Client.Networking.Messages.VmProxy
{
    public class VmProxyMessageOut : IWritableMessage
    {
        
        [JsonPropertyName("cmd")] public string Cmd { get; set; }
        [JsonPropertyName("id")] public int Id { get; set; }

        public VmProxyMessageOut(string cmd, int id)
        {
            Cmd = cmd;
            Id = id;
        }

        public void Write(BinaryWriter writer)
            => writer.Write(JsonSerializer.Serialize(this, GetType()));
    }
}