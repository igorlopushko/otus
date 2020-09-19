using Otus.Tester.ConsoleApp.Tasks;
using Otus.Tester.ConsoleApp.Base;
using System;

namespace Otus.Tester.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = PrintMenu();
            ITask task;

            switch (result)
            {
                case 1:
                    task = new SegmentSumTask();
                    break;
                default:
                    return;
            }

            var t = new TestRunner(task, $"data/");
            t.Run();

            Console.WriteLine("Completed. Press ENTER to exit.");
            Console.ReadLine();
        }

        private static int PrintMenu()
        {
            int rightPadding = 4;
            while (true)
            {
                Console.WriteLine("Выберите номер задания/решения:");
                Console.WriteLine("{0} - Сумма на отрезке", 1.ToString().PadRight(rightPadding));

                int result;
                var answer = Console.ReadLine();
                if (int.TryParse(answer, out result))
                {
                    return result;
                }
            }
        }
    }
}