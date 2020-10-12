/*
Фермер хочет построить на своей земле как можно больший по площади сарай.
Но на его участке есть деревья и хозяйственные постройки, которые он не хочет никуда переносить.
Для удобства представим ферму сеткой размера N × M.
Каждое из деревьев и построек размещается в одном или нескольких узлах сетки.

Для каждой клетки фермы вычислить, сколько находится свободных клеток подряд, если смотреть от этой клетки "вверх" матрицы.

Начальные данные:
На первой строке вводится размер матрицы N M (через пробел) от 1 до 1000.
На второй строке вводится количество построек T (от 0 до 10000) (занятых клеток).
Далее на T строчках вводится координаты построек
по два числа X Y, где 0 <= X < N; 0 <= Y < M для каждой занятой клетки.
Вывод результата: Матрица N x M из чисел -
сколько "выше" есть свободных клеток подряд, начиная с указанной клетки поля.
Всего M строк, по N чисел на каждой, записанных через пробел.
В конце строк лишних пробелов быть не должно!
 */

using System;

namespace Otus.ProblemSolving.Task6
{
    class Program
    {
        private static int n, m;
        private static int[,] matrix;
        private static int[] line;
        private static int[,] resultLines;
        
        static void Main()
        {
            Read();
            Calculate();
            Print();
        }

        static void Read()
        {   
            var sizes = Console.ReadLine().Trim().Split(' ');

            n = int.Parse(sizes[0]);
            m = int.Parse(sizes[1]);

            matrix = new int[n, m];
            resultLines = new int[n, m];

            var buildingNumber = int.Parse(Console.ReadLine().Trim());

            for (int i = 0; i < buildingNumber; i++)
            {
                var values = Console.ReadLine().Trim().Split(' ');

                if (values.Length == 2)
                {
                    int x = int.Parse(values[0]);
                    int y = int.Parse(values[1]);
                    matrix[x, y] = 1;
                }
            }
        }
        
        static void Calculate()
        {
            line = new int[n];
            for (int y = 0; y < m; y++)
            {
                GetHeightLine(y);
                
                for (int x = 0; x < line.Length; x++)
                {
                    resultLines[x, y] = line[x];
                }
            }
        }
        
        static void GetHeightLine(int y)
        {
            for (int x = 0; x < n; x++)
            {
                if (matrix[x, y] == 1)
                {
                    line[x] = 0;
                }
                else
                {
                    line[x]++;
                }
            }
        }
        
        static void Print()
        {
            for (int y = 0; y < m; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    Console.Write(resultLines[x, y] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}