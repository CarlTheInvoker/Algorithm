namespace OjProblems.LeetCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Problem1520
    {
        public class Solution
        {
            private readonly int[][] firstLastIndex = new int[26][];
            private string input;

            public IList<string> MaxNumOfSubstrings(string s)
            {
                this.Initialize(s);

                List<int[]> intervals = new List<int[]>();
                for (int i = 0; i < 26; ++i)
                {
                    // A valid interval's start index must be the first occurrence of a char
                    // Get the all valid intervals first
                    if (this.firstLastIndex[i][0] != this.input.Length)
                    {
                        if (this.TryGetIntervalEnd(this.firstLastIndex[i][0], out int intervalEnd))
                        {
                            intervals.Add(new int[] { this.firstLastIndex[i][0], intervalEnd });
                        }
                    }
                }

                // Sort the interval by the interval start index
                intervals = intervals.OrderBy(interval => interval[0]).ToList();

                // Find all intervals that don't containning anyother interval
                List<string> result = new List<string>();
                int index = 0;
                while (index < intervals.Count)
                {
                    index++;
                    if (index < intervals.Count && intervals[index - 1][1] > intervals[index][0])
                    {
                        continue;
                    }

                    result.Add(s.Substring(intervals[index - 1][0], intervals[index - 1][1] - intervals[index - 1][0] + 1));
                }

                return result;
            }

            /// Get the first and last occurrence of a char
            private void Initialize(string s)
            {
                this.input = s;
                for (int i = 0; i < 26; ++i)
                {
                    this.firstLastIndex[i] = new int[2];
                    this.firstLastIndex[i][0] = s.Length;
                    this.firstLastIndex[i][1] = 0;
                }

                for (int i = 0; i < s.Length; ++i)
                {
                    int index = s[i] - 'a';
                    if (this.firstLastIndex[index][0] == s.Length)
                    {
                        this.firstLastIndex[index][0] = i;
                    }

                    this.firstLastIndex[index][1] = i;
                }
            }

            private bool TryGetIntervalEnd(int intervalStart, out int intervalEnd)
            {
                int i = intervalStart;
                intervalEnd = this.firstLastIndex[this.input[i] - 'a'][1];
                while (i <= intervalEnd)
                {
                    if (intervalStart > this.firstLastIndex[this.input[i] - 'a'][0])
                    {
                        // Means the interval containing a char that first occurrence is before the interval start
                        // It is not a valid interval
                        return false;
                    }

                    intervalEnd = Math.Max(intervalEnd, this.firstLastIndex[this.input[i] - 'a'][1]);
                    i++;
                }

                return true;
            }
        }
    }
}
