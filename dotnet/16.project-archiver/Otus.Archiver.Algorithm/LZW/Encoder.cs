using System.Threading.Tasks;
using Otus.Archiver.Base;

namespace Otus.Archiver.Algorithm.LZW
{
    public class Encoder : IEncoder
    {
        public Task<IArchive> EncodeAsync(string data)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> DecodeAsync(IArchive archive)
        {
            throw new System.NotImplementedException();
        }
    }
}