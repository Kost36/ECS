using System.Collections.Generic;

namespace GameLib.Algorithms
{
    /// <summary>
    /// Структура.
    /// 1) Детект коллизий
    /// 2) Детект видимых сканером объектов
    /// 3) Детект возможности атаки
    /// </summary>
    /// <typeparam name="T"> Тип данных </typeparam>
    public class QuadTree<T>
    {
        private readonly int _splitCount;
        private readonly int _depthLimit;

        private readonly Node _root;
        private readonly Stack<Node> _poolNodes = new Stack<Node>();
        private readonly Stack<Item> _poolItems = new Stack<Item>();
        private readonly Dictionary<T, Item> _allItems = new Dictionary<T, Item>();

        /// <summary>
        /// Количество нод
        /// </summary>
        public int NodesCount { get => GetNodesCount(); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="splitCount">Сколько элементов может содержать квадрат, прежде чем он разделится на 4 квадрата.</param>
		/// <param name="depthLimit">Максимальное глубина, на котором узел может находиться от корня дерева.</param>
        /// <param name="region">Обслуживаемая область</param>
        public QuadTree(int splitCount, int depthLimit, ref Quad region)
        {
            _root = CreateNode(this, null, 0, ref region);
            _splitCount = splitCount;
            _depthLimit = depthLimit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="splitCount">Сколько элементов может содержать квадрат, прежде чем он разделится на 4 квадрата.</param>
		/// <param name="depthLimit">Максимальное глубина, на котором узел может находиться от корня дерева.</param>
        /// <param name="region">Обслуживаемая область</param>
        /// 
		public QuadTree(int splitCount, int depthLimit, Quad region)
            : this(splitCount, depthLimit, ref region) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="splitCount">Сколько элементов может содержать квадрат, прежде чем он разделится на 4 квадрата.</param>
		/// <param name="depthLimit">Максимальное глубина, на котором узел может находиться от корня дерева.</param>
        /// <param name="x">Обслуживаемая область - X начальной точки</param>
        /// <param name="y">Обслуживаемая область - Y начальной точки</param>
        /// <param name="width">Обслуживаемая область - ширина</param>
        /// <param name="height">Обслуживаемая область - высота</param>
        public QuadTree(int splitCount, int depthLimit, float x, float y, float width, float height)
            : this(splitCount, depthLimit, new Quad(x, y, x + width, y + height)) { }

        /// <summary>
        /// Очистка всего дерева
        /// </summary>
        public void Clear()
        {
            _root.Clear();
            _root._tree = this;
            _allItems.Clear();
        }

        /// <summary>
        /// Добавить элемент в коллекцию
        /// </summary>
        /// <param name="value">Элемент</param>
        /// <param name="quad">Размер элемента</param>
        public void Add(T value, ref Quad quad)
        {
            if (!_allItems.TryGetValue(value, out Item item))
            {
                item = CreateItem(value, ref quad);
                _allItems.Add(value, item);
            }
            _root.Add(item);
        }
        /// <summary>
        /// Добавить элемент в коллекцию
        /// </summary>
        /// <param name="value">Элемент</param>
        /// <param name="quad">Размер элемента</param>
        public void Add(T value, Quad quad)
        {
            Add(value, ref quad);
        }
        /// <summary>
        /// Добавить элемент в коллекцию
        /// </summary>
        /// <param name="value">Элемент</param>
        /// <param name="x">Размер элемента - X начальной точки</param>
        /// <param name="y">Размер элемента - Y начальной точки</param>
        /// <param name="width">Размер элемента - ширина</param>
        /// <param name="height">Размер элемента - высота</param>
        public void Add(T value, float x, float y, float width, float height)
        {
            var quad = new Quad(x, y, x + width, y + height);
            Add(value, ref quad);
        }

        /// <summary>
        /// Найдите все объекты, находящиеся в области.
        /// </summary>
        /// <param name="quad">Область поиска</param>
        /// <param name="values">Найденные объекты</param>
        public bool SearchArea(ref Quad quad, ref List<T> values)
        {
            values = new List<T>();
            _root.SearchInsideQuad(ref quad, values);
            return values.Count > 0;
        }
        /// <summary>
        /// Найдите все объекты, находящиеся в области.
        /// </summary>
        /// <param name="quad">Область поиска</param>
        /// <param name="values">Найденные объекты</param>
        public bool SearchArea(Quad quad, ref List<T> values)
        {
            return SearchArea(ref quad, ref values);
        }
        /// <summary>
        /// Найдите все объекты, находящиеся в области.
        /// </summary>
        /// <param name="x">Область поиска - X начальной точки</param>
        /// <param name="y">Область поиска - Y начальной точки</param>
        /// <param name="width">Область поиска - ширина</param>
        /// <param name="height">Область поиска - высота</param>
        /// <param name="values">Объекты, находящиеся в области</param>
        public bool SearchArea(float x, float y, float width, float height, ref List<T> values)
        {
            var quad = new Quad(x, y, x + width, y + height);
            return SearchArea(ref quad, ref values);
        }

        /// <summary>
        /// Найти все элементы, перекрывающие указанную точку.
        /// </summary> 
        /// <param name="x">Позиция. X.</param>
        /// <param name="y">Позиция. Y.</param>
        /// <param name="values">Список элементов перекрывающих точку</param>
        public bool SearchСollisionsWithPoint(float x, float y, out List<T> values)
        {
            values = new List<T>();
            _root.SearchСollisionWithPoint(x, y, values);
            return values.Count > 0;
        }

        /// <summary>
        /// Найти все элементы, перекрывающие указанный элемент.
        /// </summary>
        /// <param name="value">Анализируемый элемент</param>
        /// <param name="values">Список перекрывающих элементов</param>
        public bool FindCollisionsWithItem(T value, out List<T> values)
        {
            values = new List<T>();
            if (_allItems.TryGetValue(value, out Item item))
            {
                item.Node.FindCollisions(item, values);
            }

            return false;
        }

        private int GetNodesCount()
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

        private Node CreateNode(QuadTree<T> tree, Node parent, int depth, ref Quad quad)
        {
            var branch = tree._poolNodes.Count > 0 
                ? tree._poolNodes.Pop() 
                : new Node(tree, parent, depth, ref quad);

            return branch;
        }

        protected class Node
        {
            internal Node[] _nodes = new Node[4];
            internal List<Item> _items = new List<Item>();

            internal QuadTree<T> _tree;
            internal Node _parent;
            internal Quad[] _quads;
            internal int _depth;
            internal bool _isSplited;

            internal Node(QuadTree<T> tree, Node parent, int depth, ref Quad quad)
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
//2) Объект пропал нужен API для и удаления объекта из дерева
//3) Поиск с уточнением в радиусе