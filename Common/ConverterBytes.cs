using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace Common
{
    public static class ConverterBytes
    {
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            var formatter = new BinaryFormatter();
            var stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }

        public static Object ByteArrayToObject(byte[] data)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            stream.Write(data, 0, data.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return (Object)formatter.Deserialize(stream);
        }
    }
}
