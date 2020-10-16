namespace Otus.Archiver.Console.Commands
{
    public class CommandInfo
    {
        public static readonly string ArchiveCommandTag = "archive";
        public static readonly string ArchiveCommandShortTag = "a";
        public static readonly string ArchiveCommandDescription = $"Archive file. Short is -{ArchiveCommandShortTag}.";
        
        public static readonly string UnArchiveCommandTag = "unarchive";
        public static readonly string UnArchiveCommandShortTag = "u";
        public static readonly string UnArchiveCommandDescription = $"UnArchive file. Short is -{UnArchiveCommandShortTag}.";
        
        public static readonly string HelpCommandTag = "help";
        public static readonly string HelpCommandShortTag = "h";
        public static readonly string HelpCommandDescription = $"Displays user's manual. Short is -{HelpCommandShortTag}. Other arguments not used.";
        
        public static readonly string SourceOptionTag = "source";
        public static readonly string SourceOptionShortTag = "s";
        public static readonly string SourceOptionDescription = $"Source file path. Short is -{SourceOptionShortTag}.";
        
        public static readonly string TargetOptionTag = "target";
        public static readonly string TargetOptionShortTag = "t";
        public static readonly string TargetOptionDescription = $"Target file path. Short is -{TargetOptionShortTag}.";
        
        public static readonly string MethodOptionTag = "method";
        public static readonly string MethodOptionShortTag = "m";
        public static readonly string MethodOptionDescription = 
            $"--{MethodOptionTag} [value] is optional parameter. Short is -{MethodOptionShortTag} [value]. Does not required for UnArchive operation. If not set Huffman Codes are used for archive operation.";
    }
}