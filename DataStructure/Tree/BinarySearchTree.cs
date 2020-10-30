namespace DataStructure.Tree
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        internal Node Root;

        public BinarySearchTree()
        {
            this.Root = null;
        }

        public TValue Search(TKey key)
        {
            Node now = this.SearchNode(key);
            if (now == null)
            {
                throw new KeyNotFoundException();
            }

            return now.Value;
        }

        public void Upsert(TKey key, TValue value)
        {
            if (this.Root == null)
            {
                this.Root = new Node(key, value);
            }
            else
            {
                Node now = this.Root;
                Node parent = this.Root.Parent;
                while (now != null)
                {
                    parent = now;
                    if (key.CompareTo(now.Key) < 0)
                    {
                        now = now.Left;
                    }
                    else if (key.CompareTo(now.Key) > 0)
                    {
                        now = now.Right;
                    }
                    else
                    {
                        now.Value = value;
                        return;
                    }
                }

                if (key.CompareTo(parent.Key) < 0)
                {
                    parent.Left = new Node(parent, null, null, key, value);
                }
                else
                {
                    parent.Right = new Node(parent, null, null, key, value);
                }
            }
        }

        public TValue Remove(TKey key)
        {
            Node deleteNode = this.SearchNode(key);
            if (deleteNode == null)
            {
                throw new KeyNotFoundException();
            }

            // Store the value for returning, since we will update node value in some case
            TValue result = deleteNode.Value;

            if (deleteNode.Right != null)
            {
                // Case 1, deletedNode's right child is not null;
                Node right = deleteNode.Right;
                if (right.Left != null)
                {
                    // Case 1.1, right's left child is not null
                    // Replace key / value in deleted node with the minimal node in its right subtree
                    // Then remove the minimal node in its right subtree
                    Node parent = right;
                    Node leftMin = right.Left;
                    while (leftMin.Left != null)
                    {
                        parent = leftMin;
                        leftMin = leftMin.Left;
                    }

                    deleteNode.Key = leftMin.Key;
                    deleteNode.Value = leftMin.Value;
                    parent.Left = null;
                }
                else
                {
                    // Case 1.2, right's left child is null
                    // Replace key / value in deleted node with its right child, and update the right child link
                    deleteNode.Key = right.Key;
                    deleteNode.Value = right.Value;
                    deleteNode.Right = right.Right;
                }
            }
            else if (deleteNode.Left != null)
            {
                // Case 2, deletedNode's left child is not null
                // Replace key / value in deleted node with its left child, and update the left child link
                Node left = deleteNode.Left;
                deleteNode.Key = left.Key;
                deleteNode.Value = left.Value;
                deleteNode.Left = left.Left;
            }
            else
            {
                // Case 3, deleted node is a leaf node, update parent's child link
                if (deleteNode.Parent == null)
                {
                    // Case 3.1 it's the only node in the tree
                    this.Root = null;
                }
                else if (deleteNode.Key.CompareTo(deleteNode.Parent.Key) < 0)
                {
                    // Case 3.2 it's parent's left child
                    deleteNode.Parent.Left = null;
                }
                else
                {
                    // Case 3.3 it's parent's right child
                    deleteNode.Parent.Right = null;
                }
            }

            return result;
        }

        private Node SearchNode(TKey key)
        {
            Node now = this.Root;
            while (now != null)
            {
                if (key.CompareTo(now.Key) < 0)
                {
                    now = now.Left;
                }
                else if (key.CompareTo(now.Key) > 0)
                {
                    now = now.Right;
                }
                else
                {
                    return now;
                }
            }

            return null;
        }

        internal class Node
        {
            public Node(Node parent, Node left, Node right, TKey key, TValue value)
            {
                this.Parent = parent;
                this.Left = left;
                this.Right = right;
                this.Key = key;
                this.Value = value;
            }

            public Node(TKey key, TValue value) : this(null, null, null, key, value)
            {
            }

            public Node Parent { get; internal set; }
            public Node Left { get; internal set; }
            public Node Right { get; internal set; }
            public TKey Key { get; internal set; }
            public TValue Value { get; internal set; }
        }
    }
}
