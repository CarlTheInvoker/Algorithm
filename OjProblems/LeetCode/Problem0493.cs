namespace OjProblems.LeetCode
{
    using System.Collections.Generic;

    internal class Problem0493
    {
        public class Solution
        {
            private int _reversePairCount = 0;
            public int ReversePairs(int[] nums)
            {
                List<long> originalNumbers = new List<long>();
                foreach (int num in nums)
                {
                    originalNumbers.Add(num);
                }

                this.MergeSort(originalNumbers, 0, originalNumbers.Count);

                return this._reversePairCount;
            }

            /// <summary>
            /// Merge sort the numbers 
            /// </summary>
            /// <param name="nums">Original numbers</param>
            /// <param name="start">Start index (including)</param>
            /// <param name="end">End index (excluding)</param>
            /// <returns>The sorted list</returns>
            private List<long> MergeSort(List<long> nums, int start, int end)
            {
                if (start == end)
                {
                    return new List<long>();
                }

                if (start == end - 1)
                {
                    return new List<long>() { nums[start] };
                }

                int middle = (start + end) / 2;
                List<long> leftNumbers = this.MergeSort(nums, start, middle);
                List<long> rightNumbers = this.MergeSort(nums, middle, end);

                // For a reverse pair, considering i, j, there are 2 possibilities:
                //     - i, j both in left or right (we can handle this case when merge sort the subarray)
                //     - i in left and j in right (we need to handle it here)
                this.CountReversePairs(leftNumbers, rightNumbers);

                // Do the merge
                int index = start;
                int leftIndex = 0, rightIndex = 0;
                List<long> sorted = new List<long>();
                while (leftIndex < leftNumbers.Count && rightIndex < rightNumbers.Count)
                {
                    if (leftNumbers[leftIndex] < rightNumbers[rightIndex])
                    {
                        sorted.Add(leftNumbers[leftIndex++]);
                    }
                    else
                    {
                        sorted.Add(rightNumbers[rightIndex++]);
                    }
                }

                while (leftIndex < leftNumbers.Count)
                {
                    sorted.Add(leftNumbers[leftIndex++]);
                }

                while (rightIndex < rightNumbers.Count)
                {
                    sorted.Add(rightNumbers[rightIndex++]);
                }

                return sorted;
            }

            private void CountReversePairs(List<long> leftNumbers, List<long> rightNumbers)
            {
                int left = 0, right = 0;
                while (left < leftNumbers.Count)
                {
                    while (right < rightNumbers.Count && leftNumbers[left] > 2 * rightNumbers[right])
                    {
                        right++;
                    }

                    left++;
                    this._reversePairCount += right;
                }
            }
        }
    }
}
