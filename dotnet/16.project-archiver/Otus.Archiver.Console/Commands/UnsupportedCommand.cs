using System.Threading.Tasks;
using Otus.Archiver.Console.Base;

namespace Otus.Archiver.Console.Commands
{
    public class UnsupportedCommand : ICommand
    {
        public async Task Execute()
        {
            await Task.Run(() =>
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Command is not supported.");
                System.Console.WriteLine();
            });
        }
    }
}