using System.Linq;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class CountingSortTask : ITask
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
            var countingArray = new int[array.Length];

            foreach (var item in array)
            {
                if (item > countingArray.Length - 1)
                {
                    // extend counting array
                    var temp = new int[item + 1];
                    for (var j = 0; j < countingArray.Length; j++)
                    {
                        temp[j] = countingArray[j];
                    }

                    countingArray = temp;
                }
                
                countingArray[item]++;
            }

            var result = new int[countingArray.ToList().Sum()];

            var itemIndex = 0;
            for (var i = 0; i < countingArray.Length; i++)
            {
                if (countingArray[i] <= 0) continue;
                
                var elementCount = 0;
                while (elementCount < countingArray[i])
                {
                    result[itemIndex] = i;
                    elementCount++;
                    itemIndex++;
                }
            }
            
            return result;
        }
    }
}