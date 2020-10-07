using System;
using System.Collections.Generic;
using System.Linq;

namespace Otus.Archiver.Algorithm.Huffman
{
    [Serializable]
    public class Tree
    {
        private readonly List<Node> _nodes = new List<Node>();
        private readonly Dictionary<char, int> _frequencies = new Dictionary<char, int>();
        
        public Node Root { get; set; }

        public void Build(string source)
        {
            // build frequency table
            foreach (var symbol in source)
            {
                if (!_frequencies.ContainsKey(symbol))
                {
                    _frequencies.Add(symbol, 0);
                }

                _frequencies[symbol]++;
            }

            // build nodes
            foreach (KeyValuePair<char, int> symbol in _frequencies)
            {
                _nodes.Add(new Node { Symbol = symbol.Key, Frequency = symbol.Value });
            }

            while (_nodes.Count > 1)
            {
                var orderedNodes = _nodes.OrderBy(node => node.Frequency).ToList();

                if (orderedNodes.Count >= 2)
                {
                    // take first two items
                    var taken = orderedNodes.Take(2).ToList();

                    // create a parent node by combining the frequencies
                    var parent = new Node
                    {
                        Symbol = '*',
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    _nodes.Remove(taken[0]);
                    _nodes.Remove(taken[1]);
                    _nodes.Add(parent);
                }

                Root = _nodes.FirstOrDefault();
            }
        }

        public bool IsLeaf(Node node)
        {
            return node.Left == null && node.Right == null;
        }
    }
}