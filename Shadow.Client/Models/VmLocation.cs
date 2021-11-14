using System.Text.Json.Serialization;

namespace Shadow.Client.Models
{
    public class VmLocation
    {
        [JsonPropertyName("ip")] public string Host { get; set; }
        [JsonPropertyName("port")] public string RawPort { get; set; }
        [JsonIgnore] public int Port => 7000 + int.Parse(RawPort);
        [JsonPropertyName("alias")] public string Alias { get; set; }

        public override string ToString() => $"{Host}:{Port}";
    }
}