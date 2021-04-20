using System;
using System.Linq;

namespace OjProblems.LeetCode
{
    internal class Problem0329
    {
        public class Solution
        {
            // dp[i][j] is the max length when starting at i,j
            private int[][] dp;
            private int height;
            private int width;
            public int LongestIncreasingPath(int[][] matrix)
            {
                this.height = matrix.Length;
                this.width = matrix[0].Length;
                this.dp = new int[this.height][];
                for (int i = 0; i < this.height; ++i)
                {
                    this.dp[i] = new int[this.width];
                    for (int j = 0; j < this.width; ++j)
                    {
                        this.dp[i][j] = -1;
                    }
                }

                int ans = 1;
                for (int i = 0; i < this.height; ++i)
                {
                    for (int j = 0; j < this.width; ++j)
                    {
                        ans = Math.Max(ans, this.Solve(matrix, i, j));
                    }
                }

                return ans;
            }

            private int Solve(int[][] matrix, int i, int j)
            {
                if (this.dp[i][j] != -1)
                {
                    return this.dp[i][j];
                }

                this.dp[i][j] = 1;
                if (i > 0 && matrix[i][j] < matrix[i - 1][j])
                {
                    this.dp[i][j] = Math.Max(this.dp[i][j], this.Solve(matrix, i - 1, j) + 1);
                }

                if (i < this.height - 1 && matrix[i][j] < matrix[i + 1][j])
                {
                    this.dp[i][j] = Math.Max(this.dp[i][j], this.Solve(matrix, i + 1, j) + 1);
                }

                if (j > 0 && matrix[i][j] < matrix[i][j - 1])
                {
                    this.dp[i][j] = Math.Max(this.dp[i][j], this.Solve(matrix, i , j - 1) + 1);
                }

                if (j < this.width - 1 && matrix[i][j] < matrix[i][j + 1])
                {
                    this.dp[i][j] = Math.Max(this.dp[i][j], this.Solve(matrix, i, j + 1) + 1);
                }

                return this.dp[i][j];
            }
        }
    }
}
