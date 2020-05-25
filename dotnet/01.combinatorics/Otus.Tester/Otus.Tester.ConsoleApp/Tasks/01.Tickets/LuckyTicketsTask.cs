/*
Счастливые билеты 20

Билет с 2N значным номером считается счастливым,
если сумма N первых цифр равна сумме последних N цифр.
Посчитать, сколько существует счастливых 2N-значных билетов.

Начальные данные: число N от 1 до 10.
Вывод результата: количество 2N-значных счастливых билетов.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class LuckyTicketsTask : ITask
    {
        private const int ChainLength = 9;

        public string Run(string[] data)
        {
            int k = int.Parse(data[0]);

            long[] arr = null;
            for (var i = 0; i < k; i++)
            {
                arr = GetSumOfSquares(arr);
            }

            return arr.Sum(i => i * i).ToString();
        }

        private static long[] GetSumOfSquares(long[] arr)
        {
            var result = new List<long>();

            if (arr == null)
            {
                return new long[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            }

            var startIndex = 0;
            var endIndex = 0;
            for (var i = 0; i < arr.Length + ChainLength; i++)
            {
                long newValue = 0;

                for (var j = startIndex; j <= endIndex; j++)
                {
                    if (j >= arr.Length)
                    {
                        continue;
                    }
                    newValue += arr[j];
                }

                result.Add(newValue);

                if (endIndex >= ChainLength)
                {
                    startIndex++;
                }

                endIndex++;
            }

            return result.ToArray();
        }
    }
}