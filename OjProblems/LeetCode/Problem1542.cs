namespace OjProblems.LeetCode
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    internal class Problem1542
    {
        public class Solution
        {
            private static int[] exps = new[] { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512 };

            private int max = 999999999;

            // 88ms
            // Reduce the inner for loop, since when we first goes to the loop, we could find the leftmost index
            // Just save it for later use
            public int LongestAwesome(string s)
            {
                int[] leftIndexOfPalindrome = new int[1024];
                int[] firstOccurence = new int[1024];
                for (int i = 1; i < 1024; ++i)
                {
                    leftIndexOfPalindrome[i] = this.max;
                    firstOccurence[i] = this.max;
                }

                foreach (int exp in exps)
                {
                    leftIndexOfPalindrome[exp] = -1;
                }

                firstOccurence[0] = -1;
                leftIndexOfPalindrome[0] = -1;

                int ans = 0;
                int status = 0;
                for (int i = 0; i < s.Length; ++i)
                {
                    char c = s[i];
                    int digit = (int)(c - '0');
                    status ^= exps[digit];

                    if (firstOccurence[status] == this.max)
                    {
                        firstOccurence[status] = i;
                        leftIndexOfPalindrome[status] = i;

                        int tmp = status;
                        for (int j = 0; j < 10; ++j)
                        {
                            tmp ^= exps[j];
                            leftIndexOfPalindrome[status] = Math.Min(leftIndexOfPalindrome[status], firstOccurence[tmp]);
                            tmp ^= exps[j];
                        }
                    }

                    ans = Math.Max(ans, i - leftIndexOfPalindrome[status]);
                }

                return ans;
            }

            /// 244 ms
            /// No need to invoke ToString() method, use bit operation
            public int LongestAwesome1(string s)
            {
                Dictionary<int, int> dict = new Dictionary<int, int>();
                int status = 0;
                dict[0] = -1;

                int ans = 0;

                for (int i = 0; i < s.Length; ++i)
                {
                    char c = s[i];
                    int digit = (int)(c - '0');
                    status ^= exps[digit];

                    if (dict.ContainsKey(status))
                    {
                        ans = Math.Max(i - dict[status], ans);
                    }
                    else
                    {
                        dict[status] = i;
                    }

                    int tmp = status;
                    for (int j = 0; j < 10; ++j)
                    {
                        tmp ^= exps[j];
                        if (dict.ContainsKey(tmp))
                        {
                            ans = Math.Max(i - dict[tmp], ans);
                        }

                        tmp ^= exps[j];
                    }
                }

                return ans;
            }

            // 2000+ ms, the slowest version
            public int LongestAwesome3(string s)
            {
                Dictionary<string, int> dict = new Dictionary<string, int>();
                StringBuilder sb = new StringBuilder("0000000000");
                dict[sb.ToString()] = -1;

                int ans = 0;

                for (int i = 0; i < s.Length; ++i)
                {
                    char c = s[i];
                    int digit = (int)(c - '0');
                    sb[digit] = this.FlipChar(sb[digit]);

                    if (dict.ContainsKey(sb.ToString()))
                    {
                        ans = Math.Max(i - dict[sb.ToString()], ans);
                    }
                    else
                    {
                        dict[sb.ToString()] = i;
                    }

                    StringBuilder t = new StringBuilder(sb.ToString());
                    for (int j = 0; j < 10; ++j)
                    {
                        t[j] = this.FlipChar(t[j]);
                        if (dict.ContainsKey(t.ToString()))
                        {
                            ans = Math.Max(i - dict[t.ToString()], ans);
                        }

                        t[j] = this.FlipChar(t[j]);
                    }
                }

                return ans;
            }

            private char FlipChar(char c)
            {
                return c == '0' ? '1' : '0';
            }
        }
    }
}
