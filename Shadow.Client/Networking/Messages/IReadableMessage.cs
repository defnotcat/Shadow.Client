using System.IO;

namespace Shadow.Client.Networking.Messages
{
    public interface IReadableMessage
    {

        public object Read(BinaryReader reader);

    }
}