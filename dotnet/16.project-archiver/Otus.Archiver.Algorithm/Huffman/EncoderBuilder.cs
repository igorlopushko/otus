using Otus.Archiver.Base;

namespace Otus.Archiver.Algorithm.Huffman
{
    public class EncoderBuilder : IEncoderBuilder<Encoder>
    {
        public Encoder Build(string source)
        {
            return new Encoder(source);
        }

        public Encoder Build(IArchive archive)
        {
            return new Encoder((Tree)archive.Settings[0]);
        }
    }
}