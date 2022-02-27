using ProtoBuf;

namespace Shadow.Client.Protobuf.Models
{
    [ProtoContract]
    public class Context
    {
        
        [ProtoMember(1)] public int _ { get; set; }  
        
        [ProtoMember(2)] public string Version { get; set; }
       
        [ProtoMember(3)] public string Description { get; set; }

        public override string ToString() => $"{Description} {Version}";
    }
}