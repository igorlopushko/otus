using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otus.DataStructures;

namespace Otus.BasicDataStructures.UnitTests
{
    [TestClass]
    public class StackTest
    {
        [TestMethod]
        public void Push_PushThousandElements_PushesElements()
        {
            var stack = new Stack<int>();

            for (var i = 0; i < 1000; i++)
            {
                stack.Push(i);
            }
            
            Assert.AreEqual(1000, stack.Size);
        }

        [TestMethod]
        public void Pop_PopWithNotEmptyStack_PopsElement()
        {
            var stack = new Stack<int>();

            for (var i = 0; i < 1000; i++)
            {
                stack.Push(i);
            }

            var item = stack.Pop();
            
            Assert.AreEqual(999, stack.Size);
            Assert.AreEqual(999, item);
        }

        [TestMethod]
        public void Pop_PopWithEmptyStack_ReturnsDefault()
        {
            var stack = new Stack<int>();
            
            var item = stack.Pop();
            
            Assert.AreEqual(0, stack.Size);
            Assert.AreEqual(default, item);
        }

        [TestMethod]
        public void Peek_PeekWithNotEmptyStack_PeeksElement()
        {
            var stack = new Stack<int>();

            for (var i = 0; i < 1000; i++)
            {
                stack.Push(i);
            }

            var item = stack.Peek();
            
            Assert.AreEqual(1000, stack.Size);
            Assert.AreEqual(999, item);
        }
        
        [TestMethod]
        public void Peek_PeekWithEmptyStack_ReturnsDefault()
        {
            var stack = new Stack<int>();

            var item = stack.Peek();
            
            Assert.AreEqual(0, stack.Size);
            Assert.AreEqual(default, item);
        }
    }
}