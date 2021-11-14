using System.IO;

namespace Shadow.Client.Networking.Messages
{
    public interface IWritableMessage
    {

        void Write(BinaryWriter writer);

    }
}