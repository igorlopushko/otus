namespace Otus.DataStructures
{
    public interface IQueue<T>
    {
        T Peek();
        T Dequeue();
        void Enqueue(T item);
        int Size { get; }
    }
}