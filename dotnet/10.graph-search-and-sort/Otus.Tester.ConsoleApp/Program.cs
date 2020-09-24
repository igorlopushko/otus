using System;
using Otus.Tester.ConsoleApp.Base;
using Otus.Tester.ConsoleApp.Tasks;

namespace Otus.Tester.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var result = PrintMenu();
            ITask task;

            switch (result)
            {
                case 1:
                    task = new TopologicalSortDemukronTask();
                    break;
                default:
                    return;
            }

            var t = new TestRunner(task, "data/01.Topological Sort/");
            t.Run();

            Console.WriteLine("Completed. Press ENTER to exit.");
            Console.ReadLine();
        }

        private static int PrintMenu()
        {
            var rightPadding = 4;
            while (true)
            {
                Console.WriteLine("Выберите номер задания/решения:");
                Console.WriteLine("{0} - Топологическая сортировка Алгоритм Демукрона",
                    1.ToString().PadRight(rightPadding));

                int result;
                var answer = Console.ReadLine();
                if (int.TryParse(answer, out result)) return result;
            }
        }
    }
}