using System;
using System.Collections.Generic;
using Otus.DataStructure;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class PrimAlgorithmTask : ITask
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
            var result = g.GetMinSpanningTreePrim();
            
            var stringResult = new List<string>();
            foreach (var edge in result)
            {
                stringResult.Add($"{edge.Source + 1} {edge.Destination + 1} {edge.Rank}");
            }
            
            return stringResult.ToArray();
        }
    }
}