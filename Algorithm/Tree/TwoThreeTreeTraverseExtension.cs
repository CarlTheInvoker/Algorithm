namespace Algorithm.Tree
{
    using System;
    using DataStructure.Tree;

    public static class TwoThreeTreeTraverseExtension
    {
        public static void PreOrderTraverse<TKey, TValue>(this TwoThreeTree<TKey, TValue> tree, Action<TKey, TValue> action)
            where TKey : IComparable<TKey>
        {
            PreOrderTraverse(tree.Root, action);
        }

        private static void PreOrderTraverse<TKey, TValue>(TwoThreeTree<TKey, TValue>.Node node, Action<TKey, TValue> action)
            where TKey : IComparable<TKey>
        {
            if (node == null)
            {
                return;
            }

            PreOrderTraverse(node.Children[0], action);
            action(node.Keys[0], node.Values[0]);
            PreOrderTraverse(node.Children[1], action);
            if (node.KeyCount == 2)
            {
                action(node.Keys[1], node.Values[1]);
                PreOrderTraverse(node.Children[2], action);
            }
        }
    }
}
