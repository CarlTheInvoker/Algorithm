namespace OjProblems.LeetCode
{
    using System.Collections.Generic;

    internal class Problem0799
    {
        public class Solution
        {
            public double ChampagneTower(int poured, int queryRow, int queryIndex)
            {
                List<List<double>> status = new List<List<double>>();
                for (int i = 1; i <= 100; ++i)
                {
                    List<double> row = new List<double>();
                    for (int j = 0; j < i; ++j)
                    {
                        row.Add(0);
                    }

                    status.Add(row);
                }

                status[0][0] = poured;
                for (int i = 0; i < status.Count; i++)
                {
                    for (int j = 0; j < status[i].Count; j++)
                    {
                        if (status[i][j] >= 1)
                        {
                            if (i < status.Count - 1)
                            {
                                status[i + 1][j] += (status[i][j] - 1) / 2;
                                status[i + 1][j + 1] += (status[i][j] - 1) / 2;
                                status[i][j] = 1;
                            }
                        }

                        if (i == queryRow && j == queryIndex)
                        {
                            return status[i][j];
                        }
                    }
                }

                return -1;
            }
        }
    }
}
