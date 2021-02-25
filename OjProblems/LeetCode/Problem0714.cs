namespace OjProblems.LeetCode
{
    using System;

    internal class Problem0714
    {
        public class Solution
        {
            public int MaxProfit(int[] prices, int fee)
            {
                int hasStockTomorrow = 0;
                int noStockTomorrow = 0;

                for (int i = prices.Length - 1; i >= 0; --i)
                {
                    int noStock = Math.Max(
                        noStockTomorrow, // Do nothing
                        hasStockTomorrow - prices[i]); // Buy stock

                    int hasStock = Math.Max(
                        hasStockTomorrow, // Do nothing
                        noStockTomorrow + prices[i] - fee); // Sale the stock

                    hasStockTomorrow = hasStock;
                    noStockTomorrow = noStock;
                }

                return noStockTomorrow;
            }

            private int MaxProfitWithArray(int[] prices, int fee)
            {
                // hasStock[i] means max profit we could make in rest of days if we have stock at the beginning of day i
                int[] hasStock = new int[prices.Length];
                int[] noStock = new int[prices.Length];

                hasStock[prices.Length - 1] = Math.Max(prices[prices.Length - 1] - fee, 0);
                noStock[prices.Length - 1] = 0;

                for (int i = prices.Length - 2; i >= 0; --i)
                {
                    noStock[i] = Math.Max(
                        noStock[i + 1], // Do nothing
                        hasStock[i + 1] - prices[i]);

                    hasStock[i] = Math.Max(
                        hasStock[i + 1],
                        noStock[i + 1] + prices[i] - fee);
                }

                return noStock[0];
            }
        }
    }
}
