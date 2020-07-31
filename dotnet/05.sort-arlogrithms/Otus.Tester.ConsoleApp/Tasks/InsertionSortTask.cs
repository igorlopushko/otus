using System.Linq;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class InsertionSortTask : ITask
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
            for (var i = 0; i < array.Length - 1; i++)
            {
                for (var j = i + 1; j > 0; j--)
                {
                    // compare current element and previous element
                    if (array[j - 1] > array[j])
                    {
                        // swap current with the previous element if current is smaller
                        var temp = array[j - 1];
                        array[j - 1] = array[j];
                        array[j] = temp;
                    }
                }
            }

            return array;
        }
    }
}
