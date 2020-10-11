using System;
using System.Collections.Generic;
using System.Text;
using Otus.DataStructure;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class FloydWarshallAlgorithmTask : ITask
    {
        public string[] Run(string[] data)
        {
            int[,] adjacencyMatrix = new int[data.Length, data.Length];

            for (var i = 0; i < data.Length; i++)
            {
                var values = data[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < values.Length; j++)
                {
                    adjacencyMatrix[i, j] = int.Parse(values[j]);
                }
            }
            
            var g = new Graph(adjacencyMatrix);
            var result = g.GetShortestPathFloydWarshall();
            
            var stringResult = new List<string>();
            for (var i = 0; i < result.GetLength(0); i++)
            {
                var line = new StringBuilder();
                for (var j = 0; j < result.GetLength(1); j++)
                {
                    line.Append($"{ result[i, j]}");

                    if (j < result.GetLength(1) - 1)
                    {
                        line.Append($" ");
                    }
                }
                
                stringResult.Add(line.ToString());
            }
            
            return stringResult.ToArray();
        }
    }
}