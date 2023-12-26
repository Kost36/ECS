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
        public Quad Region;
        public int SplitCount;
        public int DepthLimit;

        public TreeNode<T> Root;

        public Stack<TreeNode<T>> PoolNodes = new Stack<TreeNode<T>>();
        public Dictionary<T, TreeItem<T>> AllItems = new Dictionary<T, TreeItem<T>>();

        public QuadTree(int splitCount, int depthLimit, Quad region)
        {
            Region = region;
            SplitCount = splitCount;
            DepthLimit = depthLimit;
            Root = new TreeNode<T>(this, null, 0, Region);
        }

        public TreeNode<T> CreateNode(TreeNode<T> parent, int depth, Quad quad)
        {
            var treeNode = PoolNodes.Count > 0
                ? PoolNodes.Pop()
                : new TreeNode<T>(this, parent, depth, quad);

            return treeNode;
        }

        public void AddOrUpdate(T value, Point point)
        {
            if (AllItems.TryGetValue(value, out TreeItem<T> item))
            {
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
    }

    public class TreeNode<T>
    {
        public QuadTree<T> Tree;

        public TreeNode<T> Parent;
        public TreeNode<T>[] Childs = new TreeNode<T>[4];
        public Quad[] ChildQuads;

        public Quad Quad;
        public int Depth;
        public bool IsSplited;

        public List<TreeItem<T>> Items = new List<TreeItem<T>>();

        public TreeNode(QuadTree<T> tree, TreeNode<T> parent, int depth, Quad quad)
        {
            Tree = tree;
            Parent = parent;
            Depth = depth;

            IsSplited = false;

            ChildQuads = new Quad[]
            {
                new Quad(quad.Min, quad.Center),
                new Quad(
                    min: new Point(quad.Center.X, quad.Min.Y),
                    max: new Point(quad.Max.X, quad.Center.Y)),
                new Quad(quad.Center, quad.Max),
                new Quad(
                    min: new Point(quad.Min.X, quad.Center.Y),
                    max: new Point(quad.Center.X, quad.Max.Y)),
            };
        }

        public void Add(TreeItem<T> item)
        {
            if (IsSplited)
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (ChildQuads[i].PointIsInside(item.Point))
                    {
                        if (Childs[i] == null)
                        {
                            Childs[i] = Tree.CreateNode(this, Depth + 1, ChildQuads[i]);
                        }

                        Childs[i].Add(item);
                        return;
                    }
                }

                Items.Add(item);
                item.Node = this;
                return;
            }

            Items.Add(item);
            item.Node = this;

            if (Items.Count >= Tree.SplitCount
                && Depth < Tree.DepthLimit)
            {
                IsSplited = true;
            }
        }

        public void Update(TreeItem<T> item)
        {
            if (Quad.PointIsInside(item.Point))
            {
                return;
            }

            Remove(item);
            Tree.Root.Add(item);
        }

        public bool Remove(TreeItem<T> item)
        {
            if (Items.Remove(item))
            {
                return true;
            }

            if (IsSplited)
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (Childs[i] != null)
                    {
                        if (Childs[i].Remove(item))
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
            if (!Quad.CircleIsIntersects(circle))
            {
                return;
            }

            FindCollisionsFromThis(circle, collisions);
            FindCollisionsFromСhild(circle, collisions);
        }

        private void FindCollisionsFromThis(Circle circle, List<T> collisions)
        {
            if (Items.Count > 0)
            {
                for (int i = 0; i < Items.Count; ++i)
                {
                    if (circle.PointIsInside(Items[i].Point))
                    {
                        collisions.Add(Items[i].Value);
                    }
                }
            }
        }

        private void FindCollisionsFromСhild(Circle circle, List<T> collisions)
        {
            if (IsSplited)
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (Childs[i] != null)
                    {
                        Childs[i].FindСollisionsForCircle(circle, collisions);
                    }
                }
            }
        }

    }

    public class TreeItem<T>
    {
        public TreeNode<T> Node;
        public T Value;
        public Point Point;

        public TreeItem(T value, Point point)
        {
            Value = value;
            Point = point;
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
    }

    public struct Circle
    {
        public Point Center;
        public long Radius;

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
    }

    public struct Quad
    {
        public Point Min;
        public Point Max;
        public Point Center;
        public long Width;
        public long Height;

        public Quad(Point min, Point max)
        {
            Min = min;
            Max = max;
            Width = Max.X - Min.X;
            Height = Max.Y - Min.Y;
            Center = new Point(
                x: (long)Math.Round((Min.X + Max.X) * 0.5f),
                y: (long)Math.Round((Min.Y + Max.Y) * 0.5f));
        }

        /// <summary>
        /// Окружность пересекается с квадратом
        /// </summary>
        /// <param name="circle">Окружность</param>
        public bool CircleIsIntersects(Circle circle)
        {
            var xDistance = Math.Abs(circle.Center.X - Center.X);
            var yDistance = Math.Abs(circle.Center.Y - Center.Y);

            var halfWidth = Width * 0.5;
            var halfHeight = Height * 0.5;

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
            return point.X >= Min.X && point.Y >= Min.Y
                && point.X <= Max.X && point.Y <= Max.Y;
        }
    }
}

//Заспличенные области должны рассплититься в родительсткую, если в них нету объектов
//При сплитинге объекты в родительской ноде должны распределиться по дочерним???