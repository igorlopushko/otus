using System.Threading.Tasks;
using Otus.Archiver.Console.Base;

namespace Otus.Archiver.Console.Commands
{
    public class HelpCommand : ICommand
    {
        public async Task Execute()
        {
            await Task.Run(() => {
                System.Console.WriteLine();
                System.Console.WriteLine("Use following pattern --[command] --source [source file path] --target [target file path] --method [value]");
                System.Console.WriteLine();
                System.Console.WriteLine("Here are the base commands:");
                System.Console.WriteLine("--{0, -15}: {1}", CommandInfo.HelpCommandTag, CommandInfo.HelpCommandDescription);
                System.Console.WriteLine("--{0, -15}: {1}", CommandInfo.ArchiveCommandTag, CommandInfo.ArchiveCommandDescription);
                System.Console.WriteLine("--{0, -15}: {1}", CommandInfo.UnArchiveCommandTag, CommandInfo.UnArchiveCommandDescription);
                System.Console.WriteLine("--{0, -15}: {1}", CommandInfo.SourceOptionTag, CommandInfo.SourceOptionDescription);
                System.Console.WriteLine("--{0, -15}: {1}", CommandInfo.TargetOptionTag, CommandInfo.TargetOptionDescription);
                System.Console.WriteLine();
                System.Console.WriteLine(CommandInfo.MethodOptionDescription);
                System.Console.WriteLine("Here are implemented archive methods:");
                System.Console.WriteLine("{0, -15}: Huffman codes.", "[huffman]");
                System.Console.WriteLine("{0, -15}: LZW algorithm.", "[lzw]");
                System.Console.WriteLine();
            });
        }
    }
}