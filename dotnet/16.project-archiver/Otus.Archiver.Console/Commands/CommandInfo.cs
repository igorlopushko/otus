namespace Otus.Archiver.Console.Commands
{
    public class CommandInfo
    {
        public const string ArchiveCommandTag = "archive";
        public const string ArchiveCommandShortTag = "a";
        public static readonly string ArchiveCommandDescription = $"Archive file.";
        
        public const string UnArchiveCommandTag = "unarchive";
        public const string UnArchiveCommandShortTag = "u";
        public const string UnArchiveCommandDescription = "UnArchive file.";
        
        public const string HelpCommandTag = "help";
        public const string HelpCommandShortTag = "h";
        public const string HelpCommandDescription = "Displays user's manual. Other options are not used.";
        
        public const string SourceOptionTag = "source";
        public const string SourceOptionShortTag = "s";
        public const string SourceOptionDescription = "Source file path.";
        
        public const string TargetOptionTag = "target";
        public const string TargetOptionShortTag = "t";
        public const string TargetOptionDescription = "Target file path.";
        
        public const string MethodOptionTag = "method";
        public const string MethodOptionShortTag = "m";
        public const string MethodOptionDescription = 
            "Is optional parameter. Does not required for UnArchive operation. If not set Huffman Codes are used as a default option.";

        public const string HuffmanMethodTag = "huffman";
        public const string HuffmanMethodDescription = "Huffman codes.";
        
        public const string LzwMethodTag = "lzw";
        public const string LzwMethodDescription = "LZW algorithm.";
    }
}