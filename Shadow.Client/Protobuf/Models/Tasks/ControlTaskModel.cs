using System;
using ProtoBuf;
using Shadow.Client.Networking.Messages.CtrlV2.In;

namespace Shadow.Client.Protobuf.Models.Tasks
{
    [Serializable, ProtoContract]
    public class ControlTaskModel
    {

        [ProtoMember(3)] public RequestStateTask RequestStateTask { get; set; }

        public Type GetMessageType()
        {
            if (RequestStateTask != null) return typeof(RequestStateMessageIn);
            return null;
        }
        
    }
}