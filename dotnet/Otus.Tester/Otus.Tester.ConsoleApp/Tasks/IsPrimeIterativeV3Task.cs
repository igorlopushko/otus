using System;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class IsPrimeIterativeV3Task : ITask
    {
        public string Run(string[] data)
        {
            long n = long.Parse(data[0]);

            int count = 0;
            for (long i = 1; i <= n; i++)
            {
                if (IsPrime(i))
                {
                    count++;
                }
            }

            return count.ToString();
        }

        private bool IsPrime(long number)
        {
            if (number % 2 == 0)
            {
                return number == 2;
            }

            var s = Math.Sqrt(number);
            for (long i = 3; i <= s; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}