using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Otus.Archiver.Base;

namespace Otus.Archiver.Logic
{
    public class ArchiveFactory
    {
        private readonly AlgorithmType _algorithmType;
        private readonly IEncoderBuilder<IEncoder> _builder;

        public ArchiveFactory(AlgorithmType algorithmType)
        {
            _algorithmType = algorithmType;
            _builder = InitBuilder();
        }
        
        public async Task EncodeAsync(string source, string destination)
        {
            var fileContent = await File.ReadAllTextAsync(source);

            var encoder = _builder.Build(fileContent);

            var archive = new Archive
            {
                Data = encoder.Encode(fileContent),
                Type = _algorithmType,
                Settings = encoder.Settings.ToArray()
            };
            
            SerializeToFile(archive, destination);
        }

        public async Task DecodeAsync(string source, string destination)
        {
            var archive = DeserializeFromFile(source);
            
            var encoder =  _builder.Build(archive);

            var decodedString = encoder.Decode(archive.Data);

            using (var writer = new StreamWriter(destination, false)){ 
                await writer.WriteAsync(decodedString);
            }
        }

        private IEncoderBuilder<IEncoder> InitBuilder()
        {
            switch (_algorithmType)
            {
                case AlgorithmType.Huffman:
                    return new Algorithm.Huffman.EncoderBuilder();
                case AlgorithmType.LZW:
                    break;
            }
            
            throw new NotSupportedException("Not supported provided algorithm");
        }
        
        private void SerializeToFile(IArchive archive, string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                Serializer<Archive>.SerializerToBin(fs, archive);
            }
        }
        
        private IArchive DeserializeFromFile(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                return Serializer<Archive>.DeserializerFomBin(fs);
            }
        }
    }
}