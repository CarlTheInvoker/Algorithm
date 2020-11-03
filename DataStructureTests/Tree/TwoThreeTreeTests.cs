using System;

namespace DataStructure.Tree.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DataStructure.Tree;
    using Algorithm.Tree;

    [TestClass()]
    public class TwoThreeTreeTests
    {
        private TwoThreeTree<int, int> tree;
        private List<int> treeKeys;
        [TestInitialize]
        public void Initialize()
        {
            this.tree = new TwoThreeTree<int, int>();
            this.tree.Upsert(4, 4);
            this.tree.Upsert(1, 1);
            this.tree.Upsert(7, 7);
            this.tree.Upsert(9, 9);
            this.tree.Upsert(0, 0);
            this.tree.Upsert(2, 2);
            this.tree.Upsert(3, 3);
            this.tree.Upsert(5, 5);
            this.tree.Upsert(6, 6);
            this.tree.Upsert(8, 8);
            this.tree.Upsert(10, 10);
            this.treeKeys = new List<int>() {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        }

        [TestMethod()]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void SearchNotFoundTest()
        {
            this.tree.Search(-1);
        }

        [TestMethod()]
        public void SearchAndUpdateTest()
        {
            for (int i = 0; i < 10; ++i)
            {
                Assert.AreEqual(i, this.tree.Search(i));
            }

            for (int i = 0; i < 10; ++i)
            {
                this.tree.Upsert(i, 88 - i);
                Assert.AreEqual(88 - i, this.tree.Search(i));
            }
        }

        [TestMethod()]
        public void InsertTest_Ascending()
        {
            List<int> elements = new List<int>();
            var t = new TwoThreeTree<int, int>();
            this.InsertNewNodeAndVerify(t, elements, 0);
            this.InsertNewNodeAndVerify(t, elements, 1);
            this.InsertNewNodeAndVerify(t, elements, 2);
            this.InsertNewNodeAndVerify(t, elements, 3);
            this.InsertNewNodeAndVerify(t, elements, 4);
            this.InsertNewNodeAndVerify(t, elements, 5);
            this.InsertNewNodeAndVerify(t, elements, 6);
            this.InsertNewNodeAndVerify(t, elements, 7);
            this.InsertNewNodeAndVerify(t, elements, 8);
            this.InsertNewNodeAndVerify(t, elements, 9);
        }

        [TestMethod()]
        public void InsertTest_Descending()
        {
            List<int> elements = new List<int>();
            var t = new TwoThreeTree<int, int>();
            this.InsertNewNodeAndVerify(t, elements, 9);
            this.InsertNewNodeAndVerify(t, elements, 8);
            this.InsertNewNodeAndVerify(t, elements, 7);
            this.InsertNewNodeAndVerify(t, elements, 6);
            this.InsertNewNodeAndVerify(t, elements, 5);
            this.InsertNewNodeAndVerify(t, elements, 4);
            this.InsertNewNodeAndVerify(t, elements, 3);
            this.InsertNewNodeAndVerify(t, elements, 2);
            this.InsertNewNodeAndVerify(t, elements, 1);
            this.InsertNewNodeAndVerify(t, elements, 0);
        }

        [TestMethod()]
        public void DeleteTest_R2L22_RemoveRoot()
        {
            List<int> elements = new List<int>();
            var t = new TwoThreeTree<int, int>();
            this.InsertNewNodeAndVerify(t, elements, 1);
            this.InsertNewNodeAndVerify(t, elements, 0);
            this.InsertNewNodeAndVerify(t, elements, 2);
            Assert.AreEqual(1, t.Delete(1));
            elements.Remove(1);
            this.Validate(t, elements);
        }

        [TestMethod()]
        public void DeleteTest_R2L22_RemoveLeaf()
        {
            List<int> elements = new List<int>();
            var t = new TwoThreeTree<int, int>();
            this.InsertNewNodeAndVerify(t, elements, 1);
            this.InsertNewNodeAndVerify(t, elements, 0);
            this.InsertNewNodeAndVerify(t, elements, 2);
            Assert.AreEqual(0, t.Delete(0));
            elements.Remove(0);
            this.Validate(t, elements);
        }

        [TestMethod()]
        public void DeleteTest_R2L22_RemoveLeaf2()
        {
            List<int> elements = new List<int>();
            var t = new TwoThreeTree<int, int>();
            this.InsertNewNodeAndVerify(t, elements, 1);
            this.InsertNewNodeAndVerify(t, elements, 0);
            this.InsertNewNodeAndVerify(t, elements, 2);
            Assert.AreEqual(2, t.Delete(2));
            elements.Remove(2);
            this.Validate(t, elements);
        }

        [TestMethod()]
        // p2 means parent is a 2-node, leaf is a 2-node
        public void DeleteTest_P2L2()
        {
            Assert.AreEqual(2, this.tree.Delete(2));
            this.treeKeys.RemoveAt(2);
            this.Validate(this.tree, this.treeKeys);
        }

        [TestMethod()]
        public void DeleteTest_P2L3()
        {
            Assert.AreEqual(0, this.tree.Delete(0));
            this.treeKeys.RemoveAt(0);
            this.Validate(this.tree, this.treeKeys);
        }

        [TestMethod()]
        public void DeleteTest_P3L2()
        {
            Assert.AreEqual(8, this.tree.Delete(8));
            this.treeKeys.RemoveAt(8);
            this.Validate(this.tree, this.treeKeys);
        }

        [TestMethod()]
        public void DeleteTest_P3L3()
        {
            Assert.AreEqual(5, this.tree.Delete(5));
            this.treeKeys.RemoveAt(5);
            this.Validate(this.tree, this.treeKeys);
        }

        [TestMethod()]
        public void DeleteTest_NL2Node()
        {
            Assert.AreEqual(1, this.tree.Delete(1));
            this.treeKeys.RemoveAt(1);
            this.Validate(this.tree, this.treeKeys);
        }

        [TestMethod()]
        public void DeleteTest_NL3Node()
        {
            Assert.AreEqual(7, this.tree.Delete(7));
            this.treeKeys.RemoveAt(7);
            this.Validate(this.tree, this.treeKeys);
        }

        [TestMethod()]
        public void DeleteTest_R2()
        {
            Assert.AreEqual(4, this.tree.Delete(4));
            this.treeKeys.RemoveAt(4);
            this.Validate(this.tree, this.treeKeys);
        }

        [TestMethod()]
        public void DeleteTest_R3()
        {
            List<int> elements = new List<int>();
            var t = new TwoThreeTree<int, int>();
            this.InsertNewNodeAndVerify(t, elements, 10);
            this.InsertNewNodeAndVerify(t, elements, 20);
            this.InsertNewNodeAndVerify(t, elements, 2);
            this.InsertNewNodeAndVerify(t, elements, 5);
            this.InsertNewNodeAndVerify(t, elements, 15);
            this.InsertNewNodeAndVerify(t, elements, 30);
            this.InsertNewNodeAndVerify(t, elements, 1);
            this.InsertNewNodeAndVerify(t, elements, 3);
            this.InsertNewNodeAndVerify(t, elements, 8);
            this.InsertNewNodeAndVerify(t, elements, 9);
            this.InsertNewNodeAndVerify(t, elements, 11);
            this.InsertNewNodeAndVerify(t, elements, 16);
            this.InsertNewNodeAndVerify(t, elements, 25);
            this.InsertNewNodeAndVerify(t, elements, 35);
            Assert.AreEqual(10, t.Delete(10));
            elements.Remove(10);
            this.Validate(t, elements);
        }

        [TestMethod()]
        public void DeleteTest_KeyNotFound()
        {
            List<int> elements = new List<int>();
            var t = new TwoThreeTree<int, int>();

            try
            {
                this.InsertNewNodeAndVerify(t, elements, 10);
                this.InsertNewNodeAndVerify(t, elements, 20);
                this.InsertNewNodeAndVerify(t, elements, 2);
                this.InsertNewNodeAndVerify(t, elements, 5);
                this.InsertNewNodeAndVerify(t, elements, 15);
                this.InsertNewNodeAndVerify(t, elements, 30);
                this.InsertNewNodeAndVerify(t, elements, 1);
                this.InsertNewNodeAndVerify(t, elements, 3);
                this.InsertNewNodeAndVerify(t, elements, 8);
                this.InsertNewNodeAndVerify(t, elements, 9);
                this.InsertNewNodeAndVerify(t, elements, 11);
                this.InsertNewNodeAndVerify(t, elements, 16);
                this.InsertNewNodeAndVerify(t, elements, 25);
                this.InsertNewNodeAndVerify(t, elements, 35);

                t.Delete(7);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is KeyNotFoundException);
            }
            finally
            {
                this.Validate(t, elements);
            }
        }

        [TestMethod()]
        public void DeleteTest_KeyNotFound2()
        {
            try
            {
                this.tree.Delete(-1);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is KeyNotFoundException);
            }
            finally
            {
                this.Validate(this.tree, this.treeKeys);
            }
        }


        private void InsertNewNodeAndVerify(TwoThreeTree<int, int> t, List<int> elements, int newElement)
        {
            t.Upsert(newElement, newElement);
            elements.Add(newElement);
            this.Validate(t, elements);
        }

        private void Validate(TwoThreeTree<int, int> t, List<int> elements)
        {
            List<int> keys = new List<int>();
            t.PreOrderTraverse((key, value) =>
            {
                keys.Add(key);
            });

            Assert.AreEqual(elements.Count, keys.Count);

            elements.Sort();
            for (int i = 0; i < keys.Count; ++i)
            {
                Assert.AreEqual(elements[i], keys[i]);
            }
        }
    }
}