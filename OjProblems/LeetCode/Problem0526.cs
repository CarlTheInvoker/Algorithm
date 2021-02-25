namespace OjProblems.LeetCode
{
    using System.Collections.Generic;
    
    internal class Problem0526
    {
        public class Solution
        {
            // currentPerm is like [0, a1, a2, a3, 0, ..., 0], ai must not be 0 and can't be same
            //     - First 0 is like a place holder
            //     - 
            private readonly List<bool> _used = new List<bool>();
            private int _permCount = 0;
            private int _maxNumber;

            public int CountArrangement(int n)
            {
                this._maxNumber = n;
                for (int i = 0; i <= n; ++i)
                {
                    this._used.Add(false);
                }

                this.BackTracing(1);
                return this._permCount;
            }

            private void BackTracing(int index)
            {
                if (index == this._maxNumber + 1)
                {
                    // No this.currentPerm is a valid perm
                    this._permCount++;
                    return;
                }

                for (int num = 1; num <= this._maxNumber; ++num)
                {
                    if (!this._used[num] && (index % num == 0 || num % index == 0))
                    {
                        this._used[num] = true;
                        this.BackTracing(index + 1);
                        this._used[num] = false;
                    }
                }
            }
        }
    }
}
