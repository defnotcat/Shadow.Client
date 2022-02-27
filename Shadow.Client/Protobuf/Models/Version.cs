using ProtoBuf;

namespace Shadow.Client.Protobuf.Models
{
    [ProtoContract]
    public class Version
    {
        
        [ProtoMember(1)] public int Major { get; set; }  
        
        [ProtoMember(2)] public int Minor { get; set; }
        
        [ProtoMember(3)] public int Revision { get; set; }
       
        public override string ToString() => $"{Major}.{Minor}.{Revision}";
        
    }
}