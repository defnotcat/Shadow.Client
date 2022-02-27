using System.IO;

namespace Shadow.Client.Networking.Serialization
{
    public interface ISerializable
    {
        
        /// <summary>
        /// Serialize the current instance to a BinaryWriter
        /// </summary>
        /// <param name="writer"></param>
        void Write(BinaryWriter writer);
        
    }
}