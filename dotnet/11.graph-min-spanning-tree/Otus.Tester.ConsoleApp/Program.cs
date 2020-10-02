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
            var path = "";

            switch (result)
            {
                case 1:
                    task = new KruskalAlgorithmTask();
                    break;
                case 2:
                    task = new PrimAlgorithmTask();
                    break;
                case 3:
                    task = new BoruvkaAlgorithmTask();
                    break;
                default:
                    return;
            }

            path = "data/";
            var t = new TestRunner(task, path);
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
                Console.WriteLine("{0} - Нахождение минимального остовного дерева Алгоритм Краскала", 1.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Нахождение минимального остовного дерева Алгоритм Прима", 2.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Нахождение минимального остовного дерева Алгоритм Буроки", 3.ToString().PadRight(rightPadding));

                int result;
                var answer = Console.ReadLine();
                if (int.TryParse(answer, out result)) return result;
            }
        }
    }
}