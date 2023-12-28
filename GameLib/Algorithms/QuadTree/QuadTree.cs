using System;
using System.Collections.Generic;

namespace GameLib.Algorithms.QuadTree
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

        public void AddOrUpdate(T value, Point point)
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

    public class TreeNode<T>
    {
        private readonly Quad _quad;
        private readonly int _depth;
        private readonly TreeNode<T>[] _childs = new TreeNode<T>[4];
        private Quad[] _childQuads;
        private QuadTree<T> _tree;
        private TreeNode<T> _parent;
        private bool _isSplited;
        private List<TreeItem<T>> _items = new List<TreeItem<T>>();

        public TreeNode(QuadTree<T> tree, TreeNode<T> parent, int depth, Quad quad)
        {
            _tree = tree;
            _parent = parent;
            _depth = depth;
            _quad = quad;
            _isSplited = false;
            _childQuads = quad.GetChildQuads();
        }

        public void Add(TreeItem<T> item)
        {
            if (_isSplited)
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (_childQuads[i].PointIsInside(item.Point))
                    {
                        if (_childs[i] == null)
                        {
                            _childs[i] = new TreeNode<T>(_tree, this, _depth + 1, _childQuads[i]);
                        }

                        _childs[i].Add(item);
                        return;
                    }
                }

                _items.Add(item);
                item.Node = this;
                return;
            }

            if (_items.Count + 1 >= _tree.SplitCount
                && _depth < _tree.DepthLimit)
            {
                _isSplited = true;
                MovingNodeItemsDown(item);
                return;
            }

            _items.Add(item);
            item.Node = this;
        }

        public void Update(TreeItem<T> item)
        {
            if (_quad.PointIsInside(item.Point))
            {
                return;
            }

            _tree.Root.Add(item);
            Remove(item);
        }

        public bool Remove(TreeItem<T> item)
        {
            if (_items.Remove(item))
            {
                if (_items.Count == 0)
                {
                    Rebuild();
                }
                return true;
            }

            if (_isSplited)
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (_childs[i] != null)
                    {
                        if (_childs[i].Remove(item))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void FindСollisionsForCircle(Circle circle, List<T> collisions)
        {
            if (!_quad.CircleIsIntersects(circle))
            {
                return;
            }

            FindCollisionsFromThis(circle, collisions);
            FindCollisionsFromСhild(circle, collisions);
        }

        public void GetTreeNodes(List<TreeNode<T>> treeNodes)
        {
            treeNodes.Add(this);
            for (int i = 0; i < 4; ++i)
            {
                if (_childs[i] != null)
                {
                    _childs[i].GetTreeNodes(treeNodes);
                }
            }
        }

        public void GetTreeItems(List<TreeItem<T>> treeItems)
        {
            treeItems.AddRange(_items);
            for (int i = 0; i < 4; ++i)
            {
                if (_childs[i] != null)
                {
                    _childs[i].GetTreeItems(treeItems);
                }
            }
        }

        public override string ToString()
        {
            var insideItems = new List<TreeItem<T>>();
            GetTreeItems(insideItems);

            return $"Node:[{GetHashCode()}]; Quad:[{_quad}]; Depth:[{_depth}]; IsSplited:[{_isSplited}]; Items:[{_items.Count}]; InsideItems [{insideItems.Count}]";
        }

        private void FindCollisionsFromThis(Circle circle, List<T> collisions)
        {
            if (_items.Count > 0)
            {
                for (int i = 0; i < _items.Count; ++i)
                {
                    if (circle.PointIsInside(_items[i].Point))
                    {
                        collisions.Add(_items[i].Value);
                    }
                }
            }
        }

        private void FindCollisionsFromСhild(Circle circle, List<T> collisions)
        {
            if (_isSplited)
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (_childs[i] != null)
                    {
                        _childs[i].FindСollisionsForCircle(circle, collisions);
                    }
                }
            }
        }

        private void MovingNodeItemsDown(TreeItem<T> newItem)
        {
            Add(newItem);

            foreach (var item in _items)
            {
                Add(item);
            }

            _items = new List<TreeItem<T>>();
        }

        private void Rebuild()
        {
            RemoveСhilds();
            if (СhildsIsEmpty())
            {
                if (_items.Count == 0)
                {
                    _parent?.Rebuild();
                }
            }
        }

        private void RemoveСhilds()
        {
            for (int i = 0; i < 4; ++i)
            {
                if (_childs[i] != null)
                {
                    if (!_childs[i].HasInsideItems())
                    {
                        _childs[i].Destroy();
                        _childs[i] = null;
                    }
                }
            }
        }

        private bool СhildsIsEmpty()
        {
            for (int i = 0; i < 4; ++i)
            {
                if (_childs[i] != null)
                {
                    return false;
                }
            }

            return true;
        }

        private bool HasInsideItems()
        {
            if (_items.Count > 0)
            {
                return true;
            }

            for (int i = 0; i < 4; ++i)
            {
                if (_childs[i] != null)
                {
                    if (_childs[i].HasInsideItems())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void Destroy()
        {
            _tree = null;
            _parent = null;
            _childQuads = null;
        }
    }

    public class TreeItem<T>
    {
        public readonly T Value;
        public TreeNode<T> Node;
        public Point Point;

        public TreeItem(T value, Point point)
        {
            Value = value;
            Point = point;
        }

        public override string ToString()
        {
            return $"Node:[{Node.GetHashCode()}]; Point:[{Point}]; Value:[{Value}]";
        }
    }

    public struct Point
    {
        public long X;
        public long Y;

        public Point(long x, long y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"X:[{X}]; Y:[{Y}]";
        }
    }

    public struct Circle
    {
        public readonly Point Center;
        public readonly long Radius;

        public Circle(Point center, long radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// Точка находится внутри окружности
        /// </summary>
        /// <param name="x">Центр точки. X</param>
        /// <param name="y">Центр точки. Y</param>
        public bool PointIsInside(Point point)
        {
            return Math.Pow(point.X - Center.X, 2f) + Math.Pow(point.Y - Center.Y, 2f) <= Math.Pow(Radius, 2f);
        }

        public override string ToString()
        {
            return $"Center:[{Center}]; Radius:[{Radius}]";
        }
    }

    public struct Quad
    {
        private readonly Point _min;
        private readonly Point _max;
        private readonly Point _center;
        private readonly long _width;
        private readonly long _height;

        public Quad(Point min, Point max)
        {
            _min = min;
            _max = max;
            _width = _max.X - _min.X;
            _height = _max.Y - _min.Y;
            _center = new Point(
                x: (long)Math.Round((_min.X + _max.X) * 0.5f),
                y: (long)Math.Round((_min.Y + _max.Y) * 0.5f));
        }

        /// <summary>
        /// Окружность пересекается с квадратом
        /// </summary>
        /// <param name="circle">Окружность</param>
        public bool CircleIsIntersects(Circle circle)
        {
            var xDistance = Math.Abs(circle.Center.X - _center.X);
            var yDistance = Math.Abs(circle.Center.Y - _center.Y);

            var halfWidth = _width * 0.5;
            var halfHeight = _height * 0.5;

            if (xDistance > (halfWidth + circle.Radius)) { return false; }
            if (yDistance > (halfHeight + circle.Radius)) { return false; }

            if (xDistance <= halfWidth) { return true; }
            if (yDistance <= halfHeight) { return true; }

            var cornerDistance = Math.Pow(xDistance - halfWidth, 2f) + Math.Pow(yDistance - halfHeight, 2f);

            return cornerDistance <= Math.Pow(circle.Radius, 2);
        }

        /// <summary>
        /// Точка находится внутри квадрата
        /// </summary>
        /// <param name="x">Центр точки. X</param>
        /// <param name="y">Центр точки. Y</param>
        public bool PointIsInside(Point point)
        {
            return point.X >= _min.X && point.Y >= _min.Y
                && point.X <= _max.X && point.Y <= _max.Y;
        }

        /// <summary>
        /// Поличить внутренние квадраты
        /// </summary>
        public Quad[] GetChildQuads()
        {
            return new Quad[]
            {
                new Quad(_min, _center),
                new Quad(
                    min: new Point(_center.X, _min.Y),
                    max: new Point(_max.X, _center.Y)),
                new Quad(_center, _max),
                new Quad(
                    min: new Point(_min.X, _center.Y),
                    max: new Point(_center.X, _max.Y)),
            };
        }

        public override string ToString()
        {
            return $"Min:[{_min}]; Max:[{_max}]; Center:[{_center}]; Width:[{_width}]; Height:[{_height}]";
        }
    }
}