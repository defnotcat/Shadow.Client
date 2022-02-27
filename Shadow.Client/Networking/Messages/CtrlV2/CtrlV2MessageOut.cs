using System.IO;
using ProtoBuf;
using Shadow.Client.Networking.Serialization;

namespace Shadow.Client.Networking.Messages.CtrlV2
{
    public abstract class CtrlV2MessageOut : ISerializable
    {

        public void Write(BinaryWriter writer)
        {
            var memStream = new MemoryStream();
            Serializer.Serialize(memStream, this);
            writer.Write(memStream.ToArray());
        }
    }
}