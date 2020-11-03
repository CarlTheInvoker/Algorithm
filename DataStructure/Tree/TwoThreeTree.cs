using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

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
                if (now.GetInsertIndex(key, out int index))
                {
                    now.Values[index] = value;
                    return;
                }
                else
                {
                    parent = now;
                    now = now.Children[index];
                }
            }

            if (parent == null)
            {
                this.Root = new Node(null, key, value);
                return;
            }

            // Insert a new key into the leaf node
            parent.InsertNewKey(key, value);
            this.Root = parent.SplitTemporary4Node();
        }

        public TValue Delete(TKey key)
        {
            if (this.Root == null)
            {
                throw new KeyNotFoundException();
            }

            Node now = this.Root;
            bool existInNode = now.GetInsertIndex(key, out int index);
            if (existInNode)
            {
                TransformRootWhenRemove(now, index + 1);
            }
            else
            {
                TransformRootWhenRemove(now, index);
            }

            // The root node has been merged into a 4-node, check existence again
            existInNode = now.GetInsertIndex(key, out index);

            while (!existInNode && now.Children[0] != null)
            {
                now = TransformNodeWhenRemove(now, index);
                existInNode = now.GetInsertIndex(key, out index);
            }

            if (!existInNode)
            {
                now.SplitTemporary4Node();
                throw new KeyNotFoundException();
            }

            TValue value = now.Values[index];

            // Goes to next node and remove minimum, update root
            if (now.Children[0] == null)
            {
                // The key exist in the leaf node
                now.MoveKeyToTheLeft(index, now.KeyCount - 1);
                now.KeyCount--;
                this.Root = now.SplitTemporary4Node();
            }
            else
            {
                Node nodeToDeleteMinimum = TransformNodeWhenRemove(now, index + 1);

                // After transform, we may move the key we want to remove to now's child
                existInNode = nodeToDeleteMinimum.GetInsertIndex(key, out index);
                while (existInNode && nodeToDeleteMinimum.Children[0] != null)
                {
                    now = nodeToDeleteMinimum;
                    nodeToDeleteMinimum = TransformNodeWhenRemove(nodeToDeleteMinimum, index + 1);
                    existInNode = nodeToDeleteMinimum.GetInsertIndex(key, out index);
                }

                if (existInNode)
                {
                    nodeToDeleteMinimum.MoveKeyToTheLeft(index, nodeToDeleteMinimum.KeyCount - 1);
                    nodeToDeleteMinimum.KeyCount--;
                    this.Root = now.SplitTemporary4Node();
                }
                else
                {
                    this.Root = RemoveMinimum(nodeToDeleteMinimum, out KeyValuePair<TKey, TValue> minimum);
                    // Set the minimum to the position where we delete
                    now.GetInsertIndex(key, out index);
                    now.Keys[index] = minimum.Key;
                    now.Values[index] = minimum.Value;
                }
            }

            return value;
        }

        internal class Node
        {
            /// <summary>
            /// Constructor of a 2-node
            /// </summary>
            /// <param name="parent"></param>
            /// <param name="key"></param>
            /// <param name="value"></param>
            /// <param name="leftChild"></param>
            /// <param name="rightChild"></param>
            public Node(Node parent, TKey key, TValue value, Node leftChild = null, Node rightChild = null)
            {
                this.KeyCount = 1;

                this.Keys = new TKey[3];
                this.Keys[0] = key;

                this.Values = new TValue[3];
                this.Values[0] = value;

                this.Parent = parent;
                this.Children = new Node[4] { leftChild, rightChild, null, null };
                this.UpdateChildrenParentLink();
            }

            public int KeyCount { get; internal set; }
            public TKey[] Keys { get; internal set; }
            public TValue[] Values { get; internal set; }
            public Node[] Children { get; internal set; }
            public Node Parent { get; internal set; }

            internal bool GetInsertIndex(TKey key, out int insertIndex)
            {
                for (int i = 0; i < this.KeyCount; ++i)
                {
                    insertIndex = i;
                    if (key.CompareTo(this.Keys[i]) < 0)
                    {
                        return false;
                    }
                    else if (key.CompareTo(this.Keys[i]) == 0)
                    {
                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }

                insertIndex = this.KeyCount;
                return false;
            }

            internal void InsertNewKey(TKey key, TValue value, Node leftChild = null, Node rightChild = null)
            {
                this.GetInsertIndex(key, out int index);
                
                // Move the key / value / child of parent node
                this.MoveKeyToTheRight(index, this.KeyCount);

                this.Keys[index] = key;
                this.Values[index] = value;
                this.KeyCount++;

                this.Children[index] = leftChild;
                this.Children[index + 1] = rightChild;
                this.UpdateChildrenParentLink();
            }

            internal Node SplitTemporary4Node()
            {
                if (this.KeyCount == 3)
                {
                    // Split the node to 2 2-nodes: create a right sibling node
                    Node sibling = new Node(this.Parent, this.Keys[2], this.Values[2], this.Children[2], this.Children[3]);

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
                        // Create a new node as parent
                        Node parent = new Node(null, middleKey, middleValue, this, sibling);
                    }
                    else
                    {
                        // Insert new key into parent
                        Node parent = this.Parent;
                        parent.InsertNewKey(middleKey, middleValue, this, sibling);
                    }
                }

                if (this.Parent != null)
                {
                    return this.Parent.SplitTemporary4Node();
                }
                else
                {
                    return this;
                }
            }

            /// Move keys[startIndex, endIndex) (values / children) to the right
            internal void MoveKeyToTheRight(int startIndex, int endIndex)
            {
                endIndex = Math.Min(this.KeyCount, endIndex);
                this.Children[endIndex + 1] = this.Children[endIndex];
                while (endIndex > startIndex)
                {
                    this.Keys[endIndex] = this.Keys[endIndex - 1];
                    this.Values[endIndex] = this.Values[endIndex - 1];
                    this.Children[endIndex] = this.Children[endIndex - 1];
                    endIndex--;
                }
            }

            /// Move keys(startIndex, endIndex] (values / children) to the left
            internal void MoveKeyToTheLeft(int startIndex, int endIndex)
            {
                while (startIndex < endIndex)
                {
                    this.Keys[startIndex] = this.Keys[startIndex + 1];
                    this.Values[startIndex] = this.Values[startIndex + 1];
                    this.Children[startIndex] = this.Children[startIndex + 1];
                    startIndex++;
                }
                
                this.Children[startIndex] = this.Children[startIndex + 1];
            }

            internal void UpdateChildrenParentLink()
            {
                if (this.Children[0] != null)
                {
                    for (int i = 0; i < this.KeyCount + 1; ++i)
                    {
                        this.Children[i].Parent = this;
                    }
                }
            }
        }
        
        private Node SearchNode(TKey key)
        {
            Node now = this.Root;
            while (now != null)
            {
                if (now.GetInsertIndex(key, out int index))
                {
                    return now;
                }
                else
                {
                    now = now.Children[index];
                }
            }

            return null;
        }

        /// Current node is a 2-node, right sibling is a 3-node
        private static void GetKeyFromRightSibling(Node now, int index, Node parent, Node rightSibling)
        {
            now.InsertNewKey(parent.Keys[index], parent.Values[index], now.Children[1], rightSibling.Children[0]);

            parent.Keys[index] = rightSibling.Keys[0];
            parent.Values[index] = rightSibling.Values[0];

            rightSibling.MoveKeyToTheLeft(0, rightSibling.KeyCount - 1);
            rightSibling.KeyCount--;
        }

        /// Current node is a 2-node, left sibling is a 3-node
        private static void GetKeyFromLeftSibling(Node now, int index, Node parent, Node leftSibling)
        {
            now.InsertNewKey(parent.Keys[index - 1], parent.Values[index - 1], leftSibling.Children[leftSibling.KeyCount], now.Children[0]);

            parent.Keys[index - 1] = leftSibling.Keys[1];
            parent.Values[index - 1] = leftSibling.Values[1];

            leftSibling.KeyCount--;
        }

        /// Left and right are 2 2-nodes, parent is 3-node or 4-node
        /// Left become a 4-node with 3 keys (left.Keys[0], parent.Keys[index], right.Keys[0])
        private static void MergeKeysToLeftNode(Node left, Node parent, Node right, int index)
        {
            left.InsertNewKey(parent.Keys[index], parent.Values[index], left.Children[1], right.Children[0]);
            left.InsertNewKey(right.Keys[0], right.Values[0], right.Children[0], right.Children[1]);

            parent.KeyCount--;
            parent.MoveKeyToTheLeft(index, parent.KeyCount - 1);
            parent.Children[index] = left;
            parent.UpdateChildrenParentLink();
        }

        /// Left and right are 2 2-nodes, parent is 3-node or 4-node
        /// Left become a 4-node with 3 keys (left.Keys[0], parent.Keys[index], right.Keys[0])
        private static void MergeKeysToRightNode(Node left, Node parent, Node right, int index)
        {
            right.InsertNewKey(parent.Keys[index - 1], parent.Values[index - 1], left.Children[1], right.Children[0]);
            right.InsertNewKey(left.Keys[0], left.Values[0], left.Children[0], left.Children[1]);

            parent.KeyCount--;
            parent.MoveKeyToTheLeft(index - 1, parent.KeyCount - 1);
            parent.Children[index] = right;
            parent.UpdateChildrenParentLink();
        }

        /// Will transform node when:
        ///     1. node is 2-node and 2 children are 2-node => node become a 4-node
        ///     2. root is 2-node and right child is a 3-node => node's left child become a 3-node and right child become a 2-node
        /// Return next node to search, must be a 3-node or 4-node
        private static Node TransformRootWhenRemove(Node rootNode, int nextNodeIndex)
        {
            if (rootNode.KeyCount == 1 &&
                rootNode.Children[nextNodeIndex] != null &&
                rootNode.Children[nextNodeIndex].KeyCount == 1)
            {
                if (rootNode.Children[1 - nextNodeIndex].KeyCount == 1)
                {
                    // Update parent node, should only happen when it is the root in the tree
                    Node left = rootNode.Children[0];
                    Node right = rootNode.Children[1];
                    rootNode.InsertNewKey(left.Keys[0], left.Values[0], left.Children[0], left.Children[1]);
                    rootNode.InsertNewKey(right.Keys[0], right.Values[0], right.Children[0], right.Children[1]);

                    return rootNode;
                }
                else if (nextNodeIndex == 0)
                {
                    GetKeyFromRightSibling(rootNode.Children[0], 0, rootNode, rootNode.Children[1]);
                    return rootNode.Children[0];
                }
                else
                {
                    GetKeyFromLeftSibling(rootNode.Children[1], 1, rootNode, rootNode.Children[0]);
                    return rootNode.Children[1];
                }
            }

            return rootNode;
        }

        /// Because the transform in the root, node now must be a 3-node or 4-node
        /// Do some transform to make sure next node is not a 2-node
        private static Node TransformNodeWhenRemove(Node node, int nextNodeIndex)
        {
            if (node.Children[nextNodeIndex] != null && node.Children[nextNodeIndex].KeyCount == 1)
            {
                if (nextNodeIndex >= 1 && node.Children[nextNodeIndex - 1].KeyCount == 1)
                {
                    // next and its left sibling is 2-node
                    MergeKeysToLeftNode(node.Children[0], node, node.Children[1], 0);
                }
                else if (nextNodeIndex >= 1 && node.Children[nextNodeIndex - 1].KeyCount > 1)
                {
                    // its left sibling is not a 2-node
                    GetKeyFromLeftSibling(node.Children[nextNodeIndex], nextNodeIndex, node, node.Children[nextNodeIndex - 1]);
                }
                else if (nextNodeIndex <= node.KeyCount && node.Children[nextNodeIndex + 1].KeyCount == 1)
                {
                    // next and its right sibling is 2-node
                    MergeKeysToLeftNode(node.Children[nextNodeIndex], node, node.Children[nextNodeIndex + 1], nextNodeIndex);
                }
                else
                {
                    GetKeyFromRightSibling(node.Children[nextNodeIndex], nextNodeIndex, node, node.Children[nextNodeIndex + 1]);
                }
            }

            return node.Children[nextNodeIndex];
        }

        /// Remove the minimum, return the root node
        /// Node must be a 3-node or 4-node
        private static Node RemoveMinimum(Node node, out KeyValuePair<TKey, TValue> result)
        {
            while (node.Children[0] != null)
            {
                node = TransformNodeWhenRemove(node, 0);
            }

            result = new KeyValuePair<TKey, TValue>(node.Keys[0], node.Values[0]);
            if (node.KeyCount == 1)
            {
                return null;
            }
            
            node.MoveKeyToTheLeft(0, node.KeyCount - 1);
            node.KeyCount--;
            return node.SplitTemporary4Node();
        }
    }
}