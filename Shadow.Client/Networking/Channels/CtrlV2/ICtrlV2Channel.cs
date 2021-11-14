using Shadow.Client.Networking.Messages;
using Shadow.Client.Networking.Messages.CtrlV2;

namespace Shadow.Client.Networking.Channels.CtrlV2
{
    public interface ICtrlV2Channel : IMessageChannel<CtrlV2MessageIn, CtrlV2MessageOut>
    {
        
    }
}