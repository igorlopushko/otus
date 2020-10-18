using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Otus.Archiver.Base;

namespace Otus.Archiver.Logic
{
    public class Serializer<T>
    {
        public static void SerializerToBin(Stream stream, IArchive obj)
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
        }
        
        public static T DeserializerFomBin(Stream stream)
        {
            var formatter = new BinaryFormatter();
            return (T)formatter.Deserialize(stream);
        }
    }
}