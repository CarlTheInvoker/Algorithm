namespace OjProblems.LeetCode
{
    internal class Problem0793
    {
        // We just need to check if there exist one number num satisfying that num! has k zeros in the end.
        // If yes, return 5. no, return 0
        // And can easily use binary search to do this.
        public class Solution
        {
            public int PreimageSizeFZF(int k)
            {
                long start = (long)k * 4;
                long end = (long)k * 5; // this.GetZerosCount(first) > 5

                while (start <= end)
                {
                    long middle = (start + end) / 2;
                    long zeroCount = this.GetZerosCount(middle);
                    // Console.WriteLine($"start = {start}, middle = {middle}, end = {end}, count = {zeroCount}");
                    if (zeroCount < k)
                    {
                        start = middle + 1;
                    }
                    else if (zeroCount > k)
                    {
                        end = middle - 1;
                    }
                    else
                    {
                        return 5;
                    }
                }

                return 0;
            }

            private long GetZerosCount(long num)
            {
                long ans = 0;
                while (num > 0)
                {
                    num /= 5;
                    ans += num;
                }

                return ans;
            }
        }
    }
}
