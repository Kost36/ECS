using System.Collections.Generic;

namespace GameLib.WorkFlow
{
    public abstract class QuadTreeBase<T>
    {
        private Quad _region;
        private readonly int _splitCount;
        private readonly int _depthLimit;
        private readonly Stack<Node> _poolNodes = new Stack<Node>();
        private readonly Stack<Item> _poolItems = new Stack<Item>();

        protected Node _root;
        protected readonly Dictionary<T, Item> _allItems = new Dictionary<T, Item>();

        protected QuadTreeBase(int splitCount, int depthLimit, ref Quad region)
        {
            _region = region;
            _splitCount = splitCount;
            _depthLimit = depthLimit;
            _root = CreateNode(this, null, 0);
        }

        protected void Add(T value, ref Quad quad)
        {
            if (!_allItems.TryGetValue(value, out Item item))
            {
                item = CreateItem(value, ref quad);
                _allItems.Add(value, item);
            }
            _root.Add(item);
        }

        protected int GetNodesCount()
        {
            int count = 0;
            CountNodes(_root, ref count);
            return count;
        }

        private void CountNodes(Node node, ref int count)
        {
            ++count;
            if (node._isSplited)
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (node._nodes[i] != null)
                    {
                        CountNodes(node._nodes[i], ref count);
                    }
                }
            }
        }

        private Item CreateItem(T value, ref Quad quad)
        {
            var item = _poolItems.Count > 0 ? _poolItems.Pop() : new Item();

            item.Value = value;
            item.Quad = quad;

            return item;
        }

        protected Node CreateNode(QuadTreeBase<T> tree, Node parent, int depth)
        {
            return CreateNode(tree, parent, depth, ref _region);
        }

        protected Node CreateNode(QuadTreeBase<T> tree, Node parent, int depth, ref Quad quad)
        {
            var branch = tree._poolNodes.Count > 0
                ? tree._poolNodes.Pop()
                : new Node(tree, parent, depth, ref quad);

            return branch;
        }

        protected class Node
        {
            private List<Item> _items = new List<Item>();

            internal Node[] _nodes = new Node[4];

            private QuadTreeBase<T> _tree;
            internal Node _parent;
            internal Quad[] _quads;
            internal int _depth;
            internal bool _isSplited;

            internal Node(QuadTreeBase<T> tree, Node parent, int depth, ref Quad quad)
            {
                _tree = tree;
                _parent = parent;
                _isSplited = false;
                _depth = depth;

                float midX = quad.MinX + (quad.MaxX - quad.MinX) * 0.5f;
                float midY = quad.MinY + (quad.MaxY - quad.MinY) * 0.5f;
                _quads = new Quad[]
                {
                    new Quad(quad.MinX, quad.MinY, midX, midY),
                    new Quad(midX, quad.MinY, quad.MaxX, midY),
                    new Quad(midX, midY, quad.MaxX, quad.MaxY),
                    new Quad(quad.MinX, midY, midX, quad.MaxY),
                };
            }

            /// <summary>
            /// Очистить
            /// </summary>
            internal void Clear()
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (_nodes[i] != null)
                    {
                        _tree._poolNodes.Push(_nodes[i]);
                        _nodes[i].Clear();
                        _nodes[i] = null;
                    }
                }

                for (int i = 0; i < _items.Count; ++i)
                {
                    _tree._poolItems.Push(_items[i]);
                    _items[i].Node = null;
                    _items[i].Value = default;
                }

                _tree = null;
                _parent = null;
                _isSplited = false;

                _items.Clear();
            }

            /// <summary>
            /// Добавить элемент
            /// </summary>
            /// <param name="item"></param>
            internal void Add(Item item)
            {
                if (_isSplited)
                {
                    for (int i = 0; i < 4; ++i)
                    {
                        if (_quads[i].QuadIsInside(ref item.Quad))
                        {
                            if (_nodes[i] == null)
                            {
                                _nodes[i] = _tree.CreateNode(_tree, this, _depth + 1, ref _quads[i]);
                            }

                            _nodes[i].Add(item);
                            return;
                        }
                    }

                    _items.Add(item);
                    item.Node = this;
                    return;
                }

                _items.Add(item);
                item.Node = this;

                if (_items.Count >= _tree._splitCount
                    && _depth < _tree._depthLimit)
                {
                    _isSplited = true;
                }
            }

            internal bool Remove(Item item)
            {
                if (_items.Remove(item))
                {
                    return true;
                }

                if (_isSplited)
                {
                    for (int i = 0; i < 4; ++i)
                    {
                        if (_nodes[i] != null)
                        {
                            if (_nodes[i].Remove(item))
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }

            /// <summary>
            /// Найти объекты находящиеся в области
            /// </summary>
            /// <param name="quad">Область поиска</param>
            /// <param name="values">Найденные объекты</param>
            internal void SearchInsideQuad(ref Quad quad, List<T> values)
            {
                FindCollisionsDown(ref quad, values);
            }

            /// <summary>
            /// Поиск колизий относительно точки
            /// </summary>
            /// <param name="x">Координата X</param>
            /// <param name="y">Координата Y</param>
            /// <param name="values">Найденные колизии</param>
            internal void SearchСollisionWithPoint(float x, float y, List<T> values)
            {
                if (_items.Count > 0)
                {
                    for (int i = 0; i < _items.Count; ++i)
                    {
                        if (_items[i].Quad.PointIsInside(x, y))
                        {
                            values.Add(_items[i].Value);
                        }
                    }
                }

                for (int i = 0; i < 4; ++i)
                {
                    if (_nodes[i] != null)
                    {
                        _nodes[i].SearchСollisionWithPoint(x, y, values);
                    }
                }
            }

            /// <summary>
            /// Поиск колизий относительно элемента
            /// </summary>
            /// <param name="item">Элемент</param>
            /// <param name="values">Колизии</param>
            internal void FindCollisions(Item item, List<T> values)
            {
                FindCollisionsFromThis(item, ref item.Quad, values);

                FindCollisionsFromСhild(ref item.Quad, values);

                FindCollisionsUp(ref item.Quad, values);
            }

            private void FindCollisionsFromThis(Item item, ref Quad quad, List<T> values)
            {
                if (_items.Count > 0)
                {
                    for (int i = 0; i < _items.Count; ++i)
                    {
                        if (item != _items[i] && quad.Intersects(ref _items[i].Quad))
                        {
                            values.Add(_items[i].Value);
                        }
                    }
                }
            }

            private void FindCollisionsDown(ref Quad quad, List<T> values)
            {
                FindCollisionsFromThis(ref quad, values);

                FindCollisionsFromСhild(ref quad, values);
            }

            private void FindCollisionsFromСhild(ref Quad quad, List<T> values)
            {
                if (_isSplited)
                {
                    for (int i = 0; i < 4; ++i)
                    {
                        if (_nodes[i] != null)
                        {
                            _nodes[i].FindCollisionsDown(ref quad, values);
                        }
                    }
                }
            }

            private void FindCollisionsFromThis(ref Quad quad, List<T> values)
            {
                if (_items.Count > 0)
                {
                    for (int i = 0; i < _items.Count; ++i)
                    {
                        if (quad.Intersects(ref _items[i].Quad))
                        {
                            values.Add(_items[i].Value);
                        }
                    }
                }
            }

            private void FindCollisionsUp(ref Quad quad, List<T> values)
            {
                if (_parent != null)
                {
                    _parent.FindCollisionsParent(ref quad, values);
                }

            }

            private void FindCollisionsParent(ref Quad quad, List<T> values)
            {
                FindCollisionsFromThis(ref quad, values);

                FindCollisionsUp(ref quad, values);
            }
        }

        protected class Item
        {
            internal Node Node;
            internal T Value;
            internal Quad Quad;
        }
    }

    /// <summary>
    /// Квадрат
    /// </summary>
    public struct Quad
    {
        public float MinX { get; }
        public float MinY { get; }
        public float MaxX { get; }
        public float MaxY { get; }

        public Quad(float minX, float minY, float maxX, float maxY)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }

        /// <summary>
        /// Квадрат пересекается с другим квадратом
        /// </summary>
        public bool Intersects(ref Quad other)
        {
            return MinX < other.MaxX && MinY < other.MaxY && MaxX > other.MinX && MaxY > other.MinY;
        }

        /// <summary>
        /// Квадрат полностью содержит другой квадрат
        /// </summary>
        public bool QuadIsInside(ref Quad other)
        {
            return other.MinX >= MinX && other.MinY >= MinY && other.MaxX <= MaxX && other.MaxY <= MaxY;
        }

        /// <summary>
        /// Точка находится внутри квадрата
        /// </summary>
        public bool PointIsInside(float x, float y)
        {
            return x > MinX && y > MinY && x < MaxX && y < MaxY;
        }
    }
}

//1) Объект изменил свою позицию, нужен API для валидации и перемещения объекта по дереву
//3) Поиск с уточнением в радиусе