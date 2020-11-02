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

        [TestInitialize]
        public void Initialize()
        {
            this.tree = new TwoThreeTree<int, int>();
            this.tree.Upsert(0, 0);
            this.tree.Upsert(1, 1);
            this.tree.Upsert(8, 8);
            this.tree.Upsert(4, 4);
            this.tree.Upsert(7, 7);
            this.tree.Upsert(2, 2);
            this.tree.Upsert(6, 6);
            this.tree.Upsert(5, 5);
            this.tree.Upsert(3, 3);
            this.tree.Upsert(9, 9);
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

        // [TestMethod()]
        // public void DeleteTest()
        // {
        //     Assert.Fail();
        // }

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