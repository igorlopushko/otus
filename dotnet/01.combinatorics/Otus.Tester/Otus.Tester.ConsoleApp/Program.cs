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

            ITask task = new LuckyTicketsTask();
            string path = $"data/01.Tickets/";

            var t = new TestRunner(task, path);
            t.Run();
            Console.ReadLine();
        }
    }
}
