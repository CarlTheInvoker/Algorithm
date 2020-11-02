namespace DataStructure.Tree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TwoThreeTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TwoThreeTree()
        {
            this.Root = null;
        }

        internal Node Root { get; private set; }

        public TValue Search(TKey key)
        {
            Node node = this.SearchNode(key);
            if (node == null)
            {
                throw new KeyNotFoundException();
            }

            if (key.CompareTo(node.Keys[0]) == 0)
            {
                return node.Values[0];
            }

            return node.Values[1];
        }

        /// <summary>
        /// If key already exist, just update the value
        /// Otherwise, insert the key into the leaf node
        ///     - If leaf node is a 2-node, it becomes a 3-node
        ///     - If leaf node is a 3-node, split it and generate 2 2-nodes, insert a new key in the parent node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Upsert(TKey key, TValue value)
        {
            Node parent = null;
            Node now = this.Root;
            while (now != null)
            {
                if (key.CompareTo(now.Keys[0]) == 0)
                {
                    now.Values[0] = value;
                    return;
                }
                else if (now.KeyCount > 1 && key.CompareTo(now.Keys[1]) == 0)
                {
                    now.Values[1] = value;
                    return;
                }

                parent = now;
                now = now.Children[now.GetInsertIndex(key)];
            }

            if (parent == null)
            {
                this.Root = new Node(null, key, value);
                return;
            }

            // Insert a new key into parent which is a leaf node now
            // Rename it for better readability
            Node leaf = parent;
            int insertIndex = leaf.GetInsertIndex(key);
            leaf.MoveKeyValue(insertIndex);
            leaf.Keys[insertIndex] = key;
            leaf.Values[insertIndex] = value;
            leaf.KeyCount++;
            leaf.SplitIfNeeded();

            if (this.Root.Parent != null)
            {
                this.Root = this.Root.Parent;
            }
        }

        public TValue Delete(TKey key)
        {
            throw new NotImplementedException();
        }

        private Node SearchNode(TKey key)
        {
            Node now = this.Root;
            while (now != null)
            {
                if (key.CompareTo(now.Keys[0]) == 0 || (now.KeyCount > 1 && key.CompareTo(now.Keys[1]) == 0))
                {
                    return now;
                }

                now = now.Children[now.GetInsertIndex(key)];
            }

            return null;
        }

        /// <summary>
        /// The node could contain:
        ///     - 1 key, then it is a 2-node which has 2 children
        ///     - 2 keys, then it is a 3-node which has 3 children
        /// </summary>
        internal class Node
        {
            public Node(Node parent, TKey key, TValue value)
            {
                this.Parent = parent;
                this.Children = new Node[4];
                this.Keys = new TKey[3];
                this.Values = new TValue[3];

                this.KeyCount = 1;
                this.Keys[0] = key;
                this.Values[0] = value;
            }

            public int KeyCount{ get; internal set; }
            public TKey[] Keys { get; internal set; }
            public TValue[] Values { get; internal set; }
            public Node[] Children { get; internal set; }
            public Node Parent { get; internal set; }

            /// <summary>
            /// If it is a temporary 4-node, split it into 2 2-nodes, and insert the middle key/value pair into its parent
            /// </summary>
            internal void SplitIfNeeded()
            {
                if (this.KeyCount != 3)
                {
                    return;
                }

                // Split the node to 2 2-nodes: create a right sibling node
                Node sibling = new Node(this.Parent, this.Keys[2], this.Values[2]);
                sibling.Children[0] = this.Children[2];
                if (sibling.Children[0] != null)
                {
                    // Update child's parent, otherwise it will still point to current node
                    sibling.Children[0].Parent = sibling;
                }

                sibling.Children[1] = this.Children[3];
                if (sibling.Children[1] != null)
                {
                    sibling.Children[1].Parent = sibling;
                }

                // Split the child node to 2 2-nodes: update current node to a 2-node as parent's left child
                this.KeyCount = 1;
                this.Children[2] = null;
                this.Children[3] = null;

                // Will insert the middle key value into parent
                // note that we don't clear the keys and values we need to access it after check keyCount 
                TKey middleKey = this.Keys[1];
                TValue middleValue = this.Values[1];

                if (this.Parent == null)
                {
                    Node parent = new Node(null, middleKey, middleValue);
                    
                    // Update the parent's children link
                    this.Parent = parent;
                    sibling.Parent = parent;

                    parent.Children[0] = this;
                    parent.Children[1] = sibling;
                }
                else
                {
                    Node parent = this.Parent;
                    int index = parent.GetInsertIndex(this.Keys[1]);

                    // Move the key / value / child of parent node
                    parent.MoveKeyValue(index);

                    // Set the right key
                    parent.Keys[index] = middleKey;
                    parent.Values[index] = middleValue;

                    // Set the right children link
                    parent.Children[index + 1] = sibling;
                    parent.Children[index] = this;

                    // Update the key count of parent
                    parent.KeyCount++;
                    parent.SplitIfNeeded();
                }
            }

            internal int GetInsertIndex(TKey key)
            {
                if (key.CompareTo(this.Keys[0]) < 0)
                {
                    return 0;
                }
                else if (this.KeyCount == 1 || key.CompareTo(this.Keys[1]) < 0)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }

            /// <summary>
            /// Move all the key value and children starting from index-th key right
            /// </summary>
            /// <param name="index">The starting index</param>
            internal void MoveKeyValue(int index)
            {
                for (int i = this.KeyCount; i > index; --i)
                {
                    this.Keys[i] = this.Keys[i - 1];
                    this.Values[i] = this.Values[i - 1];
                }

                for (int i = this.KeyCount + 1; i > index; --i)
                {
                    this.Children[i] = this.Children[i - 1];
                }
            }
        }
    }
}
