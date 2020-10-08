using System;
using System.Collections.Generic;
using System.Linq;

namespace Otus.Archiver.Algorithm.Huffman
{
    [Serializable]
    internal class Tree
    {
        private Dictionary<char, int> _frequencyTable;
        
        public Node Root { get; set; }
        public Dictionary<char, int> FrequencyTable => _frequencyTable;
        
        public void Build(string source)
        {
            // build frequency table
            _frequencyTable = BuildFrequencyTable(source);
            
            Process();
        }
        
        public void Restore(Dictionary<char, int> frequencyTable)
        {
            // restore frequency table
            _frequencyTable = frequencyTable;
            
            Process();
        }

        private void Process()
        {
            var nodes = _frequencyTable.Select(symbol => new Node
            {
                Symbol = symbol.Key, 
                Frequency = symbol.Value
            }).ToList();
            
            // build nodes
            while (nodes.Count > 1)
            {
                var orderedNodes = nodes.OrderBy(node => node.Frequency).ToList();

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

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                Root = nodes.FirstOrDefault();
            }
        }
        
        public bool IsLeaf(Node node)
        {
            return node.Left == null && node.Right == null;
        }

        private static Dictionary<char, int> BuildFrequencyTable(string source)
        {
            var dict = new Dictionary<char, int>();
            foreach (var symbol in source)
            {
                if (!dict.ContainsKey(symbol))
                {
                    dict.Add(symbol, 0);
                }

                dict[symbol]++;
            }

            return dict;
        }
    }
}