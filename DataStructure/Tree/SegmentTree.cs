namespace DataStructure.Tree
{
    using System;

    /// <summary>
    /// Assume the data associated with the interval is a bool to indicate if the interval exist
    /// </summary>
    public class SegmentTree
    {
        private readonly int minValue;
        private readonly int maxValue;
        public SegmentTree(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.Root = null;
        }

        internal Node Root { get; private set; }

        /// And the query operation is to check if all numbers in the interval are same
        /// If an interval is empty, return false
        public bool Query(int left, int right)
        {
            this.ValidateInput(left, right);
            return this.Query(this.Root, left, right);
        }

        public void Add(int left, int right)
        {
            this.ValidateInput(left, right);
            if (this.Root == null)
            {
                this.Root = new Node(this.minValue, this.maxValue, false);
            }

            this.Root = this.Add(this.Root, left, right, this.Root.Left, this.Root.Right);
        }

        public void Remove(int left, int right)
        {
            this.ValidateInput(left, right);
            this.Root = this.Remove(this.Root, left, right);
        }

        private void ValidateInput(int left, int right)
        {
            if (left < this.minValue || right > this.maxValue)
            {
                throw new ArgumentException($"Valid number must be in the interval [{this.minValue}, {this.maxValue})");
            }
        }

        private bool Query(Node node, int left, int right)
        {
            if (left >= right)
            {
                // empty interval return exist
                return true;
            }

            if (node == null || left < node.Left || right > node.Right)
            {
                return false;
            }

            if (node.Exist)
            {
                return true;
            }

            int middle = (node.Left + node.Right) / 2;
            if (right < middle)
            {
                return this.Query(node.LeftChild, left, right);
            }
            else if (left > middle)
            {
                return this.Query(node.RightChild, left, right);
            }

            return this.Query(node.LeftChild, left, middle) && this.Query(node.RightChild, middle, right);
        }

        private Node Add(Node node, int left, int right, int intervalLeft, int intervalRight)
        {
            if (intervalLeft >= right || intervalRight <= left)
            {
                return node;
            }

            if (node == null)
            {
                node = new Node(intervalLeft, intervalRight, false);
                return this.Add(node, left, right, node.Left, node.Right);
            }

            if (left <= node.Left && node.Right <= right)
            {
                node.Exist = true;
                node.LeftChild = null;
                node.RightChild = null;
                return node;
            }

            int middle = (node.Left + node.Right) / 2;
            node.LeftChild = this.Add(node.LeftChild, left, right, node.Left, middle);
            node.RightChild = this.Add(node.RightChild, left, right, middle, node.Right);

            if (node.LeftChild != null && node.LeftChild.Exist && node.RightChild != null && node.RightChild.Exist)
            {
                // Merge the node
                node.LeftChild = null;
                node.RightChild = null;
                node.Exist = true;
            }

            return node;
        }

        private Node Remove(Node node, int left, int right)
        {
            if (node == null || node.Left >= right || node.Right <= left)
            {
                return node;
            }

            if (left <= node.Left && node.Right <= right)
            {
                return null;
            }

            int middle = (node.Left + node.Right) / 2;
            if (node.Exist)
            {
                node.LeftChild = new Node(node.Left, middle, true);
                node.RightChild = new Node(middle, node.Right, true);
            }

            node.LeftChild = this.Remove(node.LeftChild, left, right);
            node.RightChild = this.Remove(node.RightChild, left, right);
            node.Exist = false;
            if (node.LeftChild == null && node.RightChild == null)
            {
                return null;
            }

            return node;
        }

        /// All operations in the SegmentTreeInterval satisfy: this.Left <= left < right <= this.Right
        internal class Node
        {
            public Node(int left, int right, bool exist = false)
            {
                this.Left = left;
                this.Right = right;
                this.Exist = exist;
            }

            public int Left { get; set; }
            public int Right { get; set; }
            public bool Exist { get; set; }
            public Node LeftChild { get; set; }
            public Node RightChild { get; set; }
        }
    }
}
