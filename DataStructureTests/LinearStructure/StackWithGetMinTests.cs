using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructure.LinearStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructure.LinearStructure.Tests
{
    [TestClass()]
    public class StackWithGetMinTests
    {
        private StackWithGetMin _stack;

        [TestInitialize]
        public void Initialize()
        {
            this._stack = new StackWithGetMin();
        }

        [TestMethod()]
        [ExpectedException(typeof(StackOverflowException))]
        public void GetMinTest_StackOverflowException()
        {
            this._stack.GetMin();
        }
        
        [TestMethod()]
        [ExpectedException(typeof(StackOverflowException))]
        public void PopTest_StackOverflowException()
        {
            this._stack.Pop();
        }

        [TestMethod()]
        public void GetMinTest()
        {
            Random rand = new Random();
            Stack<int> stack = new Stack<int>();
            for (int _ = 0; _ < 100; ++_)
            {
                int num = rand.Next(0, 100);
                if (rand.Next(0, 100) % 2 == 0 || stack.Count < 5)
                {
                    this._stack.Push(num);
                    stack.Push(num);

                    Assert.AreEqual(stack.Min(), this._stack.GetMin());
                }
                else
                {
                    Assert.AreEqual(stack.Pop(), this._stack.Pop());
                    Assert.AreEqual(stack.Min(), this._stack.GetMin());
                }
            }

            while (stack.Count > 0)
            {
                Assert.AreEqual(stack.Min(), this._stack.GetMin());
                Assert.AreEqual(stack.Pop(), this._stack.Pop());
            }
        }
    }
}