namespace Otus.DataStructures
{
    public interface IArray<T>
    {
        int Size { get; }
        void Add(T item);
        T Get(int index);
        void Add(T item, int index);
        T Remove(int index);
    }
}
