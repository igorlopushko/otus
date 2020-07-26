using System;
using System.Diagnostics;
using Otus.DataStructures;

namespace Otus.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            IArray<int> single = new SingleArray<int>();
            IArray<int> vector = new VectorArray<int>(1000);
            IArray<int> factor = new FactorArray<int>();
            IArray<int> matrix = new MatrixArray<int>();
            IArray<int> space = new SpaceArray<int>();;
            
            TestPut(single, 1000);
            TestPut(vector, 1000);
            TestPut(factor, 1000);
            TestPut(matrix, 1000);
            TestPut(space,  1000);

            single.Add(999, 10);
            single.Add(999, 99);
            single.Remove(101);
            single.Remove(100);
            single.Remove(10);

            vector.Add(999, 10);
            vector.Add(999, 99);
            vector.Remove(101);
            vector.Remove(100);
            vector.Remove(10);

            factor.Add(999, 10);
            factor.Add(999, 99);
            factor.Remove(101);
            factor.Remove(100);
            factor.Remove(10);

            matrix.Add(999, 10);
            matrix.Add(999, 99);
            matrix.Remove(101);
            matrix.Remove(100);
            matrix.Remove(10);
            
            // space 60
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            
            // space 70
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            
            // space 80
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            
            // space 90
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            
            // space 100
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            
            // space 150
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            space.Add(999, 50);
            space.Add(777, 50);
            
            
            //space.Remove(101);
            //space.Remove(100);
            //space.Remove(10);

            Console.ReadLine();
        }

        private static void TestPut(IArray<int> array, int total)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            for (int i = 0; i < total; i++)
            {
                array.Add(i);
            }

            sw.Stop();

            Console.WriteLine(array + " Test Put: " + sw.ElapsedMilliseconds + " (ms)");
        }
    }
}
