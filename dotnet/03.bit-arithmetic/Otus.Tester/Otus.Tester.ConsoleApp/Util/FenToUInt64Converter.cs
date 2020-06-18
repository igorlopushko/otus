using System;

namespace Otus.Tester.ConsoleApp.Util
{
    public class FenToUInt64Converter
    {
        public static ulong GetUInt64(Piece piece, string data)
        {
            ulong mask = 0;
            var rank = 0;
            var rows = data.Split("/", StringSplitOptions.RemoveEmptyEntries);

            for (var i = rows.Length - 1; i >= 0; i--)
            {
                foreach (var c in rows[i].ToCharArray())
                {
                    if (int.TryParse(c.ToString(), out var blankCells))
                    {
                        rank += blankCells;
                    }
                    else
                    {
                        if (IsGivenPiece(piece, c))
                        {
                            mask += Convert.ToUInt64(Math.Pow(2, rank));
                        }

                        rank++;
                    }
                }
            }

            return mask;
        }

        private static bool IsGivenPiece(Piece p, char c)
        {
            switch (p)
            {
                case Piece.WhitePawns:
                    if (c.Equals('P'))
                    {
                        return true;
                    }
                    break;
                case Piece.WhiteKnights:
                    if (c.Equals('N'))
                    {
                        return true;
                    }
                    break;
                case Piece.WhiteBishops:
                    if (c.Equals('B'))
                    {
                        return true;
                    }
                    break;
                case Piece.WhiteRooks:
                    if (c.Equals('R'))
                    {
                        return true;
                    }
                    break;
                case Piece.WhiteQueens:
                    if (c.Equals('Q'))
                    {
                        return true;
                    }
                    break;
                case Piece.WhiteKing:
                    if (c.Equals('K'))
                    {
                        return true;
                    }
                    break;
                case Piece.BlackPawns:
                    if (c.Equals('p'))
                    {
                        return true;
                    }
                    break;
                case Piece.BlackKnights:
                    if (c.Equals('n'))
                    {
                        return true;
                    }
                    break;
                case Piece.BlackBishops:
                    if (c.Equals('b'))
                    {
                        return true;
                    }
                    break;
                case Piece.BlackRooks:
                    if (c.Equals('r'))
                    {
                        return true;
                    }
                    break;
                case Piece.BlackQueens:
                    if (c.Equals('q'))
                    {
                        return true;
                    }
                    break;
                case Piece.BlackKing:
                    if (c.Equals('k'))
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
            }
            return false;
        }
    }
}