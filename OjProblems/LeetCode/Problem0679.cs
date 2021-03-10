namespace OjProblems.LeetCode
{
    using System;
    using System.Linq;

    internal class Problem0679
    {
        public class Solution
        {
            private readonly double eps = 0.00001;

            public bool JudgePoint24(int[] nums)
            {
                if (this.JudgePoint24_4(nums.Select(num=>(double)num).ToArray()))
                {
                    return true;
                }

                return false;
            }

            public bool JudgePoint24_4(double[] nums)
            {
                double sum = nums.Sum();

                for (int i = 0; i < 4; ++i)
                {
                    for (int j = 0; j < 4; ++j)
                    {
                        for (int k = 0; k < 4; ++k)
                        {
                            if (i == j || j == k || i == k)
                            {
                                continue;
                            }

                            double last = sum - nums[i] - nums[j] - nums[k];
                            foreach (double num in this.Generate(nums[k], last))
                            {
                                if (this.JudgePoint24_3(new double[] { nums[i], nums[j], num }))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

                return false;
            }

            private bool JudgePoint24_3(double[] nums)
            {
                double sum = nums.Sum();

                for (int i = 0; i < 3; ++i)
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        double last = sum - nums[i] - nums[j];
                        foreach (double num in this.Generate(nums[j], last))
                        {
                            if (this.JudgePoint24_2(nums[i], num))
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }

            private bool JudgePoint24_2(double a, double b)
            {
                return this.Generate(a, b).Any(num => this.ConsiderEqual(num, 24));
            }

            private bool ConsiderEqual(double a, double b)
            {
                return Math.Abs(a - b) < this.eps;
            }

            private double[] Generate(double a, double b)
            {
                return new double[] { a + b, a - b, b - a, a * b, b != 0 ? a / b : 0, a != 0 ? b / a : 0 };
            }
        }
    }
}
