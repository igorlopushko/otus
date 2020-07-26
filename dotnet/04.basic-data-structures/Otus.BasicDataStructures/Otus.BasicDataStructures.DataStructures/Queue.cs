namespace Otus.DataStructures
{
    public class Queue<T> : IQueue<T>
    {
        private int _size;
        private Node<T> _head;
        private Node<T> _tail;
        
        public int Size => _size;
        
        public T Peek()
        {
            if (_head == null)
                return default;
            
            return _head.Value;
        }

        public T Dequeue()
        {
            if (_size == 0)
            {
                return default;
            }
            
            var item = _head;
            _head = _head.Next;
            _size--;
            return item.Value;
        }

        public void Enqueue(T item)
        {
            if (_head == null)
            {
                _head = new Node<T>
                {
                    Value = item
                };
                _tail = _head;
                _head.Next = _tail;
            }
            else
            {
                var node = new Node<T>();
                node.Value = item;
                _tail.Next = node;
                _tail = node;
            }

            _size++;
        }
    }
}