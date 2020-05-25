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
            ITask task = null;
            string path = string.Empty;

            switch (result)
            {
                case 0:
                    task = new StringLengthTask();
                    path = $"data/00.String/";
                    break;
                case 1:
                    task = new LuckyTicketsTask();
                    path = $"data/01.Tickets/";
                    break;
                default:
                    return;
            }

            var t = new TestRunner(task, path);
            t.Run();
            Console.ReadLine();
        }

        private static int PrintMenu()
        {
            int rightPadding = 4;
            while (true)
            {
                Console.WriteLine("Выберите номер задания/решения:");
                Console.WriteLine("{0} - Вычислить длину строки", 0.ToString().PadRight(rightPadding));

                Console.WriteLine("{0} - Вычислить кол-во счастливых билетов", 1.ToString().PadRight(rightPadding));

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
