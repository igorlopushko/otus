using System.Threading.Tasks;

namespace Otus.Archiver.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var command = CommandParser.Parse(args);
            await command.Execute();
        }
    }
}