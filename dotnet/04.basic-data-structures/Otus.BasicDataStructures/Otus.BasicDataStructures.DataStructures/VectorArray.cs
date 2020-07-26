namespace Otus.DataStructures
{
    public class VectorArray<T> : BaseArray<T>, IArray<T>
    {
        private int _vector;

        public VectorArray() : this(100)
        {
        }

        public VectorArray(int vector)
        {
            _vector = vector;
            _size = 0;
            _array = new T[0];
        }

        public override void Add(T item)
        {
            if (_array.Length == _size)
            {
                Resize();
            } 
            _array[_size] = item;
            _size++;
        }

        public override void Add(T item, int index)
        {
            T[] newArray;


            if (_array.Length == _size)
            {
                // resize if length == size
                newArray = new T[_array.Length + _vector];
            }
            else
            {
                // resize size + 1
                newArray = new T[_array.Length + 1];
            }

            for (var i = 0; i < index; i++)
            {
                newArray[i] = _array[i];
            }
            newArray[index] = item;
            for (var i = index; i < _array.Length; i++)
            {
                newArray[i + 1] = _array[i];
            }

            _array = newArray;
            _size++;
        }

        private void Resize()
        {
            var newArray = new T[_array.Length + _vector];
            for (var i = 0; i < _array.Length; i++)
            {
                newArray[i] = _array[i];
            }
            _array = newArray;
        }
    }
}
