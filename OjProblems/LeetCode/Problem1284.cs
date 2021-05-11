using System;
using System.Collections.Generic;
using System.Text;

namespace OjProblems.LeetCode
{
    class Problem1284
    {
        public class Solution
        {
            private int[][] _board = null;
            private int _boardHeight = 0;
            private int _boardWidth = 0;
            private int _operationCnt = 0;
            private int _result = 10;
            public int MinFlips(int[][] mat)
            {
                _board = mat;
                this._boardHeight = mat.Length;
                this._boardWidth = mat[0].Length;
                this._operationCnt = 0;
                this.BackTracing(0);

                return this._result == 10 ? -1 : this._result;
            }

            private void BackTracing(int index)
            {
                if (this.IsAllZero())
                {
                    this._result = this._operationCnt;
                    return;
                }

                if (index == this._boardWidth * this._boardHeight || this._result != 10)
                {
                    return;
                }

                // Don't flip this index
                this.BackTracing(index + 1);
                if (this._result != 10)
                {
                    return;
                }

                // Flip this index
                int i = index / this._boardWidth;
                int j = index % this._boardWidth;
                this.Flip(i, j);
                this._operationCnt++;
                this.BackTracing(index + 1);
                this._operationCnt--;
                this.Flip(i, j);
            }

            private void Flip(int i, int j)
            {
                this.FlipSingleBit(i, j);
                this.FlipSingleBit(i - 1, j);
                this.FlipSingleBit(i + 1, j);
                this.FlipSingleBit(i, j - 1);
                this.FlipSingleBit(i, j + 1);
            }

            private void FlipSingleBit(int i, int j)
            {
                if (i >= 0 && i < this._boardHeight && j >= 0 && j < this._boardWidth)
                {
                    this._board[i][j] = Math.Abs(1 - this._board[i][j]);
                }
            }

            private bool IsAllZero()
            {
                for (int i = 0; i < this._boardHeight; ++i)
                {
                    for (int j = 0; j < this._boardWidth; ++j)
                    {
                        if (this._board[i][j] != 0)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }
    }
}
