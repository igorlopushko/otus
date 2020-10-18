using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Archiver.Base;

namespace Otus.Archiver.Algorithm.Huffman
{
    public class Encoder : IEncoder
    {   
        public async Task<IArchive> EncodeAsync(string source)
        {
            var encodedSource = new List<bool>();
            var settings = new List<object>();

            await Task.Run(() =>
            {
                var tree = new Tree();
                tree.Build(source);
                settings.Add(tree.FrequencyTable);
                
                foreach (var symbol in source)
                {
                    var encodedSymbol = tree.Root.Traverse(symbol, new List<bool>());
                    encodedSource.AddRange(encodedSymbol);
                }
            });

            return new Archive
            {
                Data = new BitArray(encodedSource.ToArray()),
                Settings = settings.ToArray(),
                Type = EncodingType.Huffman
            };
        }

        public async Task<string> DecodeAsync(IArchive archive)
        {
            var decoded = string.Empty;
            var bits = (BitArray) archive.Data;

            await Task.Run(() =>
            {
                var tree = new Tree();
                tree.Restore((Dictionary<char, int>)archive.Settings[0]);
                var current = tree.Root;
                
                foreach (bool bit in bits)
                {
                    if (bit)
                    {
                        if (current.Right != null)
                        {
                            current = current.Right;
                        }
                    }
                    else
                    {
                        if (current.Left != null)
                        {
                            current = current.Left;
                        }
                    }

                    if (tree.IsLeaf(current))
                    {
                        decoded += current.Symbol;
                        current = tree.Root;
                    }
                }
            });
            
            return decoded;
        }
    }
}