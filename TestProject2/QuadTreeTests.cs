using GameLib.Algorithms.QuadTree;
using GameLib.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GameLibTests
{
    [TestClass()]
    public class QuadTreeTests
    {
        private List<Position> _positions = new()
        {
            new Position() { X = 0, Y = 0, Z = 0 },
            new Position() { X = 0, Y = 0, Z = 0 },
            new Position() { X = 1, Y = 1, Z = 0 },
            new Position() { X = 2, Y = 2, Z = 0 },
            new Position() { X = 3, Y = 3, Z = 0 },
            new Position() { X = 4, Y = 4, Z = 0 },
            new Position() { X = 5, Y = 5, Z = 0 },
            new Position() { X = 6, Y = 6, Z = 0 },
            new Position() { X = 7, Y = 7, Z = 0 },
            new Position() { X = 8, Y = 8, Z = 0 },
            new Position() { X = 9, Y = 9, Z = 0 },
            new Position() { X = 10, Y = 10, Z = 0 },
            new Position() { X = 20, Y = 20, Z = 0 },
            new Position() { X = 30, Y = 30, Z = 0 },
            new Position() { X = 40, Y = 40, Z = 0 },
            new Position() { X = 50, Y = 50, Z = 0 },
            new Position() { X = 60, Y = 60, Z = 0 },
            new Position() { X = 70, Y = 70, Z = 0 },
            new Position() { X = 80, Y = 80, Z = 0 },
            new Position() { X = 90, Y = 90, Z = 0 },
            new Position() { X = 100, Y = 100, Z = 0 },
            new Position() { X = 1000, Y = 1000, Z = 0 },
            new Position() { X = 2000, Y = 2000, Z = 0 },
            new Position() { X = 3000, Y = 3000, Z = 0 },
            new Position() { X = 4000, Y = 4000, Z = 0 },
            new Position() { X = 5000, Y = 5000, Z = 0 },
            new Position() { X = 6000, Y = 6000, Z = 0 },
            new Position() { X = 7000, Y = 7000, Z = 0 },
            new Position() { X = 8000, Y = 8000, Z = 0 },
            new Position() { X = 9000, Y = 9000, Z = 0 },
            new Position() { X = 10000, Y = 10000, Z = 0 },
        };
        private int _splitCount = 10;
        private int _depth = 10;
        private Quad _region = new Quad(
            new Point(0,0), 
            new Point(10000, 10000));

        [TestMethod()]
        public void QuadTreeFillingTest()
        {
            var quadTree = new QuadTree<Position>(_splitCount, _depth, _region);

            foreach (var pos in _positions)
            {
                quadTree.AddOrUpdate(pos, new Point((long)pos.X, (long)pos.Y));
            }

            Assert.AreEqual(_positions.Count, quadTree.AllItems.Count);
            Assert.AreEqual(_positions.Count, quadTree.GetAllTreeItems().Count);

            var expectedNodeCount = 5;
            Assert.AreEqual(expectedNodeCount, quadTree.GetAllTreeNodes().Count);
        }

        [TestMethod()]
        public void QuadTreeUpdateTest()
        {
            var quadTree = new QuadTree<Position>(_splitCount, _depth, _region);
            var position = _positions.First();

            foreach (var pos in _positions)
            {
                quadTree.AddOrUpdate(pos, new Point((long)pos.X, (long)pos.Y));
            }

            position.X = 999;
            position.Y = 999;
            position.Z = 999;
            quadTree.AddOrUpdate(position, new Point((long)position.X, (long)position.Y));

            Assert.AreEqual(_positions.Count, quadTree.AllItems.Count);
            Assert.AreEqual(_positions.Count, quadTree.GetAllTreeItems().Count);

            var expectedNodeCount = 5;
            Assert.AreEqual(expectedNodeCount, quadTree.GetAllTreeNodes().Count);
        }

        [TestMethod()]
        public void QuadTreeUpdateWithMovingToOtherQuadTest()
        {
            var quadTree = new QuadTree<Position>(_splitCount, _depth, _region);
            var position = _positions.Where(p => p.X == 10000).First();

            foreach (var pos in _positions)
            {
                quadTree.AddOrUpdate(pos, new Point((long)pos.X, (long)pos.Y));
            }

            position.X = 0;
            position.Y = 0;
            position.Z = 0;
            quadTree.AddOrUpdate(position, new Point((long)position.X, (long)position.Y));

            Assert.AreEqual(_positions.Count, quadTree.AllItems.Count);
            Assert.AreEqual(_positions.Count, quadTree.GetAllTreeItems().Count);

            var expectedNodeCount = 5;
            Assert.AreEqual(expectedNodeCount, quadTree.GetAllTreeNodes().Count);
        }

        [TestMethod()]
        public void QuadTreeRemoveTest()
        {
            var quadTree = new QuadTree<Position>(_splitCount, _depth, _region);
            var position = _positions.First();

            foreach (var pos in _positions)
            {
                quadTree.AddOrUpdate(pos, new Point((long)pos.X, (long)pos.Y));
            }

            quadTree.Remove(position);

            var expectedItemsCount = _positions.Count - 1;
            Assert.AreEqual(expectedItemsCount, quadTree.AllItems.Count);
            Assert.AreEqual(expectedItemsCount, quadTree.GetAllTreeItems().Count);

            var expectedNodeCount = 5;
            Assert.AreEqual(expectedNodeCount, quadTree.GetAllTreeNodes().Count);
        }

        [TestMethod()]
        public void QuadTreeRemoveWithRemoveTreeNodeTest()
        {
            var quadTree = new QuadTree<Position>(_splitCount, _depth, _region);

            foreach (var pos in _positions)
            {
                quadTree.AddOrUpdate(pos, new Point((long)pos.X, (long)pos.Y));
            }

            var expectedItemsCount = _positions.Count;
            Assert.AreEqual(expectedItemsCount, quadTree.AllItems.Count);
            Assert.AreEqual(expectedItemsCount, quadTree.GetAllTreeItems().Count);

            var expectedNodeCount = 5;
            Assert.AreEqual(expectedNodeCount, quadTree.GetAllTreeNodes().Count);

            foreach (var pos in _positions)
            {
                quadTree.Remove(pos);
            }

            expectedItemsCount = 0;
            Assert.AreEqual(expectedItemsCount, quadTree.AllItems.Count);
            Assert.AreEqual(expectedItemsCount, quadTree.GetAllTreeItems().Count);

            expectedNodeCount = 1; //Root
            Assert.AreEqual(expectedNodeCount, quadTree.GetAllTreeNodes().Count);
        }

        [TestMethod()]
        public void QuadTreeUpdateWithRemoveTreeNodeTest()
        {
            var quadTree = new QuadTree<Position>(_splitCount, _depth, _region);

            var positions = new List<Position>();

            var expectedItemsCount = 30;

            for (int i = 0; i < expectedItemsCount; i++)
            {
                positions.Add(new Position() { X = 0, Y = 0, Z = 0 });
            }

            Assert.AreEqual(expectedItemsCount, positions.Count);

            foreach (var pos in _positions)
            {
                quadTree.AddOrUpdate(pos, new Point((long)pos.X, (long)pos.Y));
            }

            Assert.AreEqual(expectedItemsCount, quadTree.AllItems.Count);
            Assert.AreEqual(expectedItemsCount, quadTree.GetAllTreeItems().Count);

            //var expectedNodeCount = 5;
            //Assert.AreEqual(expectedNodeCount, quadTree.GetAllTreeNodes().Count);

            //foreach (var pos in _positions)
            //{
            //    quadTree.Remove(pos);
            //}

            //expectedItemsCount = 0;
            //Assert.AreEqual(expectedItemsCount, quadTree.AllItems.Count);
            //Assert.AreEqual(expectedItemsCount, quadTree.GetAllTreeItems().Count);

            //expectedNodeCount = 1; //Root
            //Assert.AreEqual(expectedNodeCount, quadTree.GetAllTreeNodes().Count);
        }
        //Поиск ->
    }
}
