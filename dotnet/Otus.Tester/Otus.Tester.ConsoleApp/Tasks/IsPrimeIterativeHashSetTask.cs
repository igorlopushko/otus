using System;
using System.Collections.Generic;
using System.Linq;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class IsPrimeIterativeHashSetTask : ITask
    {
        public string Run(string[] data)
        {
            var hashset = new HashSet<long>();
            long n = long.Parse(data[0]);

            int count = 0;
            for (long i = 1; i <= n; i++)
            {
                if (IsPrime(i, hashset))
                {
                    hashset.Add(i);
                    count++;
                }
            }

            return count.ToString();
        }

        private bool IsPrime(long number, HashSet<long> primes)
        {
            if (number % 2 == 0)
            {
                return number == 2;
            }

            var s = Math.Sqrt(number);

            if (number <= s)
            {
                if (primes.Contains(number))
                    return true;
            }

            long startIndex = primes.Count > 0 ? primes.Last() : 3;

            for (long i = startIndex; i <= s; i += 2)
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