/*

На двух строчках записаны два целых числа.
Вывести их наибольший общий делитель.

Решить задачу разными способами.
2. Алгоритм Евклида через остаток

 */

using System.Numerics;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class EuclidGcdMod : ITask
    {
        public string Run(string[] data)
        {
            var a = BigInteger.Parse(data[0]);
            var b = BigInteger.Parse(data[1]);

            while (true)
            {
                if (a > b)
                {
                    a = a % b;
                }
                else
                {
                    b = b % a;
                }

                if (b == 0)
                    return a.ToString();
                if (a == 0)
                    return b.ToString();
            }
        }
    }
}
