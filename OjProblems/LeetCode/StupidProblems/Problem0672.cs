namespace OjProblems.LeetCode.StupidProblems
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class Problem0672
    {
        public class Solution
        {
            public int FlipLights(int lightCount, int operationCount)
            {
                int length = Math.Min(lightCount, 4);
                StringBuilder sb = new StringBuilder(length);
                for (int i = 0; i < length; ++i)
                {
                    sb.Append("1");
                }

                HashSet<string> hs = new HashSet<string>();
                for (int flipAll = 0; flipAll < 2; flipAll++)
                {
                    for (int flipOdd = 0; flipOdd < 2; flipOdd++)
                    {
                        for (int flipEven = 0; flipEven < 2; flipEven++)
                        {
                            int flip3K1 = operationCount - flipAll - flipOdd - flipEven;
                            if (flip3K1 < 0) continue;

                            this.Reset(sb);
                            if (flipAll == 1)
                            {
                                this.FlipAll(sb);
                            }

                            if (flipOdd == 1)
                            {
                                this.FlipOdd(sb);
                            }

                            if (flipEven == 1)
                            {
                                this.FlipEven(sb);
                            }

                            if (flip3K1 % 2 == 1)
                            {
                                this.Flip3KPlus1(sb);
                            }

                            hs.Add(sb.ToString());
                        }
                    }
                }

                return hs.Count;
            }

            private char Flip(char c)
            {
                if (c == '1')
                {
                    return '0';
                }

                return '1';
            }

            private void Reset(StringBuilder sb)
            {
                for (int i = 0; i < sb.Length; ++i)
                {
                    sb[i] = '1';
                }
            }

            private void FlipAll(StringBuilder sb)
            {
                for (int i = 0; i < sb.Length; ++i)
                {
                    sb[i] = this.Flip(sb[i]);
                }
            }

            private void FlipOdd(StringBuilder sb)
            {
                for (int i = 0; i < sb.Length; ++i)
                {
                    if ((i + 1) % 2 != 0)
                    {
                        sb[i] = this.Flip(sb[i]);
                    }
                }
            }

            private void FlipEven(StringBuilder sb)
            {
                for (int i = 0; i < sb.Length; ++i)
                {
                    if ((i + 1) % 2 == 0)
                    {
                        sb[i] = this.Flip(sb[i]);
                    }
                }
            }

            private void Flip3KPlus1(StringBuilder sb)
            {
                for (int i = 0; i < sb.Length; ++i)
                {
                    if ((i + 1) % 3 == 1)
                    {
                        sb[i] = this.Flip(sb[i]);
                    }
                }
            }
        }
    }
}
