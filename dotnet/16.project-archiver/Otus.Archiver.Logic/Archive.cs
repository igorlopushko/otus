using System;
using System.Collections;
using Otus.Archiver.Base;

namespace Otus.Archiver.Logic

{
    [Serializable]
    public class Archive : IArchive
    {
        public AlgorithmType Type { get; set; }
        public BitArray Data { get; set; }
        public object[] Settings { get; set; }
    }
}