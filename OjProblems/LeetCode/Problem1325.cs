namespace OjProblems.LeetCode
{
    internal class Problem1325
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
            public TreeNode RemoveLeafNodes(TreeNode root, int target)
            {
                if (root == null)
                {
                    return null;
                }

                root.left = this.RemoveLeafNodes(root.left, target);
                root.right = this.RemoveLeafNodes(root.right, target);

                if (root.left == null && root.right == null && root.val == target)
                {
                    return null;
                }

                return root;
            }
        }
    }
}
