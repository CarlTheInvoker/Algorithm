namespace OjProblems.LeetCode
{
    using System;
    using System.Collections.Generic;

    internal class Problem1591
    {
        public class Solution
        {
            public bool IsPrintable(int[][] targetGrid)
            {
                Dictionary<int, Rectangle> dict = new Dictionary<int, Rectangle>();
                for (int i = 0; i < targetGrid.Length; ++i)
                {
                    for (int j = 0; j < targetGrid[i].Length; ++j)
                    {
                        if (!dict.ContainsKey(targetGrid[i][j]))
                        {
                            dict[targetGrid[i][j]] = new Rectangle(i, j);
                        }
                        else
                        {
                            dict[targetGrid[i][j]].UpdateSize(i, j);
                        }
                    }
                }

                int rectangleKey = 0;
                while (rectangleKey != -1)
                {
                    rectangleKey = -1;
                    foreach (KeyValuePair<int, Rectangle> kv in dict)
                    {
                        if (kv.Value.CheckValue(targetGrid, kv.Key))
                        {
                            kv.Value.UpdateValue(targetGrid);
                            rectangleKey = kv.Key;
                            break;
                        }
                    }

                    if (rectangleKey != -1)
                    {
                        dict.Remove(rectangleKey);
                    }
                }

                return dict.Count == 0;
            }

            private class Rectangle
            {
                public int Top { get; set; }
                public int Bottom { get; set; }
                public int Left { get; set; }
                public int Right { get; set; }

                public Rectangle(int i, int j)
                {
                    this.Top = i;
                    this.Bottom = i;
                    this.Left = j;
                    this.Right = j;
                }

                public void UpdateSize(int i, int j)
                {
                    this.Top = Math.Min(this.Top, i);
                    this.Bottom = Math.Max(this.Bottom, i);
                    this.Left = Math.Min(this.Left, j);
                    this.Right = Math.Max(this.Right, j);
                }

                /// <summary>
                /// Check if all elements in the grid equals to value or -1
                /// </summary>
                public bool CheckValue(int[][] targetGrid, int value)
                {
                    for (int i = this.Top; i <= this.Bottom; ++i)
                    {
                        for (int j = this.Left; j <= this.Right; ++j)
                        {
                            if (targetGrid[i][j] != -1 && targetGrid[i][j] != value)
                            {
                                return false;
                            }
                        }
                    }

                    return true;
                }

                /// <summary>
                /// Update all value in the grid to -1
                /// </summary>
                public void UpdateValue(int[][] targetGrid)
                {
                    for (int i = this.Top; i <= this.Bottom; ++i)
                    {
                        for (int j = this.Left; j <= this.Right; ++j)
                        {
                            targetGrid[i][j] = -1;
                        }
                    }
                }
            }
        }
    }
}
