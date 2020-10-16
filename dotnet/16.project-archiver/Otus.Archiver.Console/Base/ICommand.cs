using System.Threading.Tasks;

namespace Otus.Archiver.Console.Base
{
    public interface ICommand
    {
        Task Execute();
    }
}