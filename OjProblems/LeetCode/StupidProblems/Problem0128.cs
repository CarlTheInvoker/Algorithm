namespace OjProblems.LeetCode.StupidProblems
{
    using System;
    using System.Collections.Generic;

    internal class Problem0128
    {
        public class Solution
        {
            public int LongestConsecutive(int[] nums)
            {
                Dictionary<int, bool> visited = new Dictionary<int, bool>();
                foreach (int num in nums)
                {
                    visited[num] = false;
                }

                int ans = 0;
                foreach (int num in nums)
                {
                    if (visited[num])
                    {
                        int left = num, right = num;
                        while (visited.ContainsKey(left))
                        {
                            visited[left] = true;
                            left--;
                        }

                        while (visited.ContainsKey(right))
                        {
                            visited[right] = true;
                            right++;
                        }

                        ans = Math.Max(right - left - 1, ans);
                    }
                }

                return ans;
            }
        }
    }
}
