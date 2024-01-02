using GameLib.Collections.QuadTree;
using GameLib.Components;
using GameLib.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameLibTests
{
    [TestClass()]
    public class QuadTreeTests
    {
        private readonly List<Position> _positions = new()
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
        private readonly int _splitCount = 10;
        private readonly int _depth = 10;
        private readonly Quad _region = new(
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

            var expectedNodeCount = 19;
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

            var expectedNodeCount = 19;
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

            var expectedNodeCount = 19;
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

            var expectedNodeCount = 19;
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

            var expectedNodeCount = 19;
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
            var quadTree = new QuadTree<Position>(splitCount: 5, depthLimit: 5, _region);

            var positions = new List<Position>();

            var expectedItemsCount = 30;

            for (int i = 0; i < expectedItemsCount; i++)
            {
                positions.Add(new Position() { X = i * i, Y = i * i, Z = i * i });
            }

            Assert.AreEqual(expectedItemsCount, positions.Count);

            foreach (var position in positions)
            {
                quadTree.AddOrUpdate(position, new Point((long)position.X, (long)position.Y));
            }

            Assert.AreEqual(expectedItemsCount, quadTree.AllItems.Count);
            Assert.AreEqual(expectedItemsCount, quadTree.GetAllTreeItems().Count);

            var expectedNodeCount = 8;
            Assert.AreEqual(expectedNodeCount, quadTree.GetAllTreeNodes().Count);

            foreach (var position in positions)
            {
                position.X = 0;
                position.Y = 0;
                position.Z = 0;
                quadTree.AddOrUpdate(position, new Point((long)position.X, (long)position.Y));
            }

            Assert.AreEqual(expectedItemsCount, quadTree.AllItems.Count);
            Assert.AreEqual(expectedItemsCount, quadTree.GetAllTreeItems().Count);

            var expectedNodeId = quadTree.AllItems.First().Value.Node.GetHashCode();
            foreach (var item in quadTree.AllItems)
            {
                Assert.AreEqual(expectedNodeId, item.Value.Node.GetHashCode());
            }

            expectedNodeCount = 6;
            Assert.AreEqual(expectedNodeCount, quadTree.GetAllTreeNodes().Count);
        }

        [TestMethod()]
        [DataRow(1, 1, 5)]
        [DataRow(2, 2, 13)]
        [DataRow(5, 5, 12)]
        [DataRow(10, 10, 5)]
        [DataRow(10, 100, 5)]
        [DataTestMethod]
        public void QuadTreeFindСollisionsForCircleTest(int splitCount, int depthLimit, int expectedNodeCount)
        {
            var quadTree = new QuadTree<Position>(
                splitCount: splitCount, 
                depthLimit: depthLimit, 
                region: new Quad(
                    new Point(0, 0),
                    new Point(100, 100)));

            var area = new Circle(new Point(50, 50), 10);
            var positionsInsideArea = new List<Position>() 
            {
                new Position() { X = 40, Y = 50, Z = 0 },
                new Position() { X = 50, Y = 50, Z = 0 },
                new Position() { X = 60, Y = 50, Z = 0 },
                new Position() { X = 50, Y = 40, Z = 0 },
                new Position() { X = 50, Y = 60, Z = 0 },
                new Position() { X = 45, Y = 45, Z = 0 },
                new Position() { X = 45, Y = 55, Z = 0 },
                new Position() { X = 55, Y = 45, Z = 0 },
                new Position() { X = 55, Y = 55, Z = 0 }
            }; 
            var positionsOutsideArea = new List<Position>()
            {
                new Position() { X = 0, Y = 0, Z = 0 },
                new Position() { X = 100, Y = 0, Z = 0 },
                new Position() { X = 0, Y = 100, Z = 0 },
                new Position() { X = 100, Y = 100, Z = 0 },
                new Position() { X = 40, Y = 40, Z = 0 },
                new Position() { X = 40, Y = 60, Z = 0 },
                new Position() { X = 60, Y = 40, Z = 0 },
                new Position() { X = 60, Y = 60, Z = 0 },
            };

            var positions = positionsInsideArea.Concat(positionsOutsideArea);
            var expectedItemsCount = positions.Count();
            foreach (var position in positions)
            {
                quadTree.AddOrUpdate(position, new Point((long)position.X, (long)position.Y));
            }

            Assert.AreEqual(expectedItemsCount, quadTree.AllItems.Count);
            Assert.AreEqual(expectedItemsCount, quadTree.GetAllTreeItems().Count);
            Assert.AreEqual(expectedNodeCount, quadTree.GetAllTreeNodes().Count);

            var result = quadTree.FindСollisionsForCircle(area, out var collisions);
            if (result)
            {
                Assert.IsTrue(collisions.All(item => positionsInsideArea.Contains(item)), "Not all collisions were found");
                Assert.IsTrue(collisions.All(item => !positionsOutsideArea.Contains(item)), "ncorrect collisions were found");
            }
            else
            {
                Assert.Fail("Collisions must be found");
            }
        }

        [TestMethod()]
        [DataRow(10, 1, 100)] //Bad timing almost greedy algorithm
        [DataRow(10, 5, 100)]
        [DataRow(10, 10, 100)]
        [DataRow(10, 100, 100)]
        [DataTestMethod]
        public void QuadTreeFindСollisionsForCircleOn100ItemsTest(int splitCount, int depthLimit, int itemCount)
        {
            QuadTreeFindСollisionsForCircleOnItemsTest(splitCount, depthLimit, itemCount);
        }

        [TestMethod()]
        [DataRow(10, 1, 1000)] //Bad timing almost greedy algorithm
        [DataRow(10, 5, 1000)]
        [DataRow(10, 10, 1000)]
        [DataRow(10, 100, 1000)]
        [DataTestMethod]
        public void QuadTreeFindСollisionsForCircleOn1000ItemsTest(int splitCount, int depthLimit, int itemCount)
        {
            QuadTreeFindСollisionsForCircleOnItemsTest(splitCount, depthLimit, itemCount);
        }

        [TestMethod()]
        [DataRow(10, 1, 10000)] //Bad timing almost greedy algorithm
        [DataRow(10, 5, 10000)]
        [DataRow(10, 10, 10000)]
        [DataRow(10, 100, 10000)]
        [DataTestMethod]
        public void QuadTreeFindСollisionsForCircleOn10000ItemsTest(int splitCount, int depthLimit, int itemCount)
        {
            QuadTreeFindСollisionsForCircleOnItemsTest(splitCount, depthLimit, itemCount);
        }

        [TestMethod()]
        [DataRow(10, 1, 100000)] //Bad timing almost greedy algorithm
        [DataRow(10, 5, 100000)]
        [DataRow(10, 10, 100000)]
        [DataRow(10, 100, 100000)]
        [DataTestMethod]
        public void QuadTreeFindСollisionsForCircleOn100000ItemsTest(int splitCount, int depthLimit, int itemCount)
        {
            QuadTreeFindСollisionsForCircleOnItemsTest(splitCount, depthLimit, itemCount);
        }

        [TestMethod()]
        [DataRow(10, 1, 1000000)] //Bad timing almost greedy algorithm
        [DataRow(10, 5, 1000000)]
        [DataRow(10, 10, 1000000)]
        [DataRow(10, 50, 1000000)]
        [DataRow(10, 100, 1000000)]
        [DataRow(20, 10, 1000000)]
        [DataRow(20, 20, 1000000)]
        [DataRow(20, 50, 1000000)]
        [DataRow(20, 100, 1000000)]
        [DataRow(50, 10, 1000000)]
        [DataRow(50, 20, 1000000)]
        [DataRow(50, 50, 1000000)]
        [DataRow(50, 100, 1000000)]
        [DataTestMethod]
        public void QuadTreeFindСollisionsForCircleOn1000000ItemsTest(int splitCount, int depthLimit, int itemCount)
        {
            QuadTreeFindСollisionsForCircleOnItemsTest(splitCount, depthLimit, itemCount);
        }


        private void QuadTreeFindСollisionsForCircleOnItemsTest(int splitCount, int depthLimit, int itemCount)
        {
            var maxPozition = 10000;
            var quadTree = new QuadTree<Position>(
                splitCount: splitCount,
                depthLimit: depthLimit,
                region: new Quad(
                    new Point(0, 0),
                    new Point(maxPozition, maxPozition)));

            var area = new Circle(new Point(5000, 5000), 300);
            var positions = new List<Position>();
            Random rnd = new Random();

            for (int i = 0; i < itemCount; i++)
            {
                positions.Add(new Position()
                {
                    X = rnd.Next(0, maxPozition),
                    Y = rnd.Next(0, maxPozition),
                    Z = 0
                });
            }

            foreach (var position in positions)
            {
                quadTree.AddOrUpdate(position, new Point((long)position.X, (long)position.Y));
            }

            Assert.AreEqual(itemCount, quadTree.AllItems.Count);
            Assert.AreEqual(itemCount, quadTree.GetAllTreeItems().Count);

            var watch = Stopwatch.StartNew();
            var result = quadTree.FindСollisionsForCircle(area, out var collisions);
            watch.Stop();

            Console.WriteLine($"FindСollisionsForCircle сompleted in [{watch.ElapsedTicks}] tics | [{watch.ElapsedMilliseconds}] ms. Return [{result}] with [{collisions.Count}] collisions.");
        }
    }
}
