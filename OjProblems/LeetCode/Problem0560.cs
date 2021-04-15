namespace OjProblems.LeetCode
{
    using System.Collections.Generic;

    internal class Problem0560
    {
        public class Solution
        {
            public int SubarraySum(int[] nums, int k)
            {
                Dictionary<int, int> count = new Dictionary<int, int>();
                count[0] = 1;
                int sum = 0;
                int res = 0;
                foreach (int num in nums)
                {
                    sum += num;
                    if (!count.ContainsKey(sum))
                    {
                        count[sum] = 0;
                    }

                    count[sum]++;
                    if (count.ContainsKey(sum - k))
                    {
                        res += count[sum - k];
                        if (k == 0)
                        {
                            res--;
                        }
                    }
                }

                return res;
            }
        }
    }
}
