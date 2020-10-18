namespace Otus.Archiver.Base
{
    public interface IArchive
    {
        EncodingType Type { get; set; }
        object Data { get; set; }
        object[] Settings { get; set; }
    }
}