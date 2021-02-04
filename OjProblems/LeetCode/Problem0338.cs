namespace OjProblems.LeetCode
{
    internal class Problem0338
    {
        /// <summary>
        /// Consider numbers in [0, 1, ..., 2^k - 1] and [2^k, 2^k + 1, ..., 2^(k + 1) - 1]
        /// The length of 2 array are same
        /// And the bit presentation are only different at first bit, which is 0 in first array and 1 in second array
        /// </summary>
        public class Solution
        {
            public int[] CountBits(int num)
            {
                int[] result = new int[num + 1];

                int count = 1;
                int indexDiff = 1;
                while (count <= num)
                {
                    for (int i = 0; i < indexDiff && count <= num; ++i)
                    {
                        result[count++] = result[i] + 1;
                    }

                    indexDiff *= 2;
                }

                return result;
            }
        }
    }
}
