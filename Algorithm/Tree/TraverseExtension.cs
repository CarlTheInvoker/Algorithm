namespace Algorithm.Tree
{
    using System;
    using DataStructure.Tree;
    
    public static class TraverseExtension
    {
        public static void PreOrderTraverse<TKey, TValue>(this BinarySearchTree<TKey, TValue> tree, Action<TKey, TValue> action)
            where TKey : IComparable<TKey>
        {
            PreOrderTraverse(tree.Root, action);
        }

        private static void PreOrderTraverse<TKey, TValue>(BinarySearchTree<TKey, TValue>.Node node, Action<TKey, TValue> action)
            where TKey : IComparable<TKey>
        {
            if (node == null)
            {
                return;
            }

            PreOrderTraverse(node.Left, action);
            action(node.Key, node.Value);
            PreOrderTraverse(node.Right, action);
        }
    }
}
