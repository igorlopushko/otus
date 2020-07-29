using System;

namespace Otus.DataStructures
{
    public class SingleArray<T> : BaseArray<T>
    {        
        public SingleArray()
        {
            _array = new T[0];
            _size = 0;
        }

        public override void Add(T item)
        {
            Resize();
            _array[_size] = item;
            _size++;
        }

        public override void Add(T item, int index)
        {
            // index is out of the range
            if (index > _size - 1)
            {
                throw new IndexOutOfRangeException();
            }
            
            var newArray = new T[_array.Length + 1];

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
            _size++;
        }

        private void Resize()
        {
            T[] newArray = new T[_array.Length + 1];
            for (var i = 0; i < _array.Length; i++)
            {
                newArray[i] = _array[i];
            }

            _array = newArray;
        }
    }
}