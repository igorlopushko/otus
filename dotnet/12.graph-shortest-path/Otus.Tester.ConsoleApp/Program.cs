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
                    task = new DijkstraAlgorithmTask();
                    path = "data/0.Dijkstra/";
                    break;
                case 2:
                    task = new FloydWarshallAlgorithmTask();
                    path = "data/1.Floyd-Warshall/";
                    break;
                case 3:
                    task = new BellmanFordAlgorithmTask();
                    path = "data/2.Bellman-Ford/";
                    break;
                default:
                    return;
            }
            
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
                Console.WriteLine("{0} - Нахождение минимального пути Алгоритм Дейкстра", 1.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Нахождение минимального пути Алгоритм Флойда-Уоршелла", 2.ToString().PadRight(rightPadding));
                Console.WriteLine("{0} - Нахождение минимального пути Алгоритм Белмана-Форда", 3.ToString().PadRight(rightPadding));

                int result;
                var answer = Console.ReadLine();
                if (int.TryParse(answer, out result)) return result;
            }
        }
    }
}