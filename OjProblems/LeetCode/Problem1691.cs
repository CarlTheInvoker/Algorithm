namespace OjProblems.LeetCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Problem1691
    {
        public class Solution
        {
            public int MaxHeight(int[][] cuboids)
            {
                List<Cube> cubes = new List<Cube>(cuboids.Length * 6);
                for (int i = 0; i < cuboids.Length; ++i)
                {
                    cubes.AddRange(Cube.CreateCubes(cuboids[i], i));
                }

                // Remove the duplicate and sort, make sure that
                // if cubes[i] is larger than cubes[j] only when i > j
                cubes = cubes.Distinct().OrderBy(x => x.Length).ThenBy(x => x.Width).ThenBy(x => x.Height).ToList();

                // results[i] is the max length to put i-th cube on the bottom
                // Don't need consider that we reuse the same cube
                // If so, then must have cubes[i] < cubes[j] < cubes[k] and i < j < k and cubes[i].Index == cubes[k].index
                // Then cubes[i] and cubes[k] must the same
                // But we already remove the duplicates, so this can't happen
                List<int> results = new List<int>(cubes.Capacity);
                for (int i = 0; i < cubes.Count; ++i)
                {
                    int ans = cubes[i].Length;
                    // Console.WriteLine($"i = {i}, the cube is {cubes[i]}");
                    for (int j = i - 1; j >= 0; --j)
                    {
                        if (cubes[i].IsLargerThan(cubes[j]))
                        {
                            ans = Math.Max(ans, cubes[i].Length + results[j]);
                            // Console.WriteLine($"larger than {j}-th cube {cubes[j]}, new ans is {ans}");
                        }
                    }

                    results.Add(ans);
                    // Console.WriteLine($"i = {i}, the max length is {ans}");
                    // Console.WriteLine();
                }

                return results.Max();
            }

            public struct Cube
            {
                public int Length;
                public int Width;
                public int Height;
                public int Index;
                public Cube(int length, int width, int height, int index)
                {
                    this.Length = length;
                    this.Width = width;
                    this.Height = height;
                    this.Index = index;
                }

                public static IEnumerable<Cube> CreateCubes(int[] size, int index)
                {
                    yield return new Cube(size[0], size[1], size[2], index);
                    yield return new Cube(size[0], size[2], size[1], index);
                    yield return new Cube(size[1], size[0], size[2], index);
                    yield return new Cube(size[1], size[2], size[0], index);
                    yield return new Cube(size[2], size[0], size[1], index);
                    yield return new Cube(size[2], size[1], size[0], index);
                }

                public bool IsLargerThan(Cube b)
                {
                    return b.Length <= this.Length && b.Width <= this.Width && b.Height <= this.Height;
                }

                public override string ToString()
                {
                    return $"({this.Length}, {this.Width}, {this.Height}, {this.Index})";
                }
            }
        }
    }
}
