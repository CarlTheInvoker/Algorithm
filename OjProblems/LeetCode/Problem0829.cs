namespace OjProblems.LeetCode
{
    internal class Problem0829
    {
        public class Solution
        {
            public int ConsecutiveNumbersSum(int sum)
            {
                long sumLong = sum;
                long tmpSum = 0;
                long num = 1;
                int ans = 0;

                while (tmpSum <= sumLong - num)
                {
                    tmpSum += num;
                    if ((sumLong - tmpSum) % num == 0)
                    {
                        ans++;
                    }

                    num++;
                }

                return ans;
            }
        }
    }
}
