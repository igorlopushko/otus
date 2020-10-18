using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Otus.Archiver.Base;

namespace Otus.Archiver.Algorithm.LZW
{
    public class Encoder : IEncoder
    {
        public async Task<IArchive> EncodeAsync(string data)
        {
            var compressed = new List<int>();

            await Task.Run(() =>
            {
                // build the dictionary
                var dictionary = new Dictionary<string, int>();
                for (var i = 0; i < 256; i++)
                {
                    dictionary.Add(((char) i).ToString(), i);
                }

                var word = string.Empty;

                foreach (var symbol in data)
                {
                    var wc = word + symbol;
                    if (dictionary.ContainsKey(wc))
                    {
                        word = wc;
                    }
                    else
                    {
                        // write w to output
                        compressed.Add(dictionary[word]);
                        // wc is a new sequence; add it to the dictionary
                        dictionary.Add(wc, dictionary.Count);
                        word = symbol.ToString();
                    }
                }

                // write remaining output if necessary
                if (!string.IsNullOrEmpty(word))
                {
                    compressed.Add(dictionary[word]);
                }
            });
            
            return new Archive
            {
                Data = compressed.ToArray(),
                Type = EncodingType.LZW
            };
        }

        public async Task<string> DecodeAsync(IArchive archive)
        {
            var data = (int[]) archive.Data;
            var compressed = new List<int>(data);
            var decompressed = new StringBuilder();

            await Task.Run(() =>
            {
                // build the dictionary
                var dictionary = new Dictionary<int, string>();
                for (var i = 0; i < 256; i++)
                {
                    dictionary.Add(i, ((char) i).ToString());
                }

                var word = dictionary[compressed[0]];
                compressed.RemoveAt(0);
                decompressed.Append(word);

                foreach (var index in compressed)
                {
                    string entry = null;
                    if (dictionary.ContainsKey(index))
                    {
                        entry = dictionary[index];
                    }
                    else if (index == dictionary.Count)
                    {
                        entry = word + word[0];
                    }

                    decompressed.Append(entry);

                    // new sequence; add it to the dictionary
                    dictionary.Add(dictionary.Count, word + entry[0]);

                    word = entry;
                }
            });
 
            return decompressed.ToString();
        }
    }
}