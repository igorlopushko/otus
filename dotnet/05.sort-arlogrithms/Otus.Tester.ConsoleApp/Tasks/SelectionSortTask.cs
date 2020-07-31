using System.Linq;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class SelectionSortTask : ITask
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

        public int[] Sort(int[] array)
        {
            for (var i = 0; i < array.Length - 1; i++)
            {
                // Find the minimum element in unsorted array 
                var minIndex = i;
                for (var j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }

                // Swap found minimum element with the first element 
                var temp = array[minIndex];
                array[minIndex] = array[i];
                array[i] = temp;
            }

            return array;
        }
    }
}
