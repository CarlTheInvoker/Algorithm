namespace OjProblems.LeetCode
{
    using System;
    using System.Collections.Generic;

    internal class Problem0996
    {
        public class Solution
        {
            private Dictionary<int, HashSet<int>> possibleNext = new Dictionary<int, HashSet<int>>();
            private Dictionary<int, int> numCount = new Dictionary<int, int>();
            private int count;
            private int arrayLength;

            public int NumSquarefulPerms(int[] nums)
            {
                this.arrayLength = nums.Length;
                this.possibleNext[-1] = new HashSet<int>();
                foreach (int num in nums)
                {
                    this.possibleNext[-1].Add(num);
                    this.possibleNext[num] = new HashSet<int>();
                    if (!this.numCount.ContainsKey(num))
                    {
                        this.numCount[num] = 0;
                    }

                    this.numCount[num]++;
                }

                for (int i = 0; i < nums.Length; ++i)
                {
                    for (int j = i + 1; j < nums.Length; ++j)
                    {
                        int tmp = (int)Math.Sqrt(nums[i] + nums[j]);
                        if (tmp * tmp == nums[i] + nums[j])
                        {
                            this.possibleNext[nums[i]].Add(nums[j]);
                            this.possibleNext[nums[j]].Add(nums[i]);
                        }
                    }
                }

                this.count = 0;
                this.Solve(0, -1);
                return this.count;
            }

            private void Solve(int step, int previous)
            {
                if (step == this.arrayLength)
                {
                    this.count++;
                    return;
                }

                foreach (int num in this.possibleNext[previous])
                {
                    if (this.numCount[num] > 0)
                    {
                        this.numCount[num]--;
                        this.Solve(step + 1, num);
                        this.numCount[num]++;
                    }
                }
            }
        }
    }
}
