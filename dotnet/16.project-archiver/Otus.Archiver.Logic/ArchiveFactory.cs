﻿using System;
using System.IO;
using System.Threading.Tasks;
using Otus.Archiver.Base;

namespace Otus.Archiver.Logic
{
    public class ArchiveFactory
    {
        private EncodingType _encodingType;
        private IEncoder _encoder;

        public ArchiveFactory() { }
        
        public ArchiveFactory(EncodingType encodingType)
        {
            _encodingType = encodingType;
            _encoder = InitEncoder();
        }
        
        public async Task EncodeAsync(string source, string destination)
        {
            if (_encoder == null)
            {
                throw new ArgumentException("Encoder is not initialized.");
            }
            
            var fileContent = await File.ReadAllTextAsync(source);
            
            var archive = await _encoder.EncodeAsync(fileContent);
            
            SerializeToFile(archive, destination);
        }

        public async Task DecodeAsync(string source, string destination)
        {
            var archive = DeserializeFromFile(source);
            _encodingType = archive.Type;
            _encoder = InitEncoder();
            
            var decodedString = await _encoder.DecodeAsync(archive);

            using (var writer = new StreamWriter(destination, false))
            { 
                await writer.WriteAsync(decodedString);
            }
        }

        private IEncoder InitEncoder()
        {
            switch (_encodingType)
            {
                case EncodingType.Huffman:
                    return new Algorithm.Huffman.Encoder();
                case EncodingType.LZW:
                    return new Algorithm.LZW.Encoder();
                case EncodingType.RLE:
                    return new Algorithm.RLE.Encoder();
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