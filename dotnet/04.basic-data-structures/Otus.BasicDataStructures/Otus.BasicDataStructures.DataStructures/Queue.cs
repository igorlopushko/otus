namespace Otus.DataStructures
{
    public class Queue<T> : IQueue<T>
    {
        private Node<T> _head;
        private Node<T> _tail;
        
        public T Peek()
        {
            return _head.Value;
        }

        public T Dequeue()
        {
            var item = _head;
            _head = _head.Next;
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
        }
    }
}