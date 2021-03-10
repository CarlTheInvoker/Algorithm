namespace OjProblems.LeetCode
{
    using System;

    internal class Problem0188
    {
        public class Solution
        {
            public int MaxProfit(int k, int[] prices)
            {
                // dp[i, 0] = max profit can make with i transaction and no stock for now
                // dp[i, 1] = max profit can make with i transaction and 1 stock for now
                // transaction here means buying stock
                int[,] dp = new int[k + 1, 2];
                if (prices.Length == 0)
                {
                    return 0;
                }

                for (int i = 0; i <= k; ++i)
                {
                    dp[i, 0] = 0;
                    dp[i, 1] = prices[prices.Length - 1];
                }

                int[,] tmp = new int[k, 2];
                for (int i = prices.Length - 1; i >= 0; --i)
                {
                    for (int j = k; j >= 0; --j)
                    {
                        if (k > 0)
                        {
                            // Either buying the stock at current price, or do nothing
                            tmp[k, 0] = Math.Max(dp[k, 0], dp[k - 1, 1] - prices[i]);
                        }
                        else
                        {
                            // No transaction available
                            tmp[k, 0] = dp[k, 0];
                        }

                        // Either selling the stock at current price, or do nothing
                        tmp[k, 1] = Math.Max(dp[k, 1], dp[k, 0] + prices[i]);
                    }

                    int[,] swap = dp;
                    dp = tmp;
                    tmp = swap;
                }

                return dp[k, 0];
            }
        }
    }
}
