using System.Collections;

namespace Otus.Archiver.Base
{
    public interface IArchive
    {
        AlgorithmType Type { get; set; }
        BitArray Data { get; set; }
        object[] Settings { get; set; }
    }
}