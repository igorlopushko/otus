using System.Collections;
using System.Collections.Generic;
using Otus.Archiver.Base;

namespace Otus.Archiver.Algorithm.Huffman
{
    public class Encoder : IEncoder
    {
        
        private readonly Tree _tree;
        private List<object> _settings;

        public object[] Settings => _settings.ToArray();
        
        internal Encoder(Tree tree)
        {
            _tree = tree;
            _settings = new List<object> {_tree};
        }
        
        internal Encoder(string source)
        {
            _tree = new Tree();
            _tree.Build(source);
            _settings = new List<object> {_tree};
        }
        
        public BitArray Encode(string source)
        {
            var encodedSource = new List<bool>();

            foreach (var symbol in source)
            {
                var encodedSymbol = _tree.Root.Traverse(symbol, new List<bool>());
                encodedSource.AddRange(encodedSymbol);
            }

            var bits = new BitArray(encodedSource.ToArray());

            return bits;
        }

        public string Decode(BitArray bits)
        {
            var current = _tree.Root;
            var decoded = string.Empty;

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

                if (_tree.IsLeaf(current))
                {
                    decoded += current.Symbol;
                    current = _tree.Root;
                }
            }

            return decoded;
        }
    }
}