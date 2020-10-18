using System;
using System.IO;
using Otus.Archiver.Console.Base;
using Otus.Archiver.Console.Commands;

namespace Otus.Archiver.Console
{
    public static class CommandParser
    {
        public static ICommand Parse(string[] args)
        {
            var command = FindCommand(args);
            var sourceFile = FindSourceFilePath(args);
            var targetFile = FindTargetFilePath(args);
            var method = FindMethod(args);
            ICommand checkCmd;
            
            switch (command)
            {
                case "help":
                case "h":
                    return new HelpCommand();
                case "archive":
                case "a":
                    checkCmd = CheckMandatoryOptions(sourceFile, targetFile);
                    return checkCmd ?? new ArchiveCommand(sourceFile, targetFile, method);
                case "unarchive":
                case "u":
                    checkCmd = CheckMandatoryOptions(sourceFile, targetFile);
                    return checkCmd ?? new UnArchiveCommand(sourceFile, targetFile);
                default:
                    return new ErrorCommand(CommandInfo.NotSupportedCommandErrorMessage);
            }
        }

        private static ICommand CheckMandatoryOptions(string sourceFile, string targetFile)
        {
            if (string.IsNullOrEmpty(sourceFile) && string.IsNullOrEmpty(targetFile))
            {
                return new HelpCommand();
            }
            
            if (string.IsNullOrEmpty(sourceFile))
            {
                return new ErrorCommand(CommandInfo.SourceFileIsMissedErrorMessage);
            }

            if (!File.Exists(sourceFile))
            {
                return new ErrorCommand(string.Format(CommandInfo.SourceFileDoesNotExistErrorMessage, sourceFile));
            }
            
            if (string.IsNullOrEmpty(targetFile))
            {
                return new ErrorCommand(CommandInfo.TargetFileIsMissedErrorMessage);
            }

            return null;
        }

        private static string FindCommand(string[] args)
        {
            if (args == null || 
                args.Length <= 0 ||
                args.Length == 1 && args[0].Equals($"--{CommandInfo.HelpCommandTag}", StringComparison.InvariantCultureIgnoreCase) ||
                args[0].Equals($"-{CommandInfo.HelpCommandShortTag}", StringComparison.InvariantCultureIgnoreCase))
            {
                return CommandInfo.HelpCommandTag;
            }

            foreach (var argument in args)
            {
                if (argument.Equals($"--{CommandInfo.ArchiveCommandTag}", StringComparison.InvariantCultureIgnoreCase) ||
                    argument.Equals($"-{CommandInfo.ArchiveCommandShortTag}", StringComparison.InvariantCultureIgnoreCase) ||
                    argument.Equals($"--{CommandInfo.UnArchiveCommandTag}", StringComparison.InvariantCultureIgnoreCase) ||
                    argument.Equals($"-{CommandInfo.UnArchiveCommandShortTag}", StringComparison.InvariantCultureIgnoreCase))
                {
                    return TrimCommand(argument);
                }
            }

            return string.Empty;
        }

        private static string FindSourceFilePath(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i].Equals($"--{CommandInfo.SourceOptionTag}", StringComparison.InvariantCultureIgnoreCase) ||
                    args[i].Equals($"-{CommandInfo.SourceOptionShortTag}", StringComparison.InvariantCultureIgnoreCase) && 
                    i + 1 < args.Length)
                {
                    return args[i + 1];
                }
            }
            
            return string.Empty;
        }
        
        private static string FindTargetFilePath(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i].Equals($"--{CommandInfo.TargetOptionTag}", StringComparison.InvariantCultureIgnoreCase) ||
                    args[i].Equals($"-{CommandInfo.TargetOptionShortTag}", StringComparison.InvariantCultureIgnoreCase) &&  
                    i + 1 < args.Length)
                {
                    return args[i + 1];
                }
            }
            
            return string.Empty;
        }
        
        private static string FindMethod(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i].Equals($"--{CommandInfo.MethodOptionTag}", StringComparison.InvariantCultureIgnoreCase) ||
                    args[i].Equals($"-{CommandInfo.MethodOptionShortTag}", StringComparison.InvariantCultureIgnoreCase) &&  
                    i + 1 < args.Length)
                {
                    return args[i + 1];
                }
            }
            
            return string.Empty;
        }

        private static string TrimCommand(string command)
        {
            if (command.StartsWith("--"))
            {
                command = command.Substring(2);
            }
                
            if (command.StartsWith("-"))
            {
                command = command.Substring(1);
            }

            return command;
        }
    }
}