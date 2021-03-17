namespace OjProblems.LeetCode
{
    using System;
    internal class Problem1373
    {
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }

        public class Solution
        {
            private int maxSumBst = 0;

            public int MaxSumBST(TreeNode root)
            {
                this.CalculateTreeNodeInfo(root);
                return this.maxSumBst;
            }

            public TreeNodeInfo CalculateTreeNodeInfo(TreeNode node)
            {
                if (node == null)
                {
                    return null;
                }

                var left = this.CalculateTreeNodeInfo(node.left);
                var right = this.CalculateTreeNodeInfo(node.right);

                TreeNodeInfo result = new TreeNodeInfo()
                {
                    IsBinarySearchTree = (left == null || (left.IsBinarySearchTree && left.Max < node.val)) && 
                                         (right == null || (right.IsBinarySearchTree && node.val < right.Min)),
                    Min = left?.Min ?? node.val,
                    Max = right?.Max ?? node.val,
                    Sum = (left?.Sum ?? 0) + (right?.Sum ?? 0) + node.val
                };

                if (result.IsBinarySearchTree)
                {
                    this.maxSumBst = Math.Max(this.maxSumBst, result.Sum);
                }

                return result;
            }
        }

        public class TreeNodeInfo
        {
            public bool IsBinarySearchTree;
            public int Min;
            public int Max;
            public int Sum;
        }
    }
}
