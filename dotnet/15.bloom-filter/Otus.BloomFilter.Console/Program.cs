using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Otus.BloomFilter.Implementation;

namespace Otus.BloomFilter.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var capacity = 2000000;
            
            var filter = new Filter<string>(capacity);
            
            var count = InitFilter(filter);
            var numberOfUniqueWords = NumberOfUniqueWords();
            var errorPercentage = ErrorPercentage(filter, capacity);
            
            System.Console.WriteLine($"Number of words: {count}");
            System.Console.WriteLine($"Number of unique words: {numberOfUniqueWords}");
            System.Console.WriteLine("Error percentage: {0:0.00}%", errorPercentage);
            
            while (true)
            {
                System.Console.Write("Enter a word: ");
                var word = System.Console.ReadLine();
                
                if (String.IsNullOrEmpty(word))
                {
                    continue;
                }
                
                System.Console.WriteLine("Word is in the text: " + (filter.Contains(word.ToLower()) ? "Yes" : "No"));
            }
        }

        private static long InitFilter(Filter<string> filter)
        {
            string line;
            long count = 0;
            var file = new StreamReader("data/sample.txt");  
            while((line = file.ReadLine()) != null)
            {
                var words = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    var value = GetWord(word);
                    filter.Add(value);
                    count++;
                }
            }

            return count;
        }

        private static long NumberOfUniqueWords()
        {
            string line;
            var dictionary = new HashSet<string>();
            var file = new StreamReader("data/sample.txt");  
            while((line = file.ReadLine()) != null)
            {
                var words = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    var value = GetWord(word);
                    
                    if (string.IsNullOrEmpty(value))
                    {
                        continue;
                    }
                    
                    if(!dictionary.Contains(value));
                    {
                        dictionary.Add(value);
                    }
                }
            }

            return dictionary.Count;
        }

        private static decimal ErrorPercentage(Filter<string> filter, long testNumber)
        {
            string line;
            
            // read data set
            var sampleDictionary = new HashSet<string>();
            var file = new StreamReader("data/sample.txt");  
            while((line = file.ReadLine()) != null)
            {
                var words = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    var value = GetWord(word);
                    
                    if (string.IsNullOrEmpty(value))
                    {
                        continue;
                    }
                    
                    if(!sampleDictionary.Contains(value));
                    {
                        sampleDictionary.Add(value);
                    }
                }
            }
            
            // read dictionary
            file = new StreamReader("data/distionary.txt");
            var dictionary = new Dictionary<long, string>();
            long wordCount = 0;
            while((line = file.ReadLine()) != null)
            {
                dictionary.Add(wordCount, line);
                wordCount++;
            }

            long errorCount = 0;
            for (long i = 0; i < testNumber; i++)
            {
                var random = new Random();
                var index = LongRandom(0, dictionary.Count, random);
                var word = dictionary[index].ToLower();

                if (filter.Contains(word) && !sampleDictionary.Contains(word) ||
                    !filter.Contains(word) && sampleDictionary.Contains(word))
                {
                    errorCount++;
                }
            }


            return Convert.ToDecimal(errorCount / Convert.ToDecimal(sampleDictionary.Count / 100));
        }
        
        private static long LongRandom(long min, long max, Random rand) 
        {
            var buffer = new byte[8];
            rand.NextBytes(buffer);
            var longRand = BitConverter.ToInt64(buffer, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }

        private static string GetWord(string word)
        {
            return Regex.Replace(word, @"[^0-9a-zA-Z]+", "").ToLower();
        }
    }
}