using System;

namespace Otus.Tree.DataStructure
{
    public class TreeHelper<K, T>  where K : IComparable<K>
    {
        /// <summary>
        /// Gets balance of the given node.
        /// </summary>
        /// <param name="node">Node to calculate balance for.</param>
        /// <returns>Balance int value.</returns>
        public static int GetBalance(Node<K, T> node)
        {
            return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
        }
        
        /// <summary>
        /// Gets height of the given node.
        /// </summary>
        /// <param name="node">Node to get height of.</param>
        /// <returns>Height int value.</returns>
        public static int GetHeight(Node<K, T> node)
        {
            return node?.Height ?? 0;
        }

        /// <summary>
        /// Gets minimum value between two values.
        /// </summary>
        /// <param name="a">First argument.</param>
        /// <param name="b">Second argument/</param>
        /// <returns></returns>
        public static int Max(int a, int b)
        {
            return a > b ? a : b;
        }
        
        /// <summary>
        /// Finds min inorder node in the subtree.
        /// </summary>
        /// <param name="root">Root node to start search with.</param>
        /// <returns>Minimum (most left node) in the subtree.</returns>
        public static Node<K, T> GetMinNode(Node<K, T> root)
        {
            var minNode = root;  
            while (root.Left != null)
            {
                minNode = root.Left;
                root = root.Left;
            }
            return minNode;
        }
    }
}