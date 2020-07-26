using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otus.DataStructures;

namespace Otus.BasicDataStructures.UnitTests.Queues
{
    [TestClass]
    public class PriorityQueueTest
    {
        [TestMethod]
        public void Enqueue_EnqueueThousandElements_EnqueuesElements()
        {
            var queue =  new PriorityQueue<int>(10);

            for (var i = 0; i < 1000; i++)
            {
                queue.Enqueue(i);
            }
            
            Assert.AreEqual(1000, queue.Size);
        }

        [TestMethod]
        public void Enqueue_EnqueueWithPriority_EnqueuesElements()
        {
            var queue =  new PriorityQueue<int>(10);

            for (var i = 0; i < 1000; i++)
            {
                var random = new Random();
                queue.Enqueue(i, random.Next(1, 10));
            }
            
            Assert.AreEqual(1000, queue.Size);
        }
        
        [TestMethod]
        public void Dequeue_DequeueTenElements_DequeuesElements()
        {
            var queue =  new PriorityQueue<int>(10);

            for (var i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
            }
            
            Assert.AreEqual(10, queue.Size);
            Assert.AreEqual(0, queue.Dequeue());
            Assert.AreEqual(9, queue.Size);
            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(8, queue.Size);
            Assert.AreEqual(2, queue.Dequeue());
            Assert.AreEqual(7, queue.Size);
            Assert.AreEqual(3, queue.Dequeue());
            Assert.AreEqual(6, queue.Size);
            Assert.AreEqual(4, queue.Dequeue());
            Assert.AreEqual(5, queue.Size);
            Assert.AreEqual(5, queue.Dequeue());
            Assert.AreEqual(4, queue.Size);
            Assert.AreEqual(6, queue.Dequeue());
            Assert.AreEqual(3, queue.Size);
            Assert.AreEqual(7, queue.Dequeue());
            Assert.AreEqual(2, queue.Size);
            Assert.AreEqual(8, queue.Dequeue());
            Assert.AreEqual(1, queue.Size);
            Assert.AreEqual(9, queue.Dequeue());
            Assert.AreEqual(0, queue.Size);
        }

        [TestMethod]
        public void Dequeue_DequeueEmptyQueue_ReturnsDefaultElementValue()
        {
            var queue = new PriorityQueue<int>(10);
            
            Assert.AreEqual(default, queue.Dequeue());
        }
        
        [TestMethod]
        public void Dequeue_DequeueWithPriority_ReturnsElementsWithPriority()
        {
            var queue = new PriorityQueue<int>(10);
            queue.Enqueue(1, 1);
            queue.Enqueue(2, 2);
            queue.Enqueue(3, 2);
            queue.Enqueue(4, 10);
            queue.Enqueue(5, 10);
            queue.Enqueue(6, 3);
            queue.Enqueue(7, 4);
            
            Assert.AreEqual(7, queue.Size);
            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(6, queue.Size);
            Assert.AreEqual(2, queue.Dequeue());
            Assert.AreEqual(5, queue.Size);
            Assert.AreEqual(3, queue.Dequeue());
            Assert.AreEqual(4, queue.Size);
            Assert.AreEqual(6, queue.Dequeue());
            Assert.AreEqual(3, queue.Size);
            Assert.AreEqual(7, queue.Dequeue());
            Assert.AreEqual(2, queue.Size);
            Assert.AreEqual(4, queue.Dequeue());
            Assert.AreEqual(1, queue.Size);
            Assert.AreEqual(5, queue.Dequeue());
            Assert.AreEqual(0, queue.Size);
        }
        
        [TestMethod]
        public void Peek_PeekElement_PeeksTheElement()
        {
            var queue =  new PriorityQueue<int>(10);

            for (var i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
            }
            
            Assert.AreEqual(0, queue.Peek());
            Assert.AreEqual(10, queue.Size);
        }
    }
}