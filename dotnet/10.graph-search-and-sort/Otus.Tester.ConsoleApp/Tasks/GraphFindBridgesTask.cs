using System;
using System.Collections.Generic;
using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class GraphFindBridgesTask : ITask
    {
        private int _time = 0;
        
        public string[] Run(string[] data)
        {
            var adjacencyVector = new int[data.Length][];

            for (var i = 0; i < data.Length; i++)
            {
                var values = data[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (values.Length > 1)
                {
                    var list = new List<int>();
                    for (int j = 1; j < values.Length; j++)
                    {
                        list.Add(int.Parse(values[j]) - 1);
                    }
                    
                    adjacencyVector[i] = list.ToArray();
                }
            }

            var result = FindBridges(adjacencyVector);
            var stringResult = new List<string>();

            foreach (var bridge in result)
            {
                stringResult.Add(bridge.Item1 + " " + bridge.Item2);
            }
            
            return stringResult.ToArray();
        }

        private Tuple<int, int>[] FindBridges(int[][] adjacencyVector)
        {
            var visited = new HashSet<int>();
            var disc = new int[adjacencyVector.GetLength(0)];
            var low = new int[adjacencyVector.GetLength(0)];
            var parent = new int[adjacencyVector.GetLength(0)];
            var result = new List<Tuple<int, int>>();
            
            for (var i = 0; i < parent.Length; i++)
            {
                parent[i] = -1;
            }

            for (var i = 0; i < adjacencyVector.GetLength(0); i++)
            {
                if (!visited.Contains(i))
                {
                    Dfs(i, adjacencyVector, visited, disc, low, parent, result);
                }
            }

            var temp = new List<Tuple<int, int>>();
            foreach (var t in result)
            {
                temp.Add(new Tuple<int, int>(t.Item1 + 1, t.Item2 + 1));
            }
            
            return temp.ToArray();
        }

        private void Dfs(int vertix, int[][] adjacencyVector, HashSet<int> visited, 
            int[] disc, int[] low, int[] parent, ICollection<Tuple<int, int>> result)
        {
            // mark current vertix as visited
            visited.Add(vertix);

            // initialize discovery time and low value  
            disc[vertix] = low[vertix] = ++_time;

            foreach (var child in adjacencyVector[vertix])
            {
                // if CHILD is not visited yet, then make it a child of current VERTIX n DFS tree and recur for it.
                if (!visited.Contains(child))
                {
                    parent[child] = vertix;
                    Dfs(child, adjacencyVector, visited, disc, low, parent, result); 
                    
                    // check if the subtree rooted with CHILD has a connection to one of the ancestors of VERTIX.
                    low[vertix] = Math.Min(low[vertix], low[child]);
                    
                    // if the lowest vertex reachable from subtree under VERTIX is below CHILD in DFS tree,
                    // then VERTIX-CHILD is a bridge.
                    if (low[child] > disc[vertix])
                    {
                        result.Add(new Tuple<int, int>(vertix, child));
                    }
                }
                // update low value of VERTIX for parent function calls.  
                else if (child != parent[vertix])
                {
                    low[vertix] = Math.Min(low[vertix], low[child]);
                }
            }
        }
    }
}