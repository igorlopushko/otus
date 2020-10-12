/*
Эта задача - подготовка к решению основной задачи.

Фермер уже знает, какой максимальной длины можно построить сарай из каждой клетки в сторону "севера".

Второй этап для решения задачи - определить подходящую ширину сарая, для этого нужно для каждой позиции вычислить, 
сколько клеток влево и вправо можно использовать для выбора прямоугольника той длины, которая доступна из этой клетки.

Например, если мы находимся в позиции [5],
из которой доступна длина сарая 2 (см. синий массив: A[5] = 2),
то позиция самой левой клетки с длиной не меньше 2 - это [3] (см. зелёный массив: L[5] = 3)
а позиция самой правой клетки с длиной не меньше 2 - это [9] (см. фиолетовый массив: R[5] = 9).

Начальные данные: массив A.
На первой строке записано число N от 1 до 10000.
Далее на N строчках записаны элементы массива A, каждое от 0 до 10000.

Вывод результата: массив L и R.
Построить две новые последовательности чисел по следующему правилу.

Массив L. L[j] = x.
Для каждого элемента A[j] определить индекс x наиболее отдаленного элемента слева, который больше или равен A[j].

И то же самое на в другую сторону:

Массив R. R[j] = x.
Для каждого элемента А[j] определить индекс x наиболее отдаленного элемента справа, который больше или равен А[j].

Индексы элементов начинаются с 0.
Вывести каждый массив на отдельной строчке, разделяя числа пробелами.
В конце строк пробелов быть не должно.
 */
using System;
using System.Collections.Generic;

namespace Otus.ProblemSolving.Task7
{
    class Program
    {
        private static int n;
        private static int[] array;
        private static Stack<int> stack;
        private static int[] left;
        private static int[] right;
        
        static void Main()
        {
            Read();
            CalculateRight();
            CalculateLeft();
            Print();
        }

        static void Read()
        {
            n = int.Parse(Console.ReadLine().Trim());
            array = new int[n];

            for (int i = 0; i < n; i++)
            {
                array[i] = int.Parse(Console.ReadLine());
            }
        }

        static void CalculateLeft()
        {
            left = new int[n];
            stack =  new Stack<int>();

            for (int i = n - 1 ; i >= 0; i--)
            {
                while (stack.Count > 0)
                {
                    if (array[i] < array[stack.Peek()])
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
        }

        static void CalculateRight()
        {
            right = new int[n];
            stack =  new Stack<int>();

            for (int i = 0; i < n; i++)
            {
                while (stack.Count > 0)
                {
                    if (array[i] < array[stack.Peek()])
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
        }
        
        static void Print()
        {
            int i;
            for (i = 0; i < n - 1; i++)
            {
                Console.Write(left[i] + " ");
            }
            Console.WriteLine(left[i]);
            for (i = 0; i < n - 1; i++)
            {
                Console.Write(right[i] + " ");
            }
            Console.WriteLine(right[i]);
        }
    }
}