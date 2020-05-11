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
            //string path = $"data/0.String/";

            //ITask task = new LuckyTicketsTask();
            //string path = $"data/1.Tickets/";

            //ITask task = new EuclidGcdMinus();
            //string path = $"data/2.GCD/";

            //ITask task = new EuclidGcdMod();
            //string path = $"data/2.GCD/";

            ITask task = new EuclidGcdBitShift();
            string path = $"data/2.GCD/";


            var t = new TestRunner(task, path);
            t.Run();
            Console.ReadLine();
        }
    }
}
