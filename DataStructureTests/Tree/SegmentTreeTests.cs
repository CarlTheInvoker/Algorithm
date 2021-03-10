using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructure.Tree.Tests
{
    [TestClass()]
    public class SegmentTreeTests
    {
        [TestMethod()]
        public void QueryTest()
        {
            SegmentTree st = new SegmentTree(1, 10);
            st.Add(3, 6);
            Assert.IsTrue(st.Query(3, 6));
            Assert.IsTrue(st.Query(4, 6));
            Assert.IsTrue(st.Query(4, 5));
            Assert.IsTrue(st.Query(5, 6));

            Assert.IsFalse(st.Query(1, 2));
            Assert.IsFalse(st.Query(2, 4));
            Assert.IsFalse(st.Query(2, 5));
            Assert.IsFalse(st.Query(2, 8));
            Assert.IsFalse(st.Query(5, 8));
            Assert.IsFalse(st.Query(7, 8));
        }

        [TestMethod()]
        public void AddTest()
        {
            SegmentTree st = new SegmentTree(1, 10);
            st.Add(2, 4);
            st.Add(7, 9);
            Assert.IsTrue(st.Query(2, 4));
            Assert.IsTrue(st.Query(7, 9));
            Assert.IsTrue(st.Query(3, 4));
            Assert.IsTrue(st.Query(7, 8));

            Assert.IsFalse(st.Query(5, 6));
            Assert.IsFalse(st.Query(4, 7));
            Assert.IsFalse(st.Query(3, 5));
            Assert.IsFalse(st.Query(5, 8));
        }

        [TestMethod()]
        public void RemoveTest()
        {
            SegmentTree st = new SegmentTree(1, 10);
            st.Add(2, 9);
            st.Remove(4, 7);
            Assert.IsTrue(st.Query(2, 4));
            Assert.IsTrue(st.Query(7, 9));
            Assert.IsTrue(st.Query(3, 4));
            Assert.IsTrue(st.Query(7, 8));

            Assert.IsFalse(st.Query(5, 6));
            Assert.IsFalse(st.Query(4, 7));
            Assert.IsFalse(st.Query(3, 5));
            Assert.IsFalse(st.Query(5, 8));
        }
    }
}