using System.Collections;
using System.Collections.Generic;

namespace Otus.Archiver.Base
{
    public interface IEncoder
    {
        BitArray Encode(string data);
        string Decode(BitArray data);
        object[] Settings { get; }
    }
}