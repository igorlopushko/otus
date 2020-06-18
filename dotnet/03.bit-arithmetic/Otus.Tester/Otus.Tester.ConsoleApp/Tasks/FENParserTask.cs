using Otus.Tester.ConsoleApp.Base;
using Otus.Tester.ConsoleApp.Util;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class FenParserTask : ITask
    {
        public string[] Run(string[] data)
        {
            return new []
            {
                FenToUInt64Converter.GetUInt64(Piece.WhitePawns, data[0]).ToString(),
                FenToUInt64Converter.GetUInt64(Piece.WhiteKnights, data[0]).ToString(),
                FenToUInt64Converter.GetUInt64(Piece.WhiteBishops, data[0]).ToString(),
                FenToUInt64Converter.GetUInt64(Piece.WhiteRooks, data[0]).ToString(),
                FenToUInt64Converter.GetUInt64(Piece.WhiteQueens, data[0]).ToString(),
                FenToUInt64Converter.GetUInt64(Piece.WhiteKing, data[0]).ToString(),
                FenToUInt64Converter.GetUInt64(Piece.BlackPawns, data[0]).ToString(),
                FenToUInt64Converter.GetUInt64(Piece.BlackKnights, data[0]).ToString(),
                FenToUInt64Converter.GetUInt64(Piece.BlackBishops, data[0]).ToString(),
                FenToUInt64Converter.GetUInt64(Piece.BlackRooks, data[0]).ToString(),
                FenToUInt64Converter.GetUInt64(Piece.BlackQueens, data[0]).ToString(),
                FenToUInt64Converter.GetUInt64(Piece.BlackKing, data[0]).ToString()
            };
        }
    }
}
