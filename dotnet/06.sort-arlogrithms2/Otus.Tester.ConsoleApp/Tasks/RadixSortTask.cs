using System.Linq;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class RadixSortTask : ITask
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
            // Find the maximum number to know number of digits  
            var max = GetMax(array);
            
            for (var exp = 1; max / exp > 0; exp *= 10)
            {
                array = CountSort(array, exp);
            }

            return array;
        }  
        
        private int GetMax(int[] array)  
        {  
            var max = array[0];
            for (var i = 1; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }
            
            return max;  
        }  
        
        private int[] CountSort(int[] array, int exp)
        {
            var result = new int[array.Length];
            var count = new int[10]; 
          
            //initializing all elements of count to 0 
            for (var i = 0; i < 10; i++)
            {
                count[i] = 0;
            } 
        
            // store count of occurrences in count[]  
            foreach (var item in array)
            {
                count[(item / exp) % 10]++;
            }  
        
            // change count[i] so that count[i] now contains actual  
            // position of this digit in output[]  
            for (var i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }  
        
            // build the result array  
            for (var i = array.Length - 1; i >= 0; i--)
            {
                result[count[(array[i] / exp) % 10] - 1] = array[i];
                count[(array[i] / exp) % 10]--;
            }

            return result;
        }
    }
}