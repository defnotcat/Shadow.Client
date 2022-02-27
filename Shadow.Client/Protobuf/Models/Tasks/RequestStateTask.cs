using ProtoBuf;

namespace Shadow.Client.Protobuf.Models.Tasks
{
    [ProtoContract]
    public class RequestStateTask : ControlTaskModel
    {
        [ProtoMember(1)] public Version Version { get; set; }
    }
    
}