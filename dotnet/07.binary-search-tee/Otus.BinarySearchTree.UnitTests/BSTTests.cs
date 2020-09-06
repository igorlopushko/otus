using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otus.BinarySearchTree.DataStructure;

namespace Otus.BinarySearchTree.UnitTests
{
    [TestClass]
    public class BSTTests
    {
        [TestMethod]
        public void Insert_IntoEmptyTree_InsertsElement()
        {
            var tree = new BST<int, int>();

            var node = new Node<int, int>
            {
                Key = 1,
                Value = 1
            };
            tree.Insert(node);
            
            Assert.AreEqual(1, tree.Root.Value);
            Assert.IsNull(tree.Root.Left);
            Assert.IsNull(tree.Root.Right);
            Assert.AreEqual(1, tree.Count);
        }

        [TestMethod]
        public void Insert_SeveralElements_InsertsElements()
        {
            var tree = new BST<int, int>();

            var rootNode = new Node<int, int>
            {
                Key = 5,
                Value = 5
            };
            var leftNode = new Node<int, int>
            {
                Key = 1,
                Value = 1
            };
            var rightNode = new Node<int, int>
            {
                Key = 7,
                Value = 7
            };
            tree.Insert(rootNode);
            tree.Insert(leftNode);
            tree.Insert(rightNode);
            
            Assert.AreEqual(5, tree.Root.Value);
            Assert.AreEqual(1, tree.Root.Left.Value);
            Assert.AreEqual(7, tree.Root.Right.Value);
            Assert.AreEqual(3, tree.Count);
        }

        [TestMethod]
        public void Insert_InsertNodeWithExistingKey_ThrowsException()
        {
            var tree = new BST<int, int>();

            var rootNode = new Node<int, int>
            {
                Key = 5,
                Value = 5
            };
            var leftNode = new Node<int, int>
            {
                Key = 1,
                Value = 1
            };
            var rightNode = new Node<int, int>
            {
                Key = 7,
                Value = 7
            };
            tree.Insert(rootNode);
            tree.Insert(leftNode);
            tree.Insert(rightNode);

            Assert.ThrowsException<ArgumentException>(() => tree.Insert(rightNode));
        }
        
        [TestMethod]
        public void Remove_RemoveRootElementWithNoLeaves_RemovesRoot()
        {
            var tree = new BST<int, int>();

            var rootNode = new Node<int, int>
            {
                Key = 5,
                Value = 5
            };
            
            tree.Insert(rootNode);
            tree.Remove(5);
            
            Assert.IsNull(tree.Root);
            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void Remove_RemoveEmptyLeaf_RemovesLeaf()
        {
            var tree = new BST<int, int>();

            var rootNode = new Node<int, int>
            {
                Key = 5,
                Value = 5
            };
            var leftNode = new Node<int, int>
            {
                Key = 1,
                Value = 1
            };
            var rightNode = new Node<int, int>
            {
                Key = 7,
                Value = 7
            };
            
            tree.Insert(rootNode);
            tree.Insert(leftNode);
            tree.Insert(rightNode);
            
            tree.Remove(1);
            
            Assert.AreEqual(5, tree.Root.Value);
            Assert.IsNull(tree.Root.Left);
            Assert.AreEqual(7, tree.Root.Right.Value);
            Assert.AreEqual(2, tree.Count);
        }

        [TestMethod]
        public void Remove_RemoveNodeWithTwoLeaves_RemovesNode()
        {
            var tree = new BST<int, int>();

            var rootNode = new Node<int, int>
            {
                Key = 5,
                Value = 5
            };
            var leftNode = new Node<int, int>
            {
                Key = 1,
                Value = 1
            };
            var rightNode = new Node<int, int>
            {
                Key = 7,
                Value = 7
            };
            
            tree.Insert(rootNode);
            tree.Insert(leftNode);
            tree.Insert(rightNode);
            
            tree.Remove(5);
            
            Assert.AreEqual(7, tree.Root.Value);
            Assert.AreEqual(1, tree.Root.Left.Value);
            Assert.IsNull(tree.Root.Right);
            Assert.AreEqual(2, tree.Count);
        }
        
        [TestMethod]
        public void Remove_RemoveFromEmptyTree_NothingIsRemoved()
        {
            var tree = new BST<int, int>();
            
            tree.Remove(5);
            
            Assert.IsNull(tree.Root);
            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void Find_NotExistingElement_ReturnsNull()
        {
            var tree = new BST<int, int>();

            var rootNode = new Node<int, int>
            {
                Key = 5,
                Value = 5
            };
            var leftNode = new Node<int, int>
            {
                Key = 1,
                Value = 1
            };
            var rightNode = new Node<int, int>
            {
                Key = 7,
                Value = 7
            };
            
            tree.Insert(rootNode);
            tree.Insert(leftNode);
            tree.Insert(rightNode);

            var result = tree.Find(10);
            
            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void Find_InEmptyTree_ReturnsNull()
        {
            var tree = new BST<int, int>();

            var result = tree.Find(10);
            
            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void Find_ExistingElement_ReturnsCorrectElement()
        {
            var tree = new BST<int, int>();

            var rootNode = new Node<int, int>
            {
                Key = 5,
                Value = 5
            };
            var leftNode = new Node<int, int>
            {
                Key = 1,
                Value = 1
            };
            var rightNode = new Node<int, int>
            {
                Key = 7,
                Value = 7
            };
            
            tree.Insert(rootNode);
            tree.Insert(leftNode);
            tree.Insert(rightNode);

            var result = tree.Find(7);

            Assert.AreEqual(7, result.Value);
            Assert.IsNull(result.Left);
            Assert.IsNull(result.Right);
        }
    }
}