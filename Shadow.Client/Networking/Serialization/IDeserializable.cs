namespace Shadow.Client.Networking.Serialization
{
    public interface IDeserializable
    {

        /// <summary>
        /// Deserialize raw data of a type and return an instance of it
        /// </summary>
        /// <param name="serializedData"></param>
        /// <returns></returns>
        public object Deserialize(byte[] serializedData);

    }
}