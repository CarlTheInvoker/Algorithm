using Algorithm.Tree;

namespace DataStructure.Tree.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass()]
    public class BinarySearchTreeTests
    {
        private BinarySearchTree<int, int> bst;

        [TestInitialize]
        public void Initialize()
        {
            this.bst = new BinarySearchTree<int, int>();
        }

        [TestMethod()]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void SearchTest_FromEmptyTree()
        {
            this.bst.Search(1);
        }

        [TestMethod()]
        public void SearchTest()
        {
            this.UpsertSomeNodes();
            Assert.AreEqual(20, this.bst.Search(20));
            Assert.AreEqual(10, this.bst.Search(10));
            Assert.AreEqual(0, this.bst.Search(0));
            Assert.AreEqual(15, this.bst.Search(15));
            Assert.AreEqual(12, this.bst.Search(12));
            Assert.AreEqual(30, this.bst.Search(30));
            Assert.AreEqual(25, this.bst.Search(25));
            Assert.AreEqual(40, this.bst.Search(40));
            Assert.AreEqual(28, this.bst.Search(28));
        }

        [TestMethod()]
        public void UpsertTest()
        {
            this.UpsertSomeNodes();
            this.bst.Upsert(20, 200);
            Assert.AreEqual(200, this.bst.Search(20));

            this.bst.Upsert(12, 120);
            Assert.AreEqual(120, this.bst.Search(12));

            this.bst.Upsert(25, 250);
            Assert.AreEqual(250, this.bst.Search(25));

            this.ValidateBst();
        }

        [TestMethod()]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void RemoveTest_KeyNotFound()
        {
            this.UpsertSomeNodes();
            this.bst.Remove(7);
        }

        [TestMethod()]
        public void RemoveTest_Case1_1()
        {
            this.UpsertSomeNodes();
            this.bst.Upsert(11, 11);
            Assert.AreEqual(10, this.bst.Remove(10));
            this.ValidateBst();
        }

        [TestMethod()]
        public void RemoveTest_Case1_2()
        {
            this.UpsertSomeNodes();
            this.bst.Upsert(29, 29);
            Assert.AreEqual(25, this.bst.Remove(25));
            this.ValidateBst();
        }

        [TestMethod()]
        public void RemoveTest_Case2()
        {
            this.UpsertSomeNodes();
            this.bst.Upsert(11, 11);
            Assert.AreEqual(15, this.bst.Remove(15));
            this.ValidateBst();
        }

        [TestMethod()]
        public void RemoveTest_Case3_1()
        {
            this.bst.Upsert(11, 11);
            Assert.AreEqual(11, this.bst.Remove(11));
            this.ValidateBst();
        }

        [TestMethod()]
        public void RemoveTest_Case3_2()
        {
            this.UpsertSomeNodes();
            Assert.AreEqual(12, this.bst.Remove(12));
            this.ValidateBst();
        }

        [TestMethod()]
        public void RemoveTest_Case3_3()
        {
            this.UpsertSomeNodes();
            Assert.AreEqual(28, this.bst.Remove(28));
            this.ValidateBst();
        }

        private void UpsertSomeNodes()
        {
            this.bst.Upsert(20, 20);
            this.bst.Upsert(10, 10);
            this.bst.Upsert(0, 0);
            this.bst.Upsert(15, 15);
            this.bst.Upsert(12, 12);
            this.bst.Upsert(30, 30);
            this.bst.Upsert(25, 25);
            this.bst.Upsert(40, 40);
            this.bst.Upsert(28, 28);
            this.ValidateBst();
        }

        private void ValidateBst()
        {
            List<int> keys = new List<int>();
            this.bst.PreOrderTraverse((key, value) =>
            {
                keys.Add(key);
            });

            for (int i = 1; i < keys.Count; ++i)
            {
                Assert.IsTrue(keys[i - 1] < keys[i]);
            }
        }
    }
}