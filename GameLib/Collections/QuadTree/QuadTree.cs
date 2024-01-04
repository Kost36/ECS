using GameLib.Structures;
using System;
using System.Collections.Generic;

namespace GameLib.Collections.QuadTree
{
    /// <summary>
    /// Структура для:
    /// 1) Детекта сущьностей, которые увидел сканер
    /// 2) Определения возможности атаки оружием
    /// </summary>
    public class QuadTree<T>
    {
        private readonly Quad _region;

        public int SplitCount;
        public int DepthLimit;
        public TreeNode<T> Root;
        public Dictionary<T, TreeItem<T>> AllItems = new Dictionary<T, TreeItem<T>>();

        public QuadTree(int splitCount, int depthLimit, Quad region)
        {
            _region = region;
            SplitCount = splitCount;
            DepthLimit = depthLimit;
            Root = new TreeNode<T>(this, null, 0, _region);
        }

        public void AddOrUpdate(T value, Point2d point)
        {
            if (!_region.PointIsInside(point))
            {
                throw new ArgumentException($"Object [{point}] is located outside the controlled region [{_region}]");
            }

            if (AllItems.TryGetValue(value, out TreeItem<T> item))
            {
                item.Point.X = point.X;
                item.Point.Y = point.Y;

                item.Node.Update(item);
                return;
            }

            var newItem = new TreeItem<T>(value, point);
            AllItems.Add(value, newItem);
            Root.Add(newItem);
        }

        public void Remove(T value)
        {
            if (AllItems.TryGetValue(value, out TreeItem<T> item))
            {
                AllItems.Remove(value);
                Root.Remove(item);
            }
        }

        public bool FindСollisionsForCircle(Circle circle, out List<T> collisions)
        {
            collisions = new List<T>();
            Root.FindСollisionsForCircle(circle, collisions);
            return collisions.Count > 0;
        }

        public List<TreeNode<T>> GetAllTreeNodes()
        {
            var treeNodes = new List<TreeNode<T>>();
            Root.GetTreeNodes(treeNodes);
            return treeNodes;
        }

        public List<TreeItem<T>> GetAllTreeItems()
        {
            var treeItems = new List<TreeItem<T>>();
            Root.GetTreeItems(treeItems);
            return treeItems;
        }
    }
}