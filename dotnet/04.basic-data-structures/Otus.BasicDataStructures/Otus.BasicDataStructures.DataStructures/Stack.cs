namespace Otus.DataStructures
{
    public class Stack<T>
    {
        private Node<T> _head;
        
        public T Peek()
        {
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
        }

        public T Pop()
        {
            if (_head == null)
            {
                return default;
            }
            
            var item = _head.Value;
            _head = _head.Next;
            return item;

        }
    }
}