namespace OjProblems.LeetCode
{
    using System;

    internal class Problem1145
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
            public bool BtreeGameWinningMove(TreeNode root, int n, int x)
            {
                int leftSubtreeCount = 0, rightSubtreeCount = 0;
                if (root.val == x)
                {
                    leftSubtreeCount = this.GetNodeCount(root.left);
                    rightSubtreeCount = n - leftSubtreeCount - 1;
                    return Math.Abs(leftSubtreeCount - rightSubtreeCount) > 1; // choose either left subtree or right subtree
                }

                TreeNode node = this.FindNode(root, x);
                leftSubtreeCount = this.GetNodeCount(node.left);
                rightSubtreeCount = this.GetNodeCount(node.right);
                return n > (leftSubtreeCount + rightSubtreeCount + 1) * 2 || // choose its parent
                       leftSubtreeCount * 2 > n || // choose left subtree
                       rightSubtreeCount * 2 > n; // choose right subtree
            }

            private TreeNode FindNode(TreeNode root, int target)
            {
                if (root == null)
                {
                    return null;
                }

                if (root.val == target)
                {
                    return root;
                }

                return this.FindNode(root.left, target) ?? this.FindNode(root.right, target); ;
            }

            private int GetNodeCount(TreeNode root)
            {
                if (root == null)
                {
                    return 0;
                }

                return this.GetNodeCount(root.left) + this.GetNodeCount(root.right) + 1;
            }
        }
    }
}
