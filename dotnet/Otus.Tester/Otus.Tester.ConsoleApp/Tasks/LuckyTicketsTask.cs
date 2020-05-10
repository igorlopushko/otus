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

            Int64[] arr = null;
            for (var i = 0; i < k; i++)
            {
                arr = GetSumOfSquares(arr);
            }

            return arr.Sum(i => i * i).ToString();
        }

        private static Int64[] GetSumOfSquares(Int64[] arr)
        {
            var result = new List<Int64>();

            if (arr == null)
            {
                return new Int64[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            }

            int startIndex = 0;
            int endIndex = 0;
            for (int i = 0; i < arr.Length + ChainLength; i++)
            {
                Int64 newValue = 0;

                for (int j = startIndex; j <= endIndex; j++)
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