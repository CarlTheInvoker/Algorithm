namespace OjProblems.LeetCode
{
    using System;

    internal class Problem0923
    {
        public class Solution
        {
            public int ThreeSumMulti(int[] arr, int target)
            {
                int[] count = new int[101];
                for (int i = 0; i < 101; ++i)
                {
                    count[i] = 0;
                }

                foreach (int num in arr)
                {
                    count[num]++;
                }

                long ans = 0;
                foreach (int first in arr)
                {
                    count[first]--;
                    int remain = target - first;
                    int start = Math.Max(0, remain - 100);
                    int end = Math.Min(100, remain);
                    if (start <= end)
                    {
                        for (int second = start; second <= (start + end) / 2; ++second)
                        {
                            int third = remain - second;
                            if (third == second)
                            {
                                ans += count[third] * (count[third] - 1) / 2;
                            }
                            else
                            {
                                ans += count[second] * count[third];
                            }
                        }
                    }
                }

                return (int)(ans % 1000000007);
            }
        }
    }
}
