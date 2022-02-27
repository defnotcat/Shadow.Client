using System;
using System.Linq;
using System.Reflection;
using ProtoBuf;
using ProtoBuf.Meta;

namespace Shadow.Client.Protobuf
{
    public static class ProtoSerializer
    {
        public static MethodInfo DeserializeMethodInf { get; set; }

        static ProtoSerializer()
        {
            // Literally the easiest way, not comparing ToString just makes your life pain
            DeserializeMethodInf = typeof(Serializer).GetMethods()
                .Last(x => x.ToString() == "T Deserialize[T](System.ReadOnlyMemory`1[System.Byte], T, System.Object)");
        }

        public static T Deserialize<T>(byte[] data)
        {
            return Serializer.Deserialize<T>(new ReadOnlyMemory<byte>(data));
        }

        public static object Deserialize(byte[] data, Type type)
        {
            var readOnlyMem = new ReadOnlyMemory<byte>(data);
            var genericMethod = DeserializeMethodInf.MakeGenericMethod(type);
            return genericMethod.Invoke(null, new object[] {readOnlyMem, null, null});
        }
    }
}