namespace Algorithm.Tests
{
    using Algorithm.Sort;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class MergeSortExtensionTest
    {
        [TestMethod()]
        public void SortTest()
        {
            int[] nums = new int[] { 1, 2, 3, 4, 4, 3, 2, 1, 1, 3, 2, 4, 2, 4, 3, 1, 0, 0 };
            nums.MergeSort();

            for (int i = 1; i < nums.Length; ++i)
            {
                Assert.IsTrue(nums[i - 1] <= nums[i]);
            }
        }
    }
}