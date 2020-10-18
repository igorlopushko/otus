using System;
using System.Threading.Tasks;
using Otus.Archiver.Base;
using Otus.Archiver.Console.Base;
using Otus.Archiver.Logic;

namespace Otus.Archiver.Console.Commands
{
    public class ArchiveCommand : ICommand
    {
        private string _sourceFile;
        private string _targetFile;
        private string _method;

        internal ArchiveCommand(string source, string target, string method)
        {
            _sourceFile = source;
            _targetFile = target;
            _method = method;
        }

        public async Task Execute()
        {
            var encodingType = string.IsNullOrEmpty(_method) ? EncodingType.Huffman : GetEncodingType();
            var factory = new ArchiveFactory(encodingType);
            await factory.EncodeAsync(_sourceFile, _targetFile);
        }

        private EncodingType GetEncodingType()
        {
            switch (_method)
            {
                case "rle":
                    return EncodingType.RLE;
                case "huffman":
                    return EncodingType.Huffman;
                case "lzw":
                    return EncodingType.LZW;
                default:
                    throw new ArgumentException("Not supported encoding method");
            }
        }
    }
}