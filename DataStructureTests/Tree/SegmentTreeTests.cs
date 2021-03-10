using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructure.Tree.Tests
{
    [TestClass()]
    public class SegmentTreeTests
    {
        [TestMethod()]
        public void SegmentTreeTest()
        {
            SegmentTree st = new SegmentTree(0, 16);
            Assert.IsNull(st.Root);
        }

        [TestMethod()]
        public void AddTest()
        {
            SegmentTree st = new SegmentTree(0, 16);
            st.Add(6, 12);
            Assert.IsNotNull(st.Root.LeftChild);
            Assert.IsNotNull(st.Root.RightChild);
            
            Assert.IsNotNull(st.Root.LeftChild.RightChild);
            Assert.IsNotNull(st.Root.RightChild.LeftChild);
            Assert.IsNotNull(st.Root.LeftChild.RightChild.RightChild);

            Assert.IsNull(st.Root.LeftChild.LeftChild);
            Assert.IsNull(st.Root.RightChild.RightChild);
            Assert.IsNull(st.Root.LeftChild.RightChild.LeftChild);

            Assert.IsFalse(st.Root.Exist);
            Assert.IsFalse(st.Root.LeftChild.Exist);
            Assert.IsFalse(st.Root.RightChild.Exist);
            Assert.IsFalse(st.Root.LeftChild.RightChild.Exist);
            Assert.IsTrue(st.Root.RightChild.LeftChild.Exist);
            Assert.IsTrue(st.Root.LeftChild.RightChild.RightChild.Exist);

            // Add (0, 2)
            st.Add(0, 16);
            Assert.IsTrue(st.Root.Exist);
            Assert.IsNull(st.Root.LeftChild);
            Assert.IsNull(st.Root.RightChild);
        }

        [TestMethod()]
        public void AddTest_MergeNode()
        {
            SegmentTree st = new SegmentTree(0, 16);
            st.Add(0, 8);
            st.Add(8, 16);
            Assert.IsNull(st.Root.LeftChild);
            Assert.IsNull(st.Root.RightChild);
            Assert.IsTrue(st.Root.Exist);
        }

        [TestMethod()]
        public void AddTest_RemoveChildren()
        {
            SegmentTree st = new SegmentTree(0, 16);
            st.Add(0, 8);
            Assert.IsNotNull(st.Root.LeftChild);
            Assert.IsNull(st.Root.RightChild);
            Assert.IsFalse(st.Root.Exist);
            Assert.IsTrue(st.Root.LeftChild.Exist);

            st.Add(0, 16);
            Assert.IsNull(st.Root.LeftChild);
            Assert.IsNull(st.Root.RightChild);
            Assert.IsTrue(st.Root.Exist);
        }

        [TestMethod()]
        public void QueryTest()
        {
            SegmentTree st = new SegmentTree(0, 16);
            st.Add(0, 4);
            st.Add(4, 8);
            Assert.IsTrue(st.Query(0, 8));
        }

        [TestMethod()]
        public void RemoveTest_RemoveAll()
        {
            SegmentTree st = new SegmentTree(0, 16);
            st.Add(2, 4);
            st.Remove(2, 4);

            Assert.IsNull(st.Root);
        }

        [TestMethod()]
        public void RemoveTest_RemoveLeaf()
        {
            SegmentTree st = new SegmentTree(0, 16);
            st.Add(2, 8);
            st.Remove(2, 4);

            Assert.IsNull(st.Root.LeftChild.LeftChild);
            Assert.IsNotNull(st.Root.LeftChild.RightChild);
            Assert.IsTrue(st.Root.LeftChild.RightChild.Exist);

            Assert.IsFalse(st.Root.Exist);
            Assert.IsFalse(st.Root.LeftChild.Exist);
        }

        [TestMethod()]
        public void RemoveTest_SplitExistNode()
        {
            SegmentTree st = new SegmentTree(0, 16);
            st.Add(0, 16);
            st.Remove(0, 8);
            
            Assert.IsFalse(st.Root.Exist);
            Assert.IsNull(st.Root.LeftChild);
            Assert.IsNotNull(st.Root.RightChild);
            Assert.IsTrue(st.Root.RightChild.Exist);
        }
    }
}