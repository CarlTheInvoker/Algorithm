namespace DataStructure.Tree
{
    using System;

    /// <summary>
    /// Consider the the data associated with an interval is the maximum value of the interval
    /// And the operation on the interval is increasing all numbers in the interval by 1
    ///
    /// And by default all the value is 0.
    /// This is basically the solution of leetcode 0732 - My Calendar III
    /// </summary>
    public class LazySegmentTree
    {
        private readonly int _left;
        private readonly int _right;
        public LazySegmentTree(int left, int right)
        {
            this._left = left;
            this._right = right;
            this.Root = new Node(left, right);
        }

        internal Node Root { get; private set; }
        
        public int Query(int left, int right)
        {
            return this.Query(this.Root, left, right);
        }

        public void Update(int left, int right)
        {
            this.Update(this.Root, left, right);
        }

        private int Query(Node node, int left, int right)
        {
            if (node == null || right <= node.Left || node.Right <= left)
            {
                return 0;
            }

            if (left <= node.Left && node.Right <= right)
            {
                return node.Value + node.Lazy;
            }

            this.PushLazyDown(node);
            return Math.Max(this.Query(node.LeftChild, left, right), this.Query(node.RightChild, left, right));
        }

        private void Update(Node node, int left, int right)
        {
            if (left <= node.Left && node.Right <= right)
            {
                // Update whole interval
                node.Lazy++;
                return;
            }

            if (node.Right <= left || right <= node.Left)
            {
                // No intersection, nothing to do
                return;
            }

            // Push the lazy down, will create child node if necessary
            this.PushLazyDown(node);
            if (node.Right - node.Left > 1)
            {
                int middle = node.GetMiddle();

                this.Update(node.LeftChild, left, right);
                this.Update(node.RightChild, left, right);

                // Update the node value
                node.Value = Math.Max(this.Query(node.LeftChild, node.Left, middle), this.Query(node.RightChild, middle, node.Right));
            }
        }

        private void PushLazyDown(Node node)
        {
            if (node.Right - node.Left > 1)
            {
                // If need to create child node
                int middle = node.GetMiddle();
                if (node.LeftChild == null)
                {
                    node.LeftChild = new Node(node.Left, middle);
                }

                if (node.RightChild == null)
                {
                    node.RightChild = new Node(middle, node.Right);
                }

                node.LeftChild.Lazy += node.Lazy;
                node.RightChild.Lazy += node.Lazy;
            }

            node.Value += node.Lazy;
            node.Lazy = 0;
        }

        internal class Node
        {
            public Node(int left, int right)
            {
                this.Left = left;
                this.Right = right;
            }

            public int Lazy { get; set; }
            public int Value { get; set; }
            public int Left { get; }
            public int Right { get; }
            public Node LeftChild { get; set; }
            public Node RightChild { get; set; }

            public int GetMiddle()
            {
                return (this.Left + this.Right) / 2;
            }
        }
    }
}
