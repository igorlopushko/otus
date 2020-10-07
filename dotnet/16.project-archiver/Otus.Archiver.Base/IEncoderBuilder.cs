using System.Collections.Generic;

namespace Otus.Archiver.Base
{
    public interface IEncoderBuilder<out T>
    {
        T Build(string source);
        T Build(IArchive archive);
    }
}