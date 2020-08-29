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
                    task = new QuickSortTask();
                    break;
                case 2:
                    task = new MergeSortTask();
                    break;
                case 3:
                    task = new CountingSortTask();
                    break;
                case 4:
                    task = new RadixSortTask();
                    break;
                case 5:
                    task = new BucketSortTask();
                    break;
                default:
                    return;
            }

            Console.WriteLine("Random Array.");
            var t = new TestRunner(task, $"data/0.random/");
            t.Run();
            
            Console.WriteLine("Digits Array.");
            t = new TestRunner(task, $"data/1.digits/");
            t.Run();

            Console.WriteLine("Sorted Array.");
            t = new TestRunner(task, $"data/2.sorted/");
            t.Run();

            Console.WriteLine("Reverse Array.");
            t = new TestRunner(task, $"data/3.revers/");
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
                Console.WriteLine("{0} - Сортировка \"Быстрая\"", 1.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Сортировка \"Слиянием\"", 2.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Сортировка \"Подсчетом\"", 3.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Поразряданая сортировка", 4.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Ведерная сортировка", 5.ToString().PadRight(rightPadding));

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