using System;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class FenParserTask : ITask
    {
        public string[] Run(string[] data)
        {
            var rows = data[0].Split("/", StringSplitOptions.RemoveEmptyEntries);
            
            return new []
            {
                GetMask(Piece.WhitePawns, rows).ToString(),
                GetMask(Piece.WhiteKnights, rows).ToString(),
                GetMask(Piece.WhiteBishops, rows).ToString(),
                GetMask(Piece.WhiteRooks, rows).ToString(),
                GetMask(Piece.WhiteQueens, rows).ToString(),
                GetMask(Piece.WhiteKing, rows).ToString(),
                GetMask(Piece.BlackPawns, rows).ToString(),
                GetMask(Piece.BlackKnights, rows).ToString(),
                GetMask(Piece.BlackBishops, rows).ToString(),
                GetMask(Piece.BlackRooks, rows).ToString(),
                GetMask(Piece.BlackQueens, rows).ToString(),
                GetMask(Piece.BlackKing, rows).ToString()
            };
        }

        private ulong GetMask(Piece piece, string[] fenList)
        {
            ulong mask = 0;
            var rank = 0;

            for (var i = fenList.Length - 1; i >= 0; i--)
            {
                foreach (var c in fenList[i].ToCharArray())
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

        private bool IsGivenPiece(Piece p, char c)
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

        enum Piece
        {
            WhitePawns,
            WhiteKnights,
            WhiteBishops,
            WhiteRooks,
            WhiteQueens,
            WhiteKing,
            BlackPawns,
            BlackKnights,
            BlackBishops,
            BlackRooks,
            BlackQueens,
            BlackKing
        }
    }
}
