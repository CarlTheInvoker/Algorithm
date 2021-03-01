namespace Algorithm.Tree
{
    using System;

    /// <summary>
    /// Segment tree is very useful when
    ///     - The data you need to process is associated with interval
    ///     - The operation on the interval can be easily calculated by the operation result on the sub-interval
    ///         ( func [a:b) = g(func[a:c), func[c:b)) where a <= c <= b, and g is simple )
    ///     - Known the minValue and maxValue of the interval
    ///     
    /// Then the Query / Add / Remove all can be handled in O(log(maxValue - minValue)), assume g is O(1)
    /// 
    /// Assume the operation on the interval is if the interval is contained in the tree, so 
    ///     func [a:b) = func[a:c) && func[c:b)
    ///     
    /// This is the solution of leetcode0715 - Range Module.
    /// </summary>
    public class SegmentTree
    {
        private readonly int MinValue;
        private readonly int MaxValue;
        private SegmentTreeInterval root;
        public SegmentTree(int minValue, int maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public bool Query(int left, int right)
        {
            this.ValidateInput(left, right);
            if (this.root == null)
            {
                return false;
            }

            if (left == right)
            {
                return false;
            }

            return this.root.Query(left, right);
        }

        public void Add(int left, int right)
        {
            this.ValidateInput(left, right);
            if (left == right)
            {
                return;
            }

            if (this.root == null)
            {
                this.root = new SegmentTreeInterval(this.MinValue, this.MaxValue);
            }

            this.root.Add(left, right);
        }

        public void Remove(int left, int right)
        {
            this.ValidateInput(left, right);
            if (left == right)
            {
                return;
            }

            this.root = this.root?.Remove(left, right);
        }

        private void ValidateInput(int left, int right)
        {
            if (left < this.MinValue || right > this.MaxValue)
            {
                throw new ArgumentException($"Valid number must be in the interval [{this.MinValue}, {this.MaxValue})");
            }

            if (left > right)
            {
                throw new ArgumentException($"{left} must smaller than {right}");
            }
        }

        /// All operations in the SegmentTreeInterval satisfy: this.Left <= left < right <= this.Right
        private class SegmentTreeInterval
        {
            public SegmentTreeInterval(int left, int right)
            {
                this.Left = left;
                this.Right = right;
                this.AllElementsExist = false;
            }

            public int Left;
            public int Right;
            public SegmentTreeInterval LeftChild { get; set; }
            public SegmentTreeInterval RightChild { get; set; }
            public bool AllElementsExist { get; set; } // For the leaf node, it must be true

            public bool Query(int left, int right)
            {
                if (this.AllElementsExist)
                {
                    return true;
                }

                int middle = (this.Left + this.Right) / 2;
                if (right <= middle)
                {
                    return this.LeftChild != null && this.LeftChild.Query(left, right);
                }
                else if (left >= middle)
                {
                    return this.RightChild != null && this.RightChild.Query(left, right);
                }
                else
                {
                    return this.LeftChild != null && this.LeftChild.Query(left, middle) && this.RightChild != null && this.RightChild.Query(middle, right);
                }
            }

            public void Add(int left, int right)
            {
                if (this.AllElementsExist)
                {
                    return;
                }

                if (this.Left == left && this.Right == right)
                {
                    this.AllElementsExist = true;
                    return;
                }

                int middle = (this.Left + this.Right) / 2;
                if (right <= middle)
                {
                    this.GetOrCreateLeftChild().Add(left, right);
                }
                else if (left >= middle)
                {
                    this.GetOrCreateRightChild().Add(left, right);
                }
                else
                {
                    this.GetOrCreateLeftChild().Add(left, middle);
                    this.GetOrCreateRightChild().Add(middle, right);
                }
            }

            public SegmentTreeInterval Remove(int left, int right)
            {
                if (this.Left == left && this.Right == right)
                {
                    return null;
                }
                
                int middle = (this.Left + this.Right) / 2;
                if (this.AllElementsExist)
                {
                    // No sub-interval exist, create first then udpate it
                    this.AllElementsExist = false;
                    this.LeftChild = new SegmentTreeInterval(this.Left, (this.Left + this.Right) / 2);
                    this.LeftChild.AllElementsExist = true;

                    this.RightChild = new SegmentTreeInterval((this.Left + this.Right) / 2, this.Right);
                    this.RightChild.AllElementsExist = true;
                }
                
                if (right <= middle)
                {
                    this.LeftChild = this.LeftChild?.Remove(left, right);
                }
                else if (left >= middle)
                {
                    this.RightChild = this.RightChild?.Remove(left, right);
                }
                else
                {
                    this.LeftChild = this.LeftChild?.Remove(left, middle);
                    this.RightChild = this.RightChild?.Remove(middle, right);
                }

                if (this.LeftChild == null && this.RightChild == null)
                {
                    return null;
                }

                return this;
            }

            private SegmentTreeInterval GetOrCreateLeftChild()
            {
                if (this.LeftChild == null)
                {
                    this.LeftChild = new SegmentTreeInterval(this.Left, (this.Left + this.Right) / 2);
                }

                return this.LeftChild;
            }

            private SegmentTreeInterval GetOrCreateRightChild()
            {
                if (this.RightChild == null)
                {
                    this.RightChild = new SegmentTreeInterval((this.Left + this.Right) / 2, this.Right);
                }

                return this.RightChild;
            }
        }
    }
}
