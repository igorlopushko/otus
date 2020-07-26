using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otus.DataStructures;

namespace Otus.BasicDataStructures.UnitTests.Arrays
{
    [TestClass]
    public class SingleArrayTest
    {
        [TestMethod]
        public void Get_GetElementAtSpecificIndex_GetsTheElement()
        {
            var array = new SingleArray<int>();

            for (var i = 0; i < 1000; i++)
            {
                array.Add(i);
            }
            
            Assert.AreEqual(10, array.Get(10));
        }
        
        [TestMethod]
        public void Add_AddThousandIntElements_AddsElements()
        {
            var array = new SingleArray<int>();

            for (var i = 0; i < 1000; i++)
            {
                array.Add(i);
            }
            
            Assert.AreEqual(1000, array.Size);
        }

        [TestMethod]
        public void Add_AddElementAtSpecificIndex_AddsElement()
        {
            var array = new SingleArray<int>();

            for (var i = 0; i < 1000; i++)
            {
                array.Add(i);
            }
            
            array.Add(9999, 10);
            
            Assert.AreEqual(1001, array.Size);
            Assert.AreEqual(9999, array.Get(10));
        }
        
        [TestMethod]
        public void Add_AddElementAtNotExistingIndex_ThrowsException()
        {
            var array = new SingleArray<int>();

            for (var i = 0; i < 1000; i++)
            {
                array.Add(i);
            }

            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                array.Add(9999, 1000);
            });
        }

        [TestMethod]
        public void Remove_RemoveElementAtIndex_RemovesTheElement()
        {
            var array = new SingleArray<int>();

            for (var i = 0; i < 1000; i++)
            {
                array.Add(i);
            }

            var item = array.Remove(75);
                
            Assert.AreEqual(999, array.Size);
            Assert.AreEqual(75, item);
        }
        
        [TestMethod]
        public void Remove_RemoveElementAtNotExistingIndex_ThrowsException()
        {
            var array = new SingleArray<int>();

            for (var i = 0; i < 1000; i++)
            {
                array.Add(i);
            }

            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                array.Remove(1000);
            });
        }
    }
}