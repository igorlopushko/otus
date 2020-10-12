using System;
using System.Collections.Generic;

namespace Otus.ProblemSolving.Task8
{
    class Program
    {
        private static int n, m;
        private static int[,] matrix;
        
        static void Main()
        {
            Read();
            int result = Calculate();
            Print(result);
        }
        
        static void Read()
        {   
            string[] sizes = Console.ReadLine().Trim().Split(' ');

            n = int.Parse(sizes[0]);
            m = int.Parse(sizes[1]);

            matrix = new int[n, m];

            int buildingNumber = int.Parse(Console.ReadLine().Trim());

            for (int i = 0; i < buildingNumber; i++)
            {
                string[] values = Console.ReadLine().Trim().Split(' ');

                if (values.Length == 2)
                {
                    int x = int.Parse(values[0]);
                    int y = int.Parse(values[1]);
                    matrix[x, y] = 1;
                }
            }
        }

        static int Calculate()
        {
            int[] row = new int[n];
            int totalMaxSquare = -1;
            for (int y = 0; y < m; y++)
            {
                row = GetRowHeight(row, y);
                int[] left = CalculateLeft(row);
                int[] right = CalculateRight(row);
                int square = FindMaxFowSquare(row, left, right);
                if (square > totalMaxSquare)
                {
                    totalMaxSquare = square;
                }
            }
            
            return totalMaxSquare;
        }
        
        static int FindMaxFowSquare(int[] row, int[] left, int[] right)
        {
            int maxSquare = -1;
            for (int i = 0; i < n; i++)
            {
                int square = (right[i] - left[i] + 1) * row[i];
                if (square > maxSquare)
                {
                    maxSquare = square;
                }
            }

            return maxSquare;
        }
        
        static int[] GetRowHeight(int[] row, int y)
        {
            for (int x = 0; x < n; x++)
            {
                if (matrix[x, y] == 1)
                {
                    row[x] = 0;
                }
                else
                {
                    row[x]++;
                }
            }
            
            return row;
        }
        
        static int[] CalculateLeft(int[] row)
        {
            int[] left = new int[n];
            Stack<int> stack =  new Stack<int>();

            for (int i = n - 1 ; i >= 0; i--)
            {
                while (stack.Count > 0)
                {
                    if (row[i] < row[stack.Peek()])
                    {
                        left[stack.Pop()] = i + 1;
                    }
                    else
                    {
                        break;
                    }
                }
                
                stack.Push(i);
            }

            while (stack.Count > 0)
            {
                left[stack.Pop()] = 0;
            }

            return left;
        }

        static int[] CalculateRight(int[] row)
        {
            int[] right = new int[n];
            Stack<int> stack =  new Stack<int>();

            for (int i = 0; i < n; i++)
            {
                while (stack.Count > 0)
                {
                    if (row[i] < row[stack.Peek()])
                    {
                        right[stack.Pop()] = i - 1;
                    }
                    else
                    {
                        break;
                    }
                }
                
                stack.Push(i);
            }
            
            while (stack.Count > 0)
            {
                right[stack.Pop()] = n - 1;
            }
            
            return right;
        }

        static void Print(int result)
        {
            Console.WriteLine(result);
        }
    }
}