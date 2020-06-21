using System.Linq;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks._05.Primes
{
    public class IsPrimeSieveOfEratosthenes : ITask
    {
        public string Run(string[] data)
        {
            var n = int.Parse(data[0]);

            return GetCountOfPrimes(n).ToString();
        }

        private int GetCountOfPrimes(int n)
        {
            // Create a boolean array "prime[0..n]" and initialize all entries it as TRUE.
            // A value in primes[i] will finally be FALSE if it is NOT a prime, else TRUE. 
            var primes = new bool[n + 1];
            for (var i = 0; i < primes.Length; i++)
            {
                primes[i] = true;
            }

            for (var p = 2; p * p <= n; p++)
            {
                // If item is not changed, then it is a prime 
                if (!primes[p])
                {
                    continue;
                }

                // Update all multiples of p 
                for (var i = p * p; i <= n; i += p)
                {
                    primes[i] = false;
                }
            }

            return primes.Skip(2).Count(x => x);
        }
    }
}
