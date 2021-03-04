namespace OjProblems.LeetCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Problem1727
    {
        public class Solution
        {
            public int LargestSubmatrix(int[][] matrix)
            {
                // Processing the matrix, after the procedure
                // matrix[i][j] is the max length of consecutive 1s which ends at i-th index in column j
                for (int i = 1; i < matrix.Length; ++i)
                {
                    for (int j = 0; j < matrix[i].Length; ++j)
                    {
                        if (matrix[i][j] == 1)
                        {
                            matrix[i][j] = matrix[i - 1][j] + 1;
                        }
                    }
                }

                int ans = 0;
                for (int i = 0; i < matrix.Length; ++i)
                {
                    List<int> tmp = matrix[i].ToList();
                    tmp.Sort();
                    for (int j = 0; j < tmp.Count; ++j)
                    {
                        ans = Math.Max(ans, tmp[j] * (tmp.Count - j));
                    }
                }

                return ans;
            }
        }
    }
}
