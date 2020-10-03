namespace Otus.DataStructure
{
    public class Subset
    {
        public int Parent { get; set; }
        public int Rank { get; set; }
        
        public Subset(int parent, int rank)
        {
            this.Parent = parent;
            this.Rank = rank;
        }
    }
}