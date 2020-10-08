using System;
using System.Collections;

namespace Otus.Archiver.Base

{
    [Serializable]
    public class Archive : IArchive
    {
        public EncodingType Type { get; set; }
        public BitArray Data { get; set; }
        public object[] Settings { get; set; }
    }
}