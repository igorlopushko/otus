namespace Otus.Archiver.Console.Commands
{
    public class CommandInfo
    {
        public static readonly string ArchiveCommandTag = "archive";
        public static readonly string ArchiveCommandShortTag = "a";
        public static readonly string ArchiveCommandDescription = $"Archive file.";
        
        public static readonly string UnArchiveCommandTag = "unarchive";
        public static readonly string UnArchiveCommandShortTag = "u";
        public static readonly string UnArchiveCommandDescription = $"UnArchive file.";
        
        public static readonly string HelpCommandTag = "help";
        public static readonly string HelpCommandShortTag = "h";
        public static readonly string HelpCommandDescription = $"Displays user's manual. Other options are not used.";
        
        public static readonly string SourceOptionTag = "source";
        public static readonly string SourceOptionShortTag = "s";
        public static readonly string SourceOptionDescription = $"Source file path.";
        
        public static readonly string TargetOptionTag = "target";
        public static readonly string TargetOptionShortTag = "t";
        public static readonly string TargetOptionDescription = $"Target file path.";
        
        public static readonly string MethodOptionTag = "method";
        public static readonly string MethodOptionShortTag = "m";
        public static readonly string MethodOptionDescription = 
            "Is optional parameter. Does not required for UnArchive operation. If not set Huffman Codes are used as a default option.";
    }
}