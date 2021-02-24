namespace OjProblems.LeetCode
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Problem1655
    {
        public class Solution
        {
            public bool CanDistribute(int[] nums, int[] quantity)
            {
                Dictionary<int, int> numCount = new Dictionary<int, int>();
                foreach (int num in nums)
                {
                    if (!numCount.ContainsKey(num))
                    {
                        numCount[num] = 0;
                    }

                    numCount[num]++;
                }

                List<int> customer = quantity.ToList();
                customer.Sort();
                customer.Reverse();

                List<int> current = numCount.Values.ToList();
                return this.BackTracing(current, customer, 0);
            }

            private bool BackTracing(List<int> current, List<int> customer, int index)
            {
                if (index == customer.Count)
                {
                    return true;
                }

                for (int i = 0; i < current.Count; ++i)
                {
                    if (current[i] >= customer[index])
                    {
                        current[i] -= customer[index];
                        if (this.BackTracing(current, customer, index + 1))
                        {
                            return true;
                        }

                        current[i] += customer[index];
                    }
                }

                return false;
            }
        }
    }
}
