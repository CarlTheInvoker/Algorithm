namespace OjProblems.LeetCode
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Problem0046
    {
        public class Solution
        {
            private int numsCount;
            public IList<IList<int>> Permute(int[] nums)
            {
                this.numsCount = nums.Length;
                int count = nums.Length;
                int size = 1;
                for (int i = 2; i <= count; ++i)
                {
                    size *= i;
                }

                List<IList<int>> ans = new List<IList<int>>(size);
                foreach (List<int> list in this.Solve(new HashSet<int>(nums)))
                {
                    ans.Add(list);
                }

                return ans;
            }

            public IEnumerable<List<int>> Solve(HashSet<int> nums)
            {
                if (nums.Count == 0)
                {
                    yield return new List<int>(this.numsCount);
                }
                else
                {
                    foreach (var num in nums.ToArray())
                    {
                        nums.Remove(num);
                        foreach (var list in this.Solve(nums))
                        {
                            list.Add(num);
                            yield return list;
                        }

                        nums.Add(num);
                    }
                }
            }
        }
    }
}
