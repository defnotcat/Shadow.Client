using System;
using ProtoBuf;
using Shadow.Client.Protobuf;
using Shadow.Client.Protobuf.Models;
using Shadow.Client.Protobuf.Models.Tasks;
using Version = Shadow.Client.Protobuf.Models.Version;

namespace Shadow.Client.Networking.Messages.CtrlV2.Out
{
    
    [Serializable, ProtoContract]
    public class RequestStateMessageOut : CtrlV2MessageOut
    {
        [ProtoMember(4)] public Context Context1 { get; set; }
        [ProtoMember(5)] public Context Context2 { get; set; }
        [ProtoMember(2)] public ControlTaskModel Model { get; set; }

        public RequestStateMessageOut(Context context1, Context context2, RequestStateTask task)
        {
            Context1 = context1;
            Context2 = context2;
            Model = new ControlTaskModel { RequestStateTask = task };
        }

        public RequestStateMessageOut() { }
    }
}