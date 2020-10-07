using System.Threading.Tasks;
using Otus.Archiver.Base;
using Otus.Archiver.Logic;

namespace Otus.Archiver.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");
            
            var factory = new ArchiveFactory(AlgorithmType.Huffman);

            //await factory.EncodeAsync("source.txt", "archive.dat");
            
            await factory.DecodeAsync("archive.dat", "result.txt");
        }
    }
}