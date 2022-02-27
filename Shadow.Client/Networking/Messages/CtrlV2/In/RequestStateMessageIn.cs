using ProtoBuf;
using Shadow.Client.Networking.Messages.CtrlV2.Out;
using Shadow.Client.Protobuf.Models.Tasks;

namespace Shadow.Client.Networking.Messages.CtrlV2.In
{
    [ProtoContract]
    public class RequestStateMessageIn : CtrlV2MessageIn
    {
        
        [ProtoMember(3)] public new ControlTaskModel Model { get; set; }
        [ProtoIgnore] public RequestStateTask Task => Model.RequestStateTask;

    }
    
}