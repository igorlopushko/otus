using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Otus.Archiver.Base;

namespace Otus.Archiver.Algorithm.RLE
{
    public class Encoder : IEncoder
    {
        public async Task<IArchive> EncodeAsync(string data)
        {
            var result = new StringBuilder();
            
            await Task.Run(() =>
            {
                var count = 1;
                var current = data[0];

                for (var i = 1; i < data.Length; i++)
                {
                    if (current == data[i])
                    {
                        count++;
                    }
                    else
                    {
                        result.AppendFormat("{0}{1}", count, current);
                        count = 1;
                        current = data[i];
                    }
                }

                result.AppendFormat("{0}{1}", count, current);
            });
            
            return new Archive
            {
                Data = Encoding.ASCII.GetBytes(result.ToString()),
                Type = EncodingType.RLE
            };
        }

        public async Task<string> DecodeAsync(IArchive archive)
        {
            var chars = new char[Encoding.ASCII.GetCharCount(archive.Data, 0, archive.Data.Length)];  
            Encoding.ASCII.GetChars(archive.Data, 0, archive.Data.Length, chars, 0);
            var asciiString = new string(chars);

            var result = new StringBuilder();
            await Task.Run(() => {
                var a = string.Empty;

                foreach (var c in asciiString)
                {
                    var current = c;
                    if (char.IsDigit(current))
                        a += current;
                    else
                    {
                        var count = int.Parse(a);
                        a = string.Empty;
                        for (var j = 0; j < count; j++)
                        {
                            result.Append(current);
                        }
                    }
                }
            
            });
            
            return result.ToString();
        }
    }
}