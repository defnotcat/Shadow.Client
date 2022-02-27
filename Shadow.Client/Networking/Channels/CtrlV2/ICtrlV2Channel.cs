using System.IO;
using Shadow.Client.Networking.Messages.CtrlV2;
using Shadow.Client.Networking.Messages.VmProxy;

namespace Shadow.Client.Networking.Channels.CtrlV2
{
    public interface ICtrlV2Channel : IMessageChannel<CtrlV2MessageIn, CtrlV2MessageOut>
    {
        byte[] SerializeMessage(VmProxyMessageOut message)
        {
            using var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            message.Write(writer);
            return stream.ToArray();
        }
    }
}