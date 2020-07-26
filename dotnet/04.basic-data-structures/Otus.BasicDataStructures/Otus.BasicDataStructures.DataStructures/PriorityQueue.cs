namespace Otus.DataStructures
{
    public class PriorityQueue<T> : IQueue<T>
    {
        private int _rank;
        private Queue<T>[] _array;

        public int Rank
        {
            get { return _rank; }
        }

        public PriorityQueue(int rank)
        {
            _rank = rank;
            _array = new Queue<T> [rank];
        }

        public T Peek()
        {
            for (var i = 0; i < _rank; i++)
            {
                if (_array[i].Peek() != null)
                {
                    return _array[i].Peek();
                }
            }

            return default;
        }

        public T Dequeue()
        {
            for (var i = 0; i < _rank; i++)
            {
                if (_array[i].Peek() != null)
                {
                    return _array[i].Dequeue();
                }
            }

            return default;
        }

        public void Enqueue(T item)
        {
            _array[_rank - 1].Enqueue(item);
        }

        public void Enqueue(T item, int priority)
        {
            _array[priority - 1].Enqueue(item);
        }
    }
}