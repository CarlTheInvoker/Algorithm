using System;

namespace OjProblems.LeetCode
{
    internal class Problem0926
    {
        public class Solution
        {
            public int MinFlipsMonoIncr(string str)
            {
                if (string.IsNullOrEmpty(str))
                {
                    return 0;
                }

                int[] zero = new int[str.Length];
                int[] one = new int[str.Length];
                zero[0] = str[0] == '0' ? 1 : 0;
                one[0] = 1 - zero[0];

                for (int i = 1; i < str.Length; ++i)
                {
                    if (str[i] == '0')
                    {
                        zero[i] = zero[i - 1] + 1;
                        one[i] = one[i - 1];
                    }
                    else
                    {
                        zero[i] = zero[i - 1];
                        one[i] = one[i - 1] + 1;
                    }
                }

                // flip the string to "11111"
                int ans = zero[str.Length - 1];

                for (int i = 0; i < str.Length; ++i)
                {
                    // flip the string to first (i + 1) digits are 0, rests are 1
                    ans = Math.Min(one[i] + zero[str.Length - 1] - zero[i], ans);
                }

                return ans;
            }
        }
    }
}
