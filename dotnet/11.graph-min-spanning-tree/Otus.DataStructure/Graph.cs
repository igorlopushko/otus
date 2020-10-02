using System.Collections.Generic;

namespace Otus.DataStructure
{
    public class Graph
    {
        private List<Edge> _edges;
        private HashSet<int> _vertices;

        public Edge[] Edges => _edges.ToArray();

        public Graph()
        {
            _edges = new List<Edge>();
            _vertices = new HashSet<int>();
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
            _edges.Add(edge);
        }

        public Edge[] GetMinSpanningTreeKruskal()
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
            
            return result.ToArray();
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