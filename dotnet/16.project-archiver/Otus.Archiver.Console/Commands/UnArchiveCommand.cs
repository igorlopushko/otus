using System.Threading.Tasks;
using Otus.Archiver.Console.Base;
using Otus.Archiver.Logic;

namespace Otus.Archiver.Console.Commands
{
    public class UnArchiveCommand : ICommand
    {
        private string _sourceFile;
        private string _targetFile;
        
        internal UnArchiveCommand(string source, string target)
        {
            _sourceFile = source;
            _targetFile = target;
        }

        public async Task Execute()
        {
            var factory = new ArchiveFactory();
            await factory.DecodeAsync(_sourceFile, _targetFile);
        }
    }
}