using System;

namespace Otus.Archiver.Base

{
    [Serializable]
    public class Archive : IArchive
    {
        public EncodingType Type { get; set; }
        public byte[] Data { get; set; }
        public object[] Settings { get; set; }
    }
}