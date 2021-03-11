namespace OjProblems.LeetCode
{
    internal class Problem0715
    {
        public class RangeModule
        {
            private SegmentTreeNode Root = new SegmentTreeNode(0, 1000000000);
            public RangeModule()
            {
            }

            public void AddRange(int left, int right)
            {
                this.Root.AddRange(left, right);
            }

            public bool QueryRange(int left, int right)
            {
                return this.Root.QueryRange(left, right);
            }

            public void RemoveRange(int left, int right)
            {
                this.Root.RemoveRange(left, right);
            }

            private class SegmentTreeNode
            {
                // If we have 1 segment, it means some numbers exist in the interval
                private readonly int _minValue;
                private readonly int _maxValue;
                private SegmentTreeNode _leftChild;
                private SegmentTreeNode _rightChild;
                private bool _allExist;

                public SegmentTreeNode(int minValue, int maxValue)
                {
                    this._minValue = minValue;
                    this._maxValue = maxValue;
                    this._leftChild = null;
                    this._rightChild = null;
                    this._allExist = false;
                }

                public void AddRange(int left, int right)
                {
                    if (left <= this._minValue && this._maxValue <= right)
                    {
                        this._allExist = true;
                        this._leftChild = null;
                        this._rightChild = null;
                        return;
                    }

                    int middle = (this._minValue + this._maxValue) / 2;
                    if (left < middle)
                    {
                        // Add [left, middle) into left subtree
                        if (this._leftChild == null)
                        {
                            this._leftChild = new SegmentTreeNode(this._minValue, middle);
                        }

                        this._leftChild.AddRange(left, middle);
                    }

                    if (right > middle)
                    {
                        // Add [middle, right) into right subtree
                        if (this._rightChild == null)
                        {
                            this._rightChild = new SegmentTreeNode(middle, this._maxValue);
                        }

                        this._rightChild.AddRange(middle, right);
                    }
                }

                public bool QueryRange(int left, int right)
                {
                    if (this._minValue <= left && this._maxValue >= right && this._allExist)
                    {
                        return true;
                    }

                    int middle = (this._minValue + this._maxValue) / 2;
                    if (left < middle)
                    {
                        // Check [left, middle) all exist in left subtree
                        if (this._leftChild == null)
                        {
                            return false;
                        }

                        if (!this._leftChild.QueryRange(left, middle))
                        {
                            return false;
                        }
                    }

                    if (right > middle)
                    {
                        // Check [middle, right) all exists in right subtree
                        if (this._rightChild == null)
                        {
                            return false;
                        }

                        if (!this._rightChild.QueryRange(middle, right))
                        {
                            return false;
                        }
                    }

                    return true;
                }

                public void RemoveRange(int left, int right)
                {
                    if (left <= this._minValue && this._maxValue <= right)
                    {
                        this._allExist = false;
                        this._leftChild = null;
                        this._rightChild = null;
                        return;
                    }

                    int middle = (this._minValue + this._maxValue) / 2;
                    if (left < middle)
                    {
                        // Remove [left, middle) into left subtree
                        this._leftChild?.RemoveRange(left, middle);
                    }

                    if (right > middle)
                    {
                        // Remove [middle, right) into right subtree
                        this._rightChild?.RemoveRange(left, middle);
                    }
                }
            }
        }
    }
}
