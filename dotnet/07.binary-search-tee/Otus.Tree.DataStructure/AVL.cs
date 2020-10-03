using System;

namespace Otus.Tree.DataStructure
{
    public class AVL<K, T> : ITree<K, T> where K : IComparable<K>
    {
        private int _count;
        private Node<K, T> _root;

        public int Count => _count;

        public Node<K, T> Root => _root;

        public AVL()
        {
            _root = null;
            _count = 0;
        }
        
        public AVL(Node<K, T> root)
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
            
            // step 1. insert the normals BST
            // recursion down the tree structure
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

            // step 2. update height of this ansector node
            root.Height = 1 + TreeHelper<K, T>.Max(
                TreeHelper<K, T>.GetHeight(root.Left), 
                TreeHelper<K, T>.GetHeight(root.Right)); 
            
            // step 3. get balance factor of this ansector node to check whether this node became unbalanced
            int balance = TreeHelper<K, T>.GetBalance(root);
            
            // if the node is unbalanced perform rotation
            // left rotation case
            if (balance > 1 && node.Key.CompareTo(root.Left.Key) < 0)
            {
                return RightRotate(root);
            }
            
            // right rotation case
            if (balance < -1 && node.Key.CompareTo(root.Right.Key) > 0)
            {
                return LeftRotate(root);
            }
            
            // left right rotation case
            if (balance > 1 && node.Key.CompareTo(root.Left.Key) > 0)
            {
                root.Left = LeftRotate(root.Left);
                return RightRotate(root);
            }
            
            // right left rotation case 
            if (balance < -1 && node.Key.CompareTo(root.Right.Key) < 0)
            {
                root.Right = RightRotate(root.Right);
                return LeftRotate(root);
            }
            
            // return unchanged subtree root
            return root;
        }
        
        private Node<K, T> RemoveNode(Node<K, T> root, K key)
        {
            // step 1. perform standard BST remove
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

            // root element was deleted
            if (root == null)
            {
                return null;
            }
            
            // step 2. update height of the current node
            root.Height = TreeHelper<K, T>.Max(TreeHelper<K, T>.GetHeight(root.Left), TreeHelper<K, T>.GetHeight(root.Right)) + 1;
            
            // step 3. get balance factor of this ansector node to check whether this node became unbalanced
            int balance = TreeHelper<K, T>.GetBalance(root);
            
            // if current node is unbalanced perform rotation
            // left rotation case
            if (balance > 1 && TreeHelper<K, T>.GetBalance(root.Left) >= 0)
            {
                return RightRotate(root);
            }
            
            // right rotation case
            if (balance < -1 && TreeHelper<K, T>.GetBalance(root.Right) <= 0)
            {
                return LeftRotate(root);
            }
            
            // left right rotation case
            if (balance > 1 && TreeHelper<K, T>.GetBalance(root.Left) < 0)
            {
                root.Left = LeftRotate(root.Left);
                return RightRotate(root);
            }
            
            // right left rotation case
            if (balance < -1 && TreeHelper<K, T>.GetBalance(root.Right) > 0)
            {
                root.Right = RightRotate(root.Right);
                return LeftRotate(root);
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
        
        private static Node<K, T> LeftRotate(Node<K, T> node)
        {
            var right = node.Right;  
            var temp = right.Left;
  
            // perform rotation
            right.Left = node;
            node.Right = temp;
  
            // update heights  
            node.Height = TreeHelper<K, T>.Max(TreeHelper<K, T>.GetHeight(node.Left), TreeHelper<K, T>.GetHeight(node.Right)) + 1;
            right.Height = TreeHelper<K, T>.Max(TreeHelper<K, T>.GetHeight(right.Left), TreeHelper<K, T>.GetHeight(right.Right)) + 1;
  
            // return new root  
            return right;
        }
        
        private static Node<K, T> RightRotate(Node<K, T> node)
        {
            var left = node.Left;
            var temp = left.Right;  
  
            // Perform rotation  
            left.Right = node;
            node.Left = temp;  
  
            // Update heights  
            node.Height = TreeHelper<K, T>.Max(TreeHelper<K, T>.GetHeight(node.Left), TreeHelper<K, T>.GetHeight(node.Right)) + 1;
            left.Height = TreeHelper<K, T>.Max(TreeHelper<K, T>.GetHeight(left.Left), TreeHelper<K, T>.GetHeight(left.Right)) + 1;
  
            // Return new root  
            return left;
        }
    }
}