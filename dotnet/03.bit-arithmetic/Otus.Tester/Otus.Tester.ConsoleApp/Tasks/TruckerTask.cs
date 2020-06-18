using System;
using Otus.Tester.ConsoleApp.Base;
using Otus.Tester.ConsoleApp.Util;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class TruckerTask : ITask
    {
        private const int BoardSize = 8;

        public string[] Run(string[] data)
        {
            ulong blockers = GetAllBlockers(data[0]);
            ulong associateBlockers = GetAllWhites(data[0]);

            return new[]
            {
                GetRookMask(FenToUInt64Converter.GetUInt64(Piece.WhiteRooks, data[0]), blockers, associateBlockers).ToString(),
                GetBishopMask(FenToUInt64Converter.GetUInt64(Piece.WhiteBishops, data[0]), blockers, associateBlockers).ToString(),
                GetQueenMask(FenToUInt64Converter.GetUInt64(Piece.WhiteQueens, data[0]), blockers, associateBlockers).ToString(),
            };
        }

        private ulong GetAllBlockers(string data)
        {
            return FenToUInt64Converter.GetUInt64(Piece.WhitePawns, data) +
                   FenToUInt64Converter.GetUInt64(Piece.WhiteKnights, data) +
                   FenToUInt64Converter.GetUInt64(Piece.WhiteBishops, data) +
                   FenToUInt64Converter.GetUInt64(Piece.WhiteRooks, data) +
                   FenToUInt64Converter.GetUInt64(Piece.WhiteQueens, data) +
                   FenToUInt64Converter.GetUInt64(Piece.WhiteKing, data) +
                   FenToUInt64Converter.GetUInt64(Piece.BlackPawns, data) +
                   FenToUInt64Converter.GetUInt64(Piece.BlackKnights, data) +
                   FenToUInt64Converter.GetUInt64(Piece.BlackBishops, data) +
                   FenToUInt64Converter.GetUInt64(Piece.BlackRooks, data) +
                   FenToUInt64Converter.GetUInt64(Piece.BlackQueens, data) +
                   FenToUInt64Converter.GetUInt64(Piece.BlackKing, data);
        }

        private ulong GetAllWhites(string data)
        {
            return FenToUInt64Converter.GetUInt64(Piece.WhitePawns, data) +
                   FenToUInt64Converter.GetUInt64(Piece.WhiteKnights, data) +
                   FenToUInt64Converter.GetUInt64(Piece.WhiteBishops, data) +
                   FenToUInt64Converter.GetUInt64(Piece.WhiteRooks, data) +
                   FenToUInt64Converter.GetUInt64(Piece.WhiteQueens, data) +
                   FenToUInt64Converter.GetUInt64(Piece.WhiteKing, data);
        }

        private ulong GetAllBlacks(string data)
        {
            return FenToUInt64Converter.GetUInt64(Piece.BlackPawns, data) +
                   FenToUInt64Converter.GetUInt64(Piece.BlackKnights, data) +
                   FenToUInt64Converter.GetUInt64(Piece.BlackBishops, data) +
                   FenToUInt64Converter.GetUInt64(Piece.BlackRooks, data) +
                   FenToUInt64Converter.GetUInt64(Piece.BlackQueens, data) +
                   FenToUInt64Converter.GetUInt64(Piece.BlackKing, data);
        }

        private ulong GetRookMask(ulong position, ulong blockers, ulong associateBlockers)
        {
            // go UP
            var northRay = GetNorthRay(position);
            // get masked blockers by masking blockers and ray
            var maskedBlockers = blockers & northRay;
            // get least significant bit from masked blockers
            var leastSignificantBit = GetLeastSignificantBit(maskedBlockers);
            if (leastSignificantBit != 0)
            {
                // get partial ray from the lsb to the up
                var blockersNorthRay = GetNorthRay(leastSignificantBit);
                // check if lsb is not associate than include lsb to the ray
                var isNotAssociateBlocker = (leastSignificantBit & associateBlockers) != 0;
                if (isNotAssociateBlocker)
                {
                    blockersNorthRay += leastSignificantBit;
                }

                // [ray] & [invert partial ray]
                northRay &= ~blockersNorthRay;
            }

            // go RIGHT
            var eastRay = GetEastRay(position);
            // get masked blockers by masking blockers and ray
            maskedBlockers = blockers & eastRay;
            // get least significant bit from masked blockers
            leastSignificantBit = GetLeastSignificantBit(maskedBlockers);
            if (leastSignificantBit != 0)
            {
                // get partial ray from the lsb to the right
                var blockersEastRay = GetEastRay(leastSignificantBit);
                // check if msb is not associate than include msb to the ray
                var isNotAssociateBlocker = (leastSignificantBit & associateBlockers) != 0;
                if (isNotAssociateBlocker)
                {
                    blockersEastRay += leastSignificantBit;
                }

                // [ray] & [invert partial ray]
                eastRay &= ~blockersEastRay;
            }

            // go DOWN 
            var southRay = GetSouthRay(position);
            // get masked blockers by masking blockers and ray
            maskedBlockers = blockers & southRay;
            // get most significant bit from masked blockers
            var mostSignificantBit = GetMostSignificantBit(maskedBlockers);
            if (mostSignificantBit != 0)
            {
                // get partial ray from the msb to the down
                var blockersSouthRay = GetSouthRay(mostSignificantBit);
                // check if msb is not associate than include msb to the ray
                var isNotAssociateBlocker = (mostSignificantBit & associateBlockers) != 0;
                if (isNotAssociateBlocker)
                {
                    blockersSouthRay += mostSignificantBit;
                }

                // [ray] & [invert partial ray]
                southRay &= ~blockersSouthRay;
            }

            // go LEFT
            var westRay = GetWestRay(position);
            // get masked blockers by masking blockers and ray
            maskedBlockers = blockers & westRay;
            // get most significant bit from masked blockers
            leastSignificantBit = GetMostSignificantBit(maskedBlockers);
            if (leastSignificantBit != 0)
            {
                // get partial ray from the lsb to the left
                ulong blockersWestRay = GetWestRay(leastSignificantBit);
                // check if msb is not associate than include msb to the ray
                var isNotAssociateBlocker = (leastSignificantBit & associateBlockers) != 0;
                if (isNotAssociateBlocker)
                {
                    blockersWestRay += leastSignificantBit;
                }

                // [ray] & [invert partial ray]
                westRay &= ~blockersWestRay;
            }

            return northRay + eastRay + southRay + westRay;
        }

        private ulong GetBishopMask(ulong position, ulong blockers, ulong associateBlockers)
        {
            // go UP RIGHT
            var northEastRay = GetNorthEastRay(position);
            // get masked blockers by masking blockers and ray
            var maskedBlockers = blockers & northEastRay;
            // get least significant bit from masked blockers
            var leastSignificantBit = GetLeastSignificantBit(maskedBlockers);
            if (leastSignificantBit != 0)
            {
                // get partial ray from the lsb to the up right
                var blockersNorthEastRay = GetNorthEastRay(leastSignificantBit);
                // check if lsb is not associate than include lsb to the ray
                var isNotAssociateBlocker = (leastSignificantBit & associateBlockers) != 0;
                if (isNotAssociateBlocker)
                {
                    blockersNorthEastRay += leastSignificantBit;
                }

                // [ray] & [invert partial ray]
                northEastRay &= ~blockersNorthEastRay;
            }

            // go DOWN RIGHT
            var southEastRay = GetSouthEastRay(position);
            // get masked blockers by masking blockers and ray
            maskedBlockers = blockers & southEastRay;
            // get most significant bit from masked blockers
            var mostSignificantBit = GetMostSignificantBit(maskedBlockers);
            if (mostSignificantBit != 0)
            {
                // get partial ray from the msb to the down right
                var blockersSouthEastRay = GetSouthEastRay(mostSignificantBit);
                // check if msb is not associate than include msb to the ray
                var isNotAssociateBlocker = (mostSignificantBit & associateBlockers) != 0;
                if (isNotAssociateBlocker)
                {
                    blockersSouthEastRay += mostSignificantBit;
                }

                // [ray] & [invert partial ray]
                southEastRay &= ~blockersSouthEastRay;
            }

            // go DOWN LEFT
            var southWestRay = GetSouthWestRay(position);
            // get masked blockers by masking blockers and ray
            maskedBlockers = blockers & southWestRay;
            // get most significant bit from masked blockers
            mostSignificantBit = GetMostSignificantBit(maskedBlockers);
            if (mostSignificantBit != 0)
            {
                // get partial ray from the msb to the down left
                var blockersSouthWestRay = GetSouthWestRay(mostSignificantBit);
                // check if msb is not associate than include msb to the ray
                var isNotAssociateBlocker = (mostSignificantBit & associateBlockers) != 0;
                if (isNotAssociateBlocker)
                {
                    blockersSouthWestRay += mostSignificantBit;
                }

                // [ray] & [invert partial ray]
                southWestRay &= ~blockersSouthWestRay;
            }

            // go UP LEFT
            var northWestRay = GetNorthWestRay(position);
            // get masked blockers by masking blockers and ray
            maskedBlockers = blockers & northWestRay;
            // get least significant bit from masked blockers
            leastSignificantBit = GetLeastSignificantBit(maskedBlockers);
            if (leastSignificantBit != 0)
            {
                // get partial ray from the lsb to the up left
                ulong blockersNorthWestRay = GetNorthWestRay(leastSignificantBit);
                // check if lsb is not associate than include lsb to the ray
                var isNotAssociateBlocker = (leastSignificantBit & associateBlockers) != 0;
                if (isNotAssociateBlocker)
                {
                    blockersNorthWestRay += leastSignificantBit;
                }

                // [ray] & [invert partial ray]
                northWestRay &= ~blockersNorthWestRay;
            }

            return northEastRay + southEastRay + southWestRay + northWestRay;
        }

        private ulong GetQueenMask(ulong position, ulong blockers, ulong associateBlockers)
        {
            return GetBishopMask(position, blockers, associateBlockers) + GetRookMask(position, blockers, associateBlockers);
        }
        
        private ulong GetNorthRay(ulong position)
        {
            var (_, columnIndex) = GetCoordinates(position);
            var index = GetBoardIndex(position);
            var topIndexBorder = GetTopBorderIndex(columnIndex);
            ulong ray = 0;

            for (var i = index + BoardSize; i <= topIndexBorder; i += BoardSize)
            {
                ulong result = Convert.ToUInt64(Math.Pow(2, i));
                ray += result;
            }

            return ray;
        }

        private ulong GetSouthRay(ulong position)
        {
            var (_, columnIndex) = GetCoordinates(position);
            var index = GetBoardIndex(position);
            var bottomIndexBorder = GetBottomBorderIndex(columnIndex);
            ulong ray = 0;

            for (var i = index - BoardSize; i >= bottomIndexBorder; i -= BoardSize)
            {
                ulong result = Convert.ToUInt64(Math.Pow(2, i));
                if (result != position)
                {
                    ray += result;
                }
            }

            return ray;
        }

        private ulong GetWestRay(ulong position)
        {
            var (rowIndex, columnIndex) = GetCoordinates(position);
            var index = GetBoardIndex(position);
            var leftIndexBorder = GetLeftBorderIndex(rowIndex);
            ulong ray = 0;

            for (var i = leftIndexBorder; i < index; i++)
            {
                ulong result = Convert.ToUInt64(Math.Pow(2, i));
                ray += result;
            }

            return ray;
        }

        private ulong GetEastRay(ulong position)
        {
            var (rowIndex, columnIndex) = GetCoordinates(position);
            var index = GetBoardIndex(position);
            var rightIndexBorder = GetRightBorderIndex(rowIndex);
            ulong ray = 0;

            for (var i = index + 1; i <= rightIndexBorder; i++)
            {
                ulong result = Convert.ToUInt64(Math.Pow(2, i));
                ray += result;
            }

            return ray;
        }

        private ulong GetNorthWestRay(ulong position)
        {
            var (rowIndex, columnIndex) = GetCoordinates(position);

            var indexShift = 1;
            ulong ray = 0;
            for (var i = rowIndex + 1; i <= BoardSize; i++)
            {
                var leftIndexBorder = GetLeftBorderIndex(i);
                var currentIndex = GetBoardIndex(i, columnIndex - indexShift);

                if (currentIndex >= leftIndexBorder)
                {
                    ray += Convert.ToUInt64(Math.Pow(2, currentIndex));
                }

                indexShift++;
            }

            return ray;
        }

        private ulong GetNorthEastRay(ulong position)
        {
            var (rowIndex, columnIndex) = GetCoordinates(position);

            ulong ray = 0;
            var indexShift = 1;
            for (var i = rowIndex + 1; i <= BoardSize; i++)
            {
                var leftIndexBorder = GetLeftBorderIndex(i);
                var rightBorderIndex = GetRightBorderIndex(i);
                var currentIndex = GetBoardIndex(i, columnIndex + indexShift);

                if (currentIndex >= leftIndexBorder && currentIndex <= rightBorderIndex)
                {
                    ray += Convert.ToUInt64(Math.Pow(2, currentIndex));
                }

                indexShift++;
            }

            return ray;
        }

        private ulong GetSouthEastRay(ulong position)
        {
            var (rowIndex, columnIndex) = GetCoordinates(position);
            var indexShift = 1;
            ulong ray = 0;

            for (var i = rowIndex - 1; i > 0; i--)
            {
                var rightIndexBorder = GetRightBorderIndex(i);
                var currentIndex = GetBoardIndex(i, columnIndex + indexShift);

                if (currentIndex <= rightIndexBorder)
                {
                    ray += Convert.ToUInt64(Math.Pow(2, currentIndex));
                }

                indexShift++;
            }

            return ray;
        }

        private ulong GetSouthWestRay(ulong position)
        {
            var (rowIndex, columnIndex) = GetCoordinates(position);
            var indexShift = 1;
            ulong ray = 0;

            for (var i = rowIndex - 1; i > 0; i--)
            {
                var leftIndexBorder = GetLeftBorderIndex(i);
                var currentIndex = GetBoardIndex(i, columnIndex - indexShift);

                if (currentIndex >= leftIndexBorder)
                {
                    ray += Convert.ToUInt64(Math.Pow(2, currentIndex));
                }

                indexShift++;
            }

            return ray;
        }

        private Tuple<int, int> GetCoordinates(ulong position)
        {
            var indexOnTheBoard = GetBoardIndex(position);
            var rowIndex = indexOnTheBoard / BoardSize + 1;
            var columnIndex = 1;

            var leftIndexBorder = GetLeftBorderIndex(rowIndex);
            var rightIndexBorder = GetRightBorderIndex(rowIndex);

            for (var i = leftIndexBorder; i <= rightIndexBorder; i++)
            {
                if (indexOnTheBoard == i)
                {
                    break;
                }

                columnIndex++;
            }

            return new Tuple<int, int>(rowIndex, columnIndex);
        }

        private int GetLeftBorderIndex(int rowIndex)
        {
            return rowIndex == 0 ? 0 : (rowIndex - 1) * BoardSize;
        }

        private int GetRightBorderIndex(int rowIndex)
        {
            return rowIndex == 0 ? BoardSize - 1 : (rowIndex - 1) * BoardSize + (BoardSize - 1);
        }

        private int GetTopBorderIndex(int columnIndex)
        {
            return BoardSize * (BoardSize - 1) + (columnIndex - 1);
        }

        private int GetBottomBorderIndex(int columnIndex)
        {
            return columnIndex - 1;
        }

        private int GetBoardIndex(ulong position)
        {
            int rank = 0;
            while (position != 1)
            {
                position /= 2;
                rank++;
            }

            return rank;
        }

        private int GetBoardIndex(int rowIndex, int columnIndex)
        {
            return GetLeftBorderIndex(rowIndex) + columnIndex - 1;
        }

        private ulong GetMostSignificantBit(ulong n)
        {
            // To find the position of the most significant set bit 
            int k = (int)(Math.Log(n) / Math.Log(2));

            // To return the the value of the number with set bit at k-th position 
            return (ulong)(Math.Pow(2, k));
        }

        private ulong GetLeastSignificantBit(ulong n)
        {
            return n & ~(n - 1);
        }
    }
}