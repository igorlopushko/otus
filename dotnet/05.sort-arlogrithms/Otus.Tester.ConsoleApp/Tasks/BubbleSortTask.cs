using System.Linq;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class BubbleSortTask : ITask
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
            for (var i = 0; i < array.Length; i++)
            {
                for (var j = 0; j < array.Length - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        var temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
            return array;
        }
    }
}
