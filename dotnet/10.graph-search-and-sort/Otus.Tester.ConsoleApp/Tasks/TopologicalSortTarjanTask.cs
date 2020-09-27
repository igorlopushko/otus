using System;
using System.Collections.Generic;
using System.Text;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class TopologicalSortTarjanTask : ITask
    {
        public string[] Run(string[] data)
        {
            var matrix = new int[data.Length, data.Length];
            
            for (var i = 0; i < data.Length; i++)
            {
                var values = data[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var valuesCount = values.Length > 1 ? 1 : 0;

                for (var j = 0; j < data.Length; j++)
                    if (values.Length > 1 &&
                        valuesCount < values.Length &&
                        int.Parse(values[valuesCount]) - 1 == j)
                    {
                        matrix[i, j] = 1;
                        valuesCount++;
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
            }

            var result = Tarjan(matrix);
            var stringResult = new List<string>();

            var str = new StringBuilder();
            
            for (var i = 0; i < result.Length; i++)
            {
                str.Append(result[i]);
                if (i < result.Length - 1)
                {
                    str.Append(" ");
                }
                
            }
            stringResult.Add(str.ToString());
            
            return stringResult.ToArray();
        }

        private int[] Tarjan(int[,] adjacencyMatrix)
        {
            var stack = new Otus.DataStructure.Stack<int>();
            var visited = new HashSet<int>();

            while (visited.Count < adjacencyMatrix.GetLength(0))
            {
                for (var vertix = 0; vertix < adjacencyMatrix.GetLength(0); vertix++)
                {
                    Dfs(vertix, adjacencyMatrix, stack, visited);
                }
            }

            var result = new int[stack.Size];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = stack.Pop() + 1;
            }

            return result;
        }

        private void Dfs(int vertix, int[,] adjacencyMatrix, Otus.DataStructure.Stack<int> stack, HashSet<int> visited)
        {
            if (visited.Contains(vertix)) return;
            
            // get child vertices
            var childVertices = new List<int>();
            for (var col = 0; col < adjacencyMatrix.GetLength(1); col++)
            {
                if (adjacencyMatrix[vertix, col] == 1 && !visited.Contains(adjacencyMatrix[vertix, col]))
                {
                    childVertices.Add(col);
                }
            }

            // if no child vertices stop and add to stack
            if (childVertices.Count == 0)
            {
                stack.Push(vertix);
            }

            // go down to children
            foreach (var child in childVertices)
            {
                Dfs(child, adjacencyMatrix, stack, visited);
            }

            // mark as visited
            visited.Add(vertix);
            
            // add to the result if not already added
            if (!stack.Contains(vertix))
            {
                stack.Push(vertix);
            }
        }
    }
}