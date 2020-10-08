using System.Threading.Tasks;

namespace Otus.Archiver.Base
{
    public interface IEncoder
    {
        Task<IArchive> EncodeAsync(string data);
        Task<string> DecodeAsync(IArchive archive);
    }
}