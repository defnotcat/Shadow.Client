using ProtoBuf;
using ProtoBuf.Meta;

namespace Shadow.Client.Protobuf.Models.Tasks
{
    [ProtoContract]
    public class RequestAuthenticationTask
    {

        [ProtoMember(2)] public int _ { get; set; }
        
        [ProtoMember(4)] public string VmToken { get; set; }
        
        [ProtoMember(5)] public string SessionId { get; set; }
        
        [ProtoMember(6)] public string Expiry { get; set; }

    }
}