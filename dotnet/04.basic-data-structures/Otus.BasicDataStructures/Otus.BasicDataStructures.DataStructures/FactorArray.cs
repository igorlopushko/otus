namespace Otus.DataStructures
{
    public class FactorArray<T> : BaseArray<T>
    {        
        public FactorArray()
        {
            _size = 0;
            _array = new T[10];
        }

        public override void Add(T item)
        {
            if (_array.Length == _size)
            {
                Resize();
            }
            _array[_size - 1] = item;
            _size++;
        }

        public override void Add(T item, int index)
        {
            T[] newArray;

            
            if (_array.Length == _size)
            {
                // resize if length == size
                newArray = new T[_array.Length * 2];
            }            
            else
            {
                // resize size + 1
                newArray = new T[_array.Length + 1];
            }

            for (int i = 0; i < index; i++)
            {
                newArray[i] = _array[i];
            }
            newArray[index] = item;
            for (int i = index; i < _array.Length - 1; i++)
            {
                newArray[i + 1] = _array[i];
            }

            _array = newArray;
            _size = _array.Length;
        }

        private void Resize()
        {
            T[] newArray = new T[_array.Length * 2];
            for (int i = 0; i < _array.Length - 1; i++)
            {
                newArray[i] = _array[i];
            }

            _array = newArray;
            _size = _array.Length;
        }
    }
}
