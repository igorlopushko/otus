using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otus.Tree.DataStructure;

namespace Otus.Tree.UnitTests
{
    [TestClass]
    public class AVLTests
    {
        [TestMethod]
        public void Insert_IntoEmptyTree_InsertsElement()
        {
            var tree = new AVL<int, int>();

            var node = new Node<int, int>(1, 1);
            tree.Insert(node);
            
            Assert.AreEqual(1, tree.Root.Value);
            Assert.IsNull(tree.Root.Left);
            Assert.IsNull(tree.Root.Right);
            Assert.AreEqual(1, tree.Count);
        }

        [TestMethod]
        public void Insert_SeveralElements_InsertsElements()
        {
            var tree = new AVL<int, int>();

            var rootNode = new Node<int, int>(5, 5);
            var leftNode = new Node<int, int>(1, 1);
            var rightNode = new Node<int, int>(7, 7);
            
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
            var tree = new AVL<int, int>();

            var rootNode = new Node<int, int>(5, 5);
            var leftNode = new Node<int, int>(1, 1);
            var rightNode = new Node<int, int>(7, 7);
            
            tree.Insert(rootNode);
            tree.Insert(leftNode);
            tree.Insert(rightNode);

            Assert.ThrowsException<ArgumentException>(() => tree.Insert(rightNode));
        }

        [TestMethod]
        public void Insert_InsertElementsWithRotation_BalancesTheTree()
        {
            var tree = new AVL<int, int>();

            tree.Insert(new Node<int, int>(10, 10));
            tree.Insert(new Node<int, int>(20, 20));
            tree.Insert(new Node<int, int>(30, 30));
            tree.Insert(new Node<int, int>(40, 40));
            tree.Insert(new Node<int, int>(50, 50));
            tree.Insert(new Node<int, int>(25, 25));

            Assert.AreEqual(30, tree.Root.Key);
            Assert.AreEqual(20, tree.Root.Left.Key);
            Assert.AreEqual(40, tree.Root.Right.Key);
            Assert.AreEqual(10, tree.Root.Left.Left.Key);
            Assert.AreEqual(25, tree.Root.Left.Right.Key);
            Assert.AreEqual(50, tree.Root.Right.Right.Key);
            Assert.AreEqual(6, tree.Count);
        }
        
        [TestMethod]
        public void Remove_RemoveRootElementWithNoLeaves_RemovesRoot()
        {
            var tree = new AVL<int, int>();

            var rootNode = new Node<int, int>(5, 5);
            
            tree.Insert(rootNode);
            tree.Remove(5);
            
            Assert.IsNull(tree.Root);
            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void Remove_RemoveEmptyLeaf_RemovesLeaf()
        {
            var tree = new AVL<int, int>();

            var rootNode = new Node<int, int>(5, 5);
            var leftNode = new Node<int, int>(1, 1);
            var rightNode = new Node<int, int>(7, 7);
            
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
            var tree = new AVL<int, int>();

            var rootNode = new Node<int, int>(5, 5);
            var leftNode = new Node<int, int>(1, 1);
            var rightNode = new Node<int, int>(7, 7);
            
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
            var tree = new AVL<int, int>();
            
            tree.Remove(5);
            
            Assert.IsNull(tree.Root);
            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void Remove_RemoveWithRotation_BalancesTheTree()
        {
            var tree = new AVL<int, int>();

            tree.Insert(new Node<int, int>(9, 9));
            tree.Insert(new Node<int, int>(5, 5));
            tree.Insert(new Node<int, int>(10, 10));
            tree.Insert(new Node<int, int>(0, 0));
            tree.Insert(new Node<int, int>(6, 6));
            tree.Insert(new Node<int, int>(11, 11));
            tree.Insert(new Node<int, int>(-1, -1));
            tree.Insert(new Node<int, int>(1, 1));
            tree.Insert(new Node<int, int>(2, 2));
            
            tree.Remove(10);
            
            Assert.AreEqual(1, tree.Root.Key);
            Assert.AreEqual(0, tree.Root.Left.Key);
            Assert.AreEqual(9, tree.Root.Right.Key);
            Assert.AreEqual(-1, tree.Root.Left.Left.Key);
            Assert.AreEqual(5, tree.Root.Right.Left.Key);
            Assert.AreEqual(11, tree.Root.Right.Right.Key);
            Assert.AreEqual(2, tree.Root.Right.Left.Left.Key);
            Assert.AreEqual(6, tree.Root.Right.Left.Right.Key);
            Assert.AreEqual(8, tree.Count);
        }
        
        [TestMethod]
        public void Find_NotExistingElement_ReturnsNull()
        {
            var tree = new AVL<int, int>();

            var rootNode = new Node<int, int>(5, 5);
            var leftNode = new Node<int, int>(1, 1);
            var rightNode = new Node<int, int>(7, 7);
            
            tree.Insert(rootNode);
            tree.Insert(leftNode);
            tree.Insert(rightNode);

            var result = tree.Find(10);
            
            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void Find_InEmptyTree_ReturnsNull()
        {
            var tree = new AVL<int, int>();

            var result = tree.Find(10);
            
            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void Find_ExistingElement_ReturnsCorrectElement()
        {
            var tree = new AVL<int, int>();

            var rootNode = new Node<int, int>(5, 5);
            var leftNode = new Node<int, int>(1, 1);
            var rightNode = new Node<int, int>(7, 7);
            
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