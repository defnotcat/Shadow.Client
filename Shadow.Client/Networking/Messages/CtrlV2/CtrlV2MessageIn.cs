using System;
using System.IO;
using ProtoBuf;
using Shadow.Client.Networking.Serialization;
using Shadow.Client.Protobuf;
using Shadow.Client.Protobuf.Models;
using Shadow.Client.Protobuf.Models.Tasks;

namespace Shadow.Client.Networking.Messages.CtrlV2
{
    [ProtoContract]
    public class CtrlV2MessageIn : IDeserializable
    {
        
        [ProtoMember(4)] public Context Context1 { get; set; }
        [ProtoMember(5)] public Context Context2 { get; set; }
        
        [ProtoMember(3)] public ControlTaskModel Model { get; set; }

        public (CtrlV2MessageIn, object) DeserializeMessage(byte[] serializedMsg)
        {
            return ((CtrlV2MessageIn, object))Deserialize(serializedMsg);
        }
        
        public object Deserialize(byte[] serializedData)
        {
            // Deserialize the msg a first time as a common type that's the same for all incoming messages
            var msgBase = ProtoSerializer.Deserialize<CtrlV2MessageIn>(serializedData);
            var msgType = msgBase.Model.GetMessageType();
            // And then deserialize a second time once we know the message kind/type
            var msg = ProtoSerializer.Deserialize(serializedData, msgType);
            return (msgBase, msg);
        }
    }
}