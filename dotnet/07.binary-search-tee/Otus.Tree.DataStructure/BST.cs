using System;

namespace Otus.Tree.DataStructure
{
    public class BST<K, T> : ITree<K, T> where K : IComparable<K>
    {
        private Node<K, T> _root;
        private int _count;

        public Node<K, T> Root => _root;
        public int Count => _count;

        public BST()
        {
            _root = null;
            _count = 0;
        }
        
        public BST(Node<K, T> root)
        {
            _root = root;
            _count = 1;
        }
        
        public void Insert(Node<K, T> node)
        {
            if (node == null || node.Value == null)
            {
                return;
            }
            
            if (_root == null || _root.Value == null)
            {
                _root = node;
                _count++;
            }
            else
            {
                _root = InsertNode(_root, node);
            }
        }

        public void Remove(K key)
        {
            _root = RemoveNode(_root, key);
        }

        public Node<K, T> Find(K key)
        {
            if (_root == null)
            {
                return null;
            }

            return FindNode(_root, key);
        }

        private Node<K, T> InsertNode(Node<K, T> root, Node<K, T> node)
        {
            // if tree is empty return current node to be inserted.
            if (root == null)
            {
                _count++;
                return node;
            }
            
            // recursion down the tree structure.
            if (root.Key.CompareTo(node.Key) > 0)
            {
                root.Left = InsertNode(root.Left, node);
            }
            else if (root.Key.CompareTo(node.Key) < 0)
            {
                root.Right = InsertNode(root.Right, node);
            }
            else if (root.Key.CompareTo(node.Key) == 0)
            {
                throw new ArgumentException($"Node with the same Key: {node.Key} already exist");
            }

            // return unchanged subtree root
            return root;
        }
        
        private Node<K, T> RemoveNode(Node<K, T> root, K key)
        {
            // return root if tree is empty
            if (root == null)
            {
                return null;
            }

            // recursive down to the tree to find element to delete 
            if (root.Key.CompareTo(key) > 0)
            {
                root.Left = RemoveNode(root.Left, key);
            }
            else if (root.Key.CompareTo(key) < 0)
            {
                root.Right = RemoveNode(root.Right, key);
            }
            // if the node is equal to the root, then this is the node to be deleted 
            else
            {
                // case 1: there is no child 
                if (root.Left == null && root.Right == null)
                {
                    root = null;
                }
                // case 2: there is one child
                else if (root.Left == null)
                {
                    root = root.Right;
                }
                else if (root.Right == null)
                {
                    root = root.Left;
                }
                // case 3: there are two children
                else
                {
                    // get inorder successor (smallest in the right subtree)
                    var minRight = TreeHelper<K, T>.GetMinNode(root.Right);
                    if (root.Key.CompareTo(_root.Key) == 0)
                    {
                        minRight.Left = _root.Left;
                    }
                    root = minRight;
                
                    // delete the inorder successor
                    root.Right = RemoveNode(root.Right, minRight.Key);                    
                }

                _count--;
            }

            return root;
        }

        private static Node<K, T> FindNode(Node<K, T> parent, K key)
        {
            if (parent == null)
            {
                return null;
            }
            
            if (key.CompareTo(parent.Key) < 0)
            {
                return FindNode(parent.Left, key);
            }
            if (key.CompareTo(parent.Key) > 0)
            {
                return FindNode(parent.Right, key);
            }
            
            return  parent;
        }
    }
}