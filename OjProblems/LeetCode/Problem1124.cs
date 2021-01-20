namespace OjProblems.LeetCode
{
    using System;
    using System.Collections.Generic;

    public class Problem1124
    {
        public class Solution
        {
            public int LongestWPI(int[] hours)
            {
                Stack<KeyValuePair<int, int>> firstMin = new Stack<KeyValuePair<int, int>>();
                Stack<KeyValuePair<int, int>> lastMax = new Stack<KeyValuePair<int, int>>();
                
                firstMin.Push(new KeyValuePair<int, int>(0, -1));
                lastMax.Push(new KeyValuePair<int, int>(0, -1));

                int tiringDaysCount = 0;
                for (int i = 0; i < hours.Length; ++i)
                {
                    if (hours[i] > 8)
                    {
                        tiringDaysCount++;
                    }
                    else
                    {
                        tiringDaysCount--;
                    }

                    if (tiringDaysCount < firstMin.Peek().Key)
                    {
                        firstMin.Push(new KeyValuePair<int, int>(tiringDaysCount, i));
                    }

                    while (lastMax.Count > 0)
                    {
                        KeyValuePair<int, int> top = lastMax.Peek();
                        if (top.Key <= tiringDaysCount)
                        {
                            lastMax.Pop();
                        }
                        else
                        {
                            break;
                        }
                    }

                    lastMax.Push(new KeyValuePair<int, int>(tiringDaysCount, i));
                }

                int ans = 0;
                while (firstMin.Count > 0 && lastMax.Count > 0)
                {
                    var minimum = firstMin.Peek();
                    var maximum = lastMax.Peek();

                    if (maximum.Key <= minimum.Key)
                    {
                        lastMax.Pop();
                    }
                    else
                    {
                        ans = Math.Max(ans, maximum.Value - minimum.Value);
                        firstMin.Pop();
                    }
                }

                return ans;
            }
        }
    }
}
