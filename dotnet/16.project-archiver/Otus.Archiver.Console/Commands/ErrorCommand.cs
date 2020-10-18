using System.Threading.Tasks;
using Otus.Archiver.Console.Base;

namespace Otus.Archiver.Console.Commands
{
    public class ErrorCommand : ICommand
    {
        private string _message;

        public ErrorCommand(string message)
        {
            _message = message;
        }
        
        public async Task Execute()
        {
            await Task.Run(() =>
            {
                System.Console.WriteLine();
                System.Console.WriteLine(_message);
                System.Console.WriteLine();
            });
        }
    }
}