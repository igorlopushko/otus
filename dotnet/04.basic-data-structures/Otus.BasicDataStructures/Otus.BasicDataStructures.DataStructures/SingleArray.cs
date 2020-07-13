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
            _array[_array.Length - 1] = item;
        }

        public override void Add(T item, int index)
        {
            T[] newArray = new T[_array.Length + 1];

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
            T[] newArray = new T[_array.Length + 1];
            for (int i = 0; i < _array.Length - 1; i++)
            {
                newArray[i] = _array[i];
            }

            _array = newArray;
            _size = _array.Length;
        }
    }
}