using System;

namespace Otus.DataStructures
{
    public class PriorityQueue<T> : IQueue<T>
    {
        private int _size;
        private int _rank;
        private Queue<T>[] _array;

        public int Size => _size;

        public int Rank => _rank;

        public PriorityQueue(int rank)
        {
            _size = 0;
            _rank = rank;
            _array = new Queue<T> [rank];

            for (var i = 0; i < _array.Length; i++)
            {
                _array[i] = new Queue<T>(); 
            }
        }

        public T Peek()
        {
            for (var i = 0; i < _rank; i++)
            {
                if (_array[i].Size > 0)
                {
                    return _array[i].Peek();
                }
            }

            return default;
        }

        public T Dequeue()
        {
            if (_size == 0)
            {
                return default;
            }
            for (var i = 0; i < _rank; i++)
            {
                if (_array[i].Size > 0)
                {
                    _size--;
                    return _array[i].Dequeue();
                }
            }

            return default;
        }

        public void Enqueue(T item)
        {
            _size++;
            _array[_rank - 1].Enqueue(item);
        }

        public void Enqueue(T item, int priority)
        {
            _size++;
            _array[priority - 1].Enqueue(item);
        }
    }
}