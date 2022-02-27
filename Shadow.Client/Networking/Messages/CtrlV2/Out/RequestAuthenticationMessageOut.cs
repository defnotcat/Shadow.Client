using System;
using ProtoBuf;
using Shadow.Client.Protobuf;
using Shadow.Client.Protobuf.Models;
using Shadow.Client.Protobuf.Models.Tasks;

namespace Shadow.Client.Networking.Messages.CtrlV2.Out
{

    [Serializable, ProtoContract]
    public class RequestAuthenticationTask
    {

        [ProtoMember(2)] public int _ { get; set; } = 1;
        
        [ProtoMember(4)] public string VmToken { get; set; }
        
        [ProtoMember(5)] public string SessionId { get; set; }
        
        [ProtoMember(6)] public string Timestamp { get; set; }

        /*
         todo
         *  7 {
              1 {
                1: 1
              }
            }
          }
         */

    }
    
    [Serializable, ProtoContract]
    public class RequestAuthenticationMessageOut : CtrlV2MessageOut
    {

        [ProtoMember(1)] public int _ { get; set; } = 1;
        [ProtoMember(4)] public Context Context1 { get; set; }
        [ProtoMember(5)] public Context Context2 { get; set; }
        [ProtoMember(2)] public ControlTaskModel Model { get; set; }

        public RequestAuthenticationMessageOut(Context context1, Context context2, RequestAuthenticationTask task)
        {
            Context1 = context1;
            Context2 = context2;
            Model = new() {  };
        }
    }
}