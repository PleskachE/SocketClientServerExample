using Model;
using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
namespace Common
{
    public static class ConverterBytes
    {
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }

        public static Object ByteArrayToObject(byte[] data)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Write(data, 0, data.Length);
            stream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)formatter.Deserialize(stream);
            return obj;
        }
    }
}
