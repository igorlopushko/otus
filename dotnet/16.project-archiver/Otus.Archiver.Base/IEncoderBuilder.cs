namespace Otus.Archiver.Base
{
    public interface IEncoderBuilder<out T> where T : IEncoder
    {
        T Build(string source);
        T Build(IArchive archive);
    }
}