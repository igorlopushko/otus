using System.Linq;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class HeapSortTask : ITask
    {
        public string[] Run(string[] data)
        {
            var inputString = data[1].Split(" ");
            var inputArray = new int[inputString.Length];

            for (int i = 0; i < inputString.Length; i++)
            {
                inputArray[i] = int.Parse(inputString[i]);
            }

            var result = inputArray;

            if (inputArray.Length > 1)
            {
                result = Sort(inputArray);
            }

            return new[]
            {
                string.Join(" ", result.Select(x => x.ToString()).ToArray())
            };
        }

        private int[] Sort(int[] array)
        {
            // convert array to heap
            for (var i = array.Length / 2 - 1; i >= 0; i--)
            {
                array = Heapify(array, i, array.Length);
            }

            // find max element and put it to the end and reheapify array
            for (var i = array.Length - 1; i >= 0; i--)
            {
                array = Swap(array, 0, i);
                array = Heapify(array,0, i);
            }

            return array;
        }

        private static int[] Heapify(int[] array, int root, int size)
        {
            // find indexes of left and right leave of the subtree
            var leftIndex = 2 * root + 1;
            var rightIndex = leftIndex + 1;

            var x = root;

            // compare root and left element
            if (leftIndex < size && array[x] < array[leftIndex])
            {
                x = leftIndex;
            }
            // compare root and right element
            if (rightIndex < size && array[x] < array[rightIndex])
            {
                x = rightIndex;
            }

            // if root not changed exit
            if (x == root)
            {
                return array;
            }

            array = Swap(array, x, root);
            array = Heapify(array, x, size);

            return array;
        }

        private static int[] Swap(int[] array, int a, int b)
        {
            var temp = array[a];
            array[a] = array[b];
            array[b] = temp;

            return array;
        }
    }
}