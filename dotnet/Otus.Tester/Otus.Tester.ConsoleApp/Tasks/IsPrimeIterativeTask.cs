using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class IsPrimeIterativeTask : ITask
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
            int count = 0;
            for (long i = 1; i <= number; i++)
            {
                
                if (number % i == 0)
                {
                    count++;
                }
            }

            return count == 2;
        }
    }
}