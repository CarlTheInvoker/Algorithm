namespace OjProblems.LeetCode
{
    using System;

    public class Problem0754
    {
        public class Solution
        {
            // For step n, we could reach all numbers in 
            // -sum, -sum + 2, -sum + 4, ..., sum - 4, sum - 2, sum
            // And we already know sum = (n + 1) * n / 2
            // So we just need to find a n which satisfy:
            //     1. sum(1..n) % 2 == target % 2;
            //     2. (n + 1) * n / 2 >= target <==> n + 0.5 >= sqrt(2 * target + 0.25)
            public int ReachNumber(int target)
            {
                long tmp = target;
                if (target < 0)
                {
                    tmp = -target;
                }

                long n = (int) Math.Sqrt(tmp * 2);
                long sum = n * (n + 1) / 2;
                while (sum < tmp || (sum - tmp) % 2 != 0)
                {
                    n++;
                    sum += n;
                }

                return (int)n;
            }
        }
    }
}
