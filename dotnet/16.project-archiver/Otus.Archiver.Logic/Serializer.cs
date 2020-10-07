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
        
        public static IArchive DeserializerFomBin(Stream stream)
        {
            var formatter = new BinaryFormatter();
            return (IArchive)formatter.Deserialize(stream);
        }
    }
}