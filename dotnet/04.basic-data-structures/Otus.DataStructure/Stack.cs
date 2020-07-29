namespace Otus.DataStructures
{
    public class Stack<T>
    {
        private int _size;
        private Node<T> _head;

        public int Size => _size;
        
        public T Peek()
        {
            if (_head == null)
            {
                return default;
            }
            
            return _head.Value;
        }

        public void Push(T item)
        {
            var node = new Node<T>
            {
                Value = item
            };

            if (_head == null)
            {
                _head = node;
            }
            else
            {
                node.Next = _head;
                _head = node;
            }

            _size++;
        }

        public T Pop()
        {
            if (_head == null)
            {
                return default;
            }
            
            var item = _head.Value;
            _head = _head.Next;

            _size--;
            
            return item;
        }
    }
}