using Otus.Tester.ConsoleApp.Tasks;
using Otus.Tester.ConsoleApp.Base;
using System;

namespace Otus.Tester.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //ITask task = new StringLengthTask();
            //string path = $"data/00.String/";

            //ITask task = new LuckyTicketsTask();
            //string path = $"data/01.Tickets/";

            //ITask task = new EuclidGcdMinus();
            //string path = $"data/02.GCD/";

            //ITask task = new EuclidGcdMod();
            //string path = $"data/02.GCD/";

            //ITask task = new EuclidGcdBitShift();
            //string path = $"data/02.GCD/";

            //ITask task = new PowerIterative();
            //string path = $"data/03.Power/";

            //ITask task = new PowerBinary();
            //string path = $"data/03.Power/";

            //ITask task = new IsPrimeIterativeTask();
            //string path = $"data/05.Primes/";

            //ITask task = new IsPrimeIterativeV2Task();
            //string path = $"data/05.Primes/";

            //ITask task = new IsPrimeIterativeV3Task();
            //string path = $"data/05.Primes/";

            ITask task = new IsPrimeIterativeHashSetTask();
            string path = $"data/05.Primes/";

            var t = new TestRunner(task, path);
            t.Run();
            Console.ReadLine();
        }
    }
}
