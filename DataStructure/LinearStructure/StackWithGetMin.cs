namespace DataStructure.LinearStructure
{
    using System;
    using System.Collections.Generic;

    public class StackWithGetMin
    {
        private readonly Stack<int> _stack = new Stack<int>();
        private int _minValue = 0;
        public StackWithGetMin()
        {
        }

        public int GetMin()
        {
            if (this._stack.Count == 0)
            {
                throw new StackOverflowException("No value in an empty stack");
            }

            return this._minValue;
        }

        public void Push(int n)
        {
            if (this._stack.Count == 0)
            {
                this._stack.Push(n);
                this._minValue = n;
            }
            else if (n >= this._minValue)
            {
                this._stack.Push(n);
            }
            else
            {
                this._stack.Push(2 * n - _minValue); // the value pushed is smaller than minValue
                this._minValue = n;
            }
        }

        public int Pop()
        {
            if (this._stack.Count == 0)
            {
                throw new StackOverflowException("Can't pop from an empty stack");
            }

            if (this._stack.Peek() >= this._minValue)
            {
                return this._stack.Pop();
            }

            // In this case, 2 * minValue - previousMinValue = this.stack.Peek()
            int currentMin = this._minValue;
            this._minValue = 2 * this._minValue - this._stack.Pop();
            return currentMin;
        }
    }
}
