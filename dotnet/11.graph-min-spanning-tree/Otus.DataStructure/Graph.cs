using System.Collections.Generic;
using System.Linq;

namespace Otus.DataStructure
{
    public class Graph
    {
        private List<Edge> _edges;
        private HashSet<int> _vertices;
        private int[,] _adjacencyMatrix;

        public Edge[] Edges => _edges.ToArray();
        public int[] Vertices => _vertices.ToArray();

        public Graph()
        {
            _edges = new List<Edge>();
            _vertices = new HashSet<int>();
        }

        public Graph(int[,] adjacencyMatrix)
        {
            _adjacencyMatrix = adjacencyMatrix;
            
            _edges = new List<Edge>();
            _vertices = new HashSet<int>();
            
            // convert to edges
            for (var i = 0; i < adjacencyMatrix.GetLongLength(0); i++)
            {
                _vertices.Add(i);
                
                for (var j = i; j < adjacencyMatrix.GetLongLength(1); j++)
                {
                    if (adjacencyMatrix[i, j] != 0)
                    {
                        _edges.Add(new Edge(i, j, adjacencyMatrix[i, j]));                        
                    }
                }
            }
        }
        
        public Graph(Edge[] edges)
        {
            _edges = new List<Edge>();
            _vertices = new HashSet<int>();
            
            foreach (var edge in edges)
            {
                if (!_vertices.Contains(edge.Source))
                {
                    _vertices.Add(edge.Source);
                }
                
                if (!_vertices.Contains(edge.Destination))
                {
                    _vertices.Add(edge.Destination);
                }
                
                _edges.Add(edge);    
            }
        }

        public void AddEdge(int source, int destination, int rank)
        {
            if (!_vertices.Contains(source))
            {
                _vertices.Add(source);
            }
                
            if (!_vertices.Contains(destination))
            {
                _vertices.Add(destination);
            }
            
            _edges.Add(new Edge(source, destination, rank));
        }
        
        public void AddEdge(Edge edge)
        {
            if (!_vertices.Contains(edge.Source))
            {
                _vertices.Add(edge.Source);
            }
                
            if (!_vertices.Contains(edge.Destination))
            {
                _vertices.Add(edge.Destination);
            }
            
            _edges.Add(edge);
        }

        public IEnumerable<Edge> GetMinSpanningTreeKruskal()
        {
            var result = new List<Edge>();
            var sortedEdges = MergeSort.Sort(_edges.ToArray());
            
            var subsets = new Subset[_vertices.Count];
            for (var i = 0; i < _vertices.Count; i++)
            {
                subsets[i] = new Subset(i, 0);
            }

            var edgeCount = 0;
            var sortedEdgeCount = 0;
            
            while (edgeCount < _vertices.Count - 1)
            {
                var minEdge = sortedEdges[sortedEdgeCount];

                var subsetRoot1 = Find(subsets, minEdge.Source);
                var subsetRoot2 = Find(subsets, minEdge.Destination);

                if (subsetRoot1 != subsetRoot2)
                {
                    result.Add(minEdge);
                    Union(subsets, subsetRoot1, subsetRoot2);
                    edgeCount++;
                }
                
                sortedEdgeCount++;
            }
            
            return result;
        }

        public IEnumerable<Edge> GetMinSpanningTreeBoruvka()
        {
            var result = new List<Edge>();
            var sortedEdges = MergeSort.Sort(_edges.ToArray());
            
            // an array to store index of the cheapest edge of subset
            var cheapestEdge = new int[_vertices.Count];
            
            var subsets = new Subset[_vertices.Count];
            for (var i = 0; i < _vertices.Count; i++)
            {
                subsets[i] = new Subset(i, 0);
                cheapestEdge[i] = -1;
            }

            // initially there is a different tree for each vertix 
            var treeCount = _vertices.Count;

            while (treeCount > 1) 
            { 
                // everytime initialize cheapest array 
                for (var vertix = 0; vertix < _vertices.Count; vertix++) 
                { 
                    cheapestEdge[vertix] = -1; 
                } 

                // traverse through all edges and update cheapest of every set 
                for (var i = 0; i < sortedEdges.Length; i++) 
                { 
                    // find roots of two corners of current edge 
                    var root1 = Find(subsets, sortedEdges[i].Source); 
                    var root2 = Find(subsets, sortedEdges[i].Destination);

                    // if two corners of current edge belong to same set, ignore current edge 
                    if (root1 == root2) continue;

                    // check if current edge is closer to previous cheapest edges of set1 (root1) and set2 (root2) 
                    if (cheapestEdge[root1] == -1 || sortedEdges[cheapestEdge[root1]].Rank > sortedEdges[i].Rank)
                    {
                        cheapestEdge[root1] = i;
                    }

                    if (cheapestEdge[root2] == -1 || sortedEdges[cheapestEdge[root2]].Rank > sortedEdges[i].Rank)
                    {
                        cheapestEdge[root2] = i;
                    }
                }

                // consider the above picked cheapest edges and add them to result 
                for (var i = 0; i < _vertices.Count; i++) 
                { 
                    // check if cheapest for current set exists 
                    if (cheapestEdge[i] != -1) 
                    { 
                        var root1 = Find(subsets, sortedEdges[cheapestEdge[i]].Source); 
                        var root2 = Find(subsets, sortedEdges[cheapestEdge[i]].Destination);

                        if (root1 == root2) continue;
                        
                        result.Add(new Edge(
                            sortedEdges[cheapestEdge[i]].Source,
                            sortedEdges[cheapestEdge[i]].Destination,
                            sortedEdges[cheapestEdge[i]].Rank));

                        // union of set1 and set2 and decrease number of trees 
                        Union(subsets, root1, root2); 
                        treeCount--; 
                    } 
                } 
            }

            return result;
        }
        
        public IEnumerable<Edge> GetMinSpanningTreePrim()
        {
            // Array to store constructed MST
            var parent = new int[_vertices.Count];

            // Key values used to pick minimum weight edge in cut
            var key = new int[_vertices.Count];

            // represents set of vertices included in MST
            var vertexSet = new bool[_vertices.Count];

            // Initialize all keys as INFINITE
            for (var i = 0; i < _vertices.Count; i++) {
                key[i] = int.MaxValue;
                vertexSet[i] = false;
            }
            
            key[0] = 0;
            parent[0] = -1;

            for (var count = 0; count < _vertices.Count - 1; count++) 
            {
                // pick thd minimum key vertex from the set of vertices not yet included in MST
                var minValue = GetMinKey(key, vertexSet);

                // add the picked vertex to the MST set
                vertexSet[minValue] = true;

                // update key value and parent index of the adjacent vertices of the picked vertex.
                // consider only those vertices which are not yet included in MST
                for (var vertex = 0; vertex < _vertices.Count; vertex++)
                {
                    if (_adjacencyMatrix[minValue, vertex] != 0 
                        && _adjacencyMatrix[minValue, vertex] < key[vertex]
                        && !vertexSet[vertex]) 
                    {
                        parent[vertex] = minValue;
                        key[vertex] = _adjacencyMatrix[minValue, vertex];
                    }
                }
            }

            var result = new List<Edge>();
            for (var i = 1; i < _vertices.Count; i++)
            {
                result.Add(new Edge(parent[i], i, _adjacencyMatrix[i, parent[i]]));
            }

            return result;
        }
        
        private int GetMinKey(int[] key, bool[] vertexSet)
        {
            var min = int.MaxValue;
            var index = -1;

            for (var vertex = 0; vertex < _vertices.Count; vertex++)
            {
                if (!vertexSet[vertex] && key[vertex] < min)
                {
                    min = key[vertex];
                    index = vertex;
                }
            }

            return index;
        }
        
        private int Find(Subset[] subsets, int vertix)
        {
            if (subsets[vertix].Parent != vertix)
            {
                subsets[vertix].Parent = Find(subsets, subsets[vertix].Parent);
            }

            return subsets[vertix].Parent; 
        }

        private void Union(Subset[] subsets, int vertix1, int vertix2)
        {
            var root1 = Find(subsets, vertix1); 
            var root2 = Find(subsets, vertix2);
            
            // attach smaller rank tree under root of high rank tree  
            if (subsets[root1].Rank < subsets[root2].Rank)
            {
                subsets[root1].Parent = root2;
            } 
            else if (subsets[root1].Rank > subsets[root2].Rank)
            {
                subsets[root2].Parent = root1;
            }
            // if ranks are same, then make one as root and increment its rank  
            else
            { 
                subsets[root2].Parent = root1; 
                subsets[root1].Rank++; 
            }
        }
    }
}