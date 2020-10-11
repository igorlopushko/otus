/*

Каждый элемент квадратной матрицы размеренности N x N равен нулю, либо единице. Найдите количество «островов», образованных единицами. 
Под «островом» понимается группа единиц (либо одна единица), со всех сторон окруженная нулями (или краями матрицы). 
Единицы относятся к одному «острову», если из одной из них можно перейти к другой «наступая» на единицы, расположенные в соседних клетках. 
Соседними являются клетки, граничащие по горизонтали или вертикали.

Входные данные
В первой строке вводится натуральное число N не больше 100 - размер квадратной матрицы.
В следующих N строках задаются элементы матрицы, по N чисел через пробел на каждой.

Выходные данные
Вывести единственное число - количество островов.
 
 */
using System;

namespace Otus.ProblemSolving.Task4
{
    class Program
    {
        private static int[,] matrix;
        private static int n;
        
        static void Main()
        {
            n = int.Parse(Console.ReadLine());

            matrix = new int[n, n];

            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                string[] values = Console.ReadLine().Trim().Split(' ');
                
                for (int y = 0; y < values.Length; y++)
                {
                    matrix[x, y] = int.Parse(values[y]);
                }
            }

            int islands = 0;
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    if (matrix[x, y] == 1)
                    {
                        islands++;
                        Go(x, y);
                    }
                }
            }
            
            Console.WriteLine(islands);
        }

        static void Go(int x,  int y)
        {
            if (x < 0 || x >= n) return;
            if (y < 0 || y >= n) return;
            if (matrix[x, y] == 0) return;
            matrix[x, y] = 0;
            Go(x - 1, y);
            Go(x + 1, y);
            Go(x, y - 1);
            Go(x , y + 1);
        }
    }
}