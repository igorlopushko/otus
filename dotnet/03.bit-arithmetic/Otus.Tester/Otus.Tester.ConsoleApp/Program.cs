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
                case 1:
                    task = new KingTask();
                    path = $"data/0.BITS/1.Bitboard - Король/";
                    break;
                case 2:
                    task = new KnightTask();
                    path = $"data/0.BITS/2.Bitboard - Конь/";
                    break;
                case 3:
                    task = new FenParserTask();
                    path = $"data/0.BITS/3.Bitboard - FEN/";
                    break;
                case 4:
                    task = new FenParserTask();
                    path = $"data/0.BITS/4.Bitboard - Дальнобойщики/";
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
                Console.WriteLine("{0} - Прогулка Короля - BITS", 1.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Конь - BITS", 2.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - FEN - BITS", 3.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Дальнобойщики - BITS", 4.ToString().PadRight(rightPadding));

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
