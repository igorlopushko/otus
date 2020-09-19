using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Otus.HashTable.UnitTests
{
    [TestClass]
    public class HashTableWithOpenAddressingTests
    {
        [TestMethod]
        public void ContainsKey_EmptyTable_ReturnsFalse()
        {
            var hashTable = new Otus.HashTable.DataStructure.HashTableWithOpenAddressing<int, string>();

            Assert.IsFalse(hashTable.ContainsKey(1));
        }
        
        [TestMethod]
        public void ContainsKey_DoesNotContainKey_ReturnsFalse()
        {
            var hashTable = new Otus.HashTable.DataStructure.HashTableWithOpenAddressing<int, string>();
            
            hashTable.Add(2, "two");
            
            Assert.IsFalse(hashTable.ContainsKey(1));
        }

        [TestMethod]
        public void ContainsKey_ContainsKey_ReturnsTrue()
        {
            var hashTable = new Otus.HashTable.DataStructure.HashTableWithOpenAddressing<int, string>();
            
            hashTable.Add(1, "one");
            
            Assert.IsTrue(hashTable.ContainsKey(1));
        }

        [TestMethod]
        public void ContainsValue_EmptyTable_ReturnsFalse()
        {
            var hashTable = new Otus.HashTable.DataStructure.HashTableWithOpenAddressing<int, string>();

            Assert.IsFalse(hashTable.ContainsValue("one"));
        }
        
        [TestMethod]
        public void ContainsValue_DoesNotContainValue_ReturnsFalse()
        {
            var hashTable = new Otus.HashTable.DataStructure.HashTableWithOpenAddressing<int, string>();
            
            hashTable.Add(2, "two");
            
            Assert.IsFalse(hashTable.ContainsValue("one"));
        }

        [TestMethod]
        public void ContainsValue_ContainsValue_ReturnsTrue()
        {
            var hashTable = new Otus.HashTable.DataStructure.HashTableWithOpenAddressing<int, string>();
            
            hashTable.Add(1, "one");
            
            Assert.IsTrue(hashTable.ContainsValue("one"));
        }

        [TestMethod]
        public void Find_DoesNotContainsNode_ReturnsDefault()
        {
            var hashTable = new Otus.HashTable.DataStructure.HashTableWithOpenAddressing<int, string>();
            
            hashTable.Add(1, "one");
            hashTable.Add(2, "two");
            
            Assert.AreEqual(default, hashTable.Find(3));
        }

        [TestMethod]
        public void Find_ContainsNode_ReturnsNodeValue()
        {
            var hashTable = new Otus.HashTable.DataStructure.HashTableWithOpenAddressing<int, string>();
            
            hashTable.Add(1, "one");
            hashTable.Add(2, "two");
            
            Assert.AreEqual("two", hashTable.Find(2));
        }

        [TestMethod]
        public void Add_WithoutRehash_AddsNodes()
        {
            var hashTable = new Otus.HashTable.DataStructure.HashTableWithOpenAddressing<int, string>();
            
            hashTable.Add(1, "one");
            hashTable.Add(2, "two");
            
            Assert.AreEqual("one", hashTable.Find(1));
            Assert.AreEqual("two", hashTable.Find(2));
            Assert.AreEqual(2, hashTable.Count);
        }

        [TestMethod]
        public void Add_WithRehash_AddsNodes()
        {
            var hashTable = new Otus.HashTable.DataStructure.HashTableWithOpenAddressing<int, string>(10);
            
            hashTable.Add(1, "one");
            hashTable.Add(2, "two");
            hashTable.Add(3, "three");
            hashTable.Add(4, "four");
            hashTable.Add(5, "five");
            hashTable.Add(6, "six");
            hashTable.Add(7, "seven");
            hashTable.Add(8, "eight");
            hashTable.Add(9, "nine");
            hashTable.Add(10, "ten");
            
            Assert.AreEqual("nine", hashTable.Find(9));
            Assert.AreEqual("ten", hashTable.Find(10));
            Assert.AreEqual(10, hashTable.Count);
        }

        [TestMethod]
        public void Add_NodeWithKeyAlreadyExist_ThrowsException()
        {
            var hashTable = new Otus.HashTable.DataStructure.HashTableWithOpenAddressing<int, string>();
            
            hashTable.Add(1, "one");

            Assert.ThrowsException<ArgumentException>(() => hashTable.Add(1, "another one"));
        }

        [TestMethod]
        public void Remove_FromEmptyTable_NothingHappens()
        {
            var hashTable = new Otus.HashTable.DataStructure.HashTableWithOpenAddressing<int, string>();
            
            hashTable.Remove(1);
            
            Assert.AreEqual(0, hashTable.Count);
        }

        [TestMethod]
        public void Remove_NotExistingNode_NothingHappens()
        {
            var hashTable = new Otus.HashTable.DataStructure.HashTableWithOpenAddressing<int, string>();
            hashTable.Add(1, "one");
            
            hashTable.Remove(2);
            
            Assert.AreEqual(1, hashTable.Count);
        }

        [TestMethod]
        public void Remove_ExistingNode_RemovesNode()
        {
            var hashTable = new Otus.HashTable.DataStructure.HashTableWithOpenAddressing<int, string>();
            hashTable.Add(1, "one");
            
            hashTable.Remove(1);
            
            Assert.AreEqual(0, hashTable.Count);
        }
    }
}