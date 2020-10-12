/*
Фермер хочет построить на своей земле как можно больший по площади сарай.
Но на его участке есть деревья и хозяйственные постройки, которые он не хочет никуда переносить.
Для удобства представим ферму сеткой размера N × M.
Каждое из деревьев и построек размещается в одном или нескольких узлах сетки.
Найти максимально возможную площадь сарая и где он может размещаться.

Начальные данные: Вводится матрица размера N × M из 0 и 1.
1 соответствует постройке, 0 - пустой клетке.
На первой строке вводится размер матрицы N M (через пробел) от 1 до 30.
Элементы матрицы вводятся на M строках по N элементов через пробел.
Вывод результата: 1 число, соответствующее максимальной площади сарая (количество ячеек).
 */
using System;

namespace Otus.ProblemSolving.Task5
{
    class Program
    {
        private static int n, m;
        private static int[,] matrix;
        
        static void Main()
        {
            Read();
            var result = Calculate();
            Print(result);
        }

        static void Read()
        {   
            var sizes = Console.ReadLine().Trim().Split(' ');

            n = int.Parse(sizes[0]);
            m = int.Parse(sizes[1]);

            matrix = new int[n, m];

            for (int x = 0; x < m; x++)
            {
                var values = Console.ReadLine().Trim().Split(' ');
                for (int y = 0; y < values.Length; y++)
                {
                    matrix[y, x] = int.Parse(values[y]);
                }
            }
        }
        
        static int Calculate()
        {
            int maxSquare = -1;
            for (int y = 0; y < m; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    int square = GetMaxSquare(x, y);

                    if (maxSquare < square)
                        maxSquare = square;
                }
            }

            return maxSquare;
        }

        static int GetMaxSquare(int x, int y)
        {
            int minHeight = GetHeight(x, y);
            if (minHeight == 0)
                return 0;
            int maxSquare = minHeight == 1 ? minHeight : minHeight + 1;
            for (int xw = x + 1; xw < n; xw++)
            {
                int height = GetHeight(xw, y);
                if (height == 0)
                {
                    return maxSquare;
                }

                if (minHeight > height)
                {
                    minHeight = height;
                }

                int square = (xw - x + 1) * minHeight;
                if (square > maxSquare)
                {
                    maxSquare = square;
                }
            }

            return maxSquare;
        }
        
        static int GetHeight(int x, int y)
        {
            int yh = y;
            while (yh < m && matrix[x, yh] == 0)
            {
                yh++;
            }

            return yh - y;
        }
        
        static void Print(int result)
        {
            Console.WriteLine(result);
        }
    }
}