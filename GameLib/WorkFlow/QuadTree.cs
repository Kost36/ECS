using System.Collections.Generic;

namespace GameLib.WorkFlow
{
    /// <summary>
    /// Структура.
    /// 1) Детект коллизий
    /// 2) Детект видимых сканером объектов
    /// 3) Детект возможности атаки
    /// </summary>
    /// <typeparam name="T"> Тип данных </typeparam>
    public sealed class QuadTree<T> : QuadTreeBase<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="splitCount">Сколько элементов может содержать квадрат, прежде чем он разделится на 4 квадрата.</param>
		/// <param name="depthLimit">Максимальное глубина, на котором узел может находиться от корня дерева.</param>
        /// <param name="region">Обслуживаемая область</param>
        public QuadTree(int splitCount, int depthLimit, ref Quad region) : base(splitCount, depthLimit, ref region) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="splitCount">Сколько элементов может содержать квадрат, прежде чем он разделится на 4 квадрата.</param>
        /// <param name="depthLimit">Максимальное глубина, на котором узел может находиться от корня дерева.</param>
        /// <param name="region">Обслуживаемая область</param>
        public QuadTree(int splitCount, int depthLimit, Quad region) : base(splitCount, depthLimit, ref region) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="splitCount">Сколько элементов может содержать квадрат, прежде чем он разделится на 4 квадрата.</param>
        /// <param name="depthLimit">Максимальное глубина, на котором узел может находиться от корня дерева.</param>
        /// <param name="x">Обслуживаемая область - x</param>
        /// <param name="y">Обслуживаемая область - y</param>
        /// <param name="width">Обслуживаемая область - ширина</param>
        /// <param name="height">Обслуживаемая область - высота</param>
        public QuadTree(int splitCount, int depthLimit, float x, float y, float width, float height) : this(splitCount, depthLimit, new Quad(x, y, x + width, y + height)) { }

        /// <summary>
        /// Количество нод
        /// </summary>
        public int NodesCount { get => GetNodesCount(); }

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
        /// Удалить Элемент из коллекции
        /// </summary>
        /// <param name="value">Элемент</param>
        public void Remove(T value)
        {
            if (!_allItems.TryGetValue(value, out Item item))
            {
                return;
            }

            _allItems.Remove(value);
            _root.Remove(item);
        }

        /// <summary>
        /// Найдите все объекты, находящиеся в области.
        /// </summary>
        /// <param name="quad">Область поиска</param>
        /// <param name="values">Найденные объекты</param>
        public bool SearchСollisionsForQuad(ref Quad quad, out List<T> values)
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
        public bool SearchСollisionsForQuad(Quad quad, out List<T> values)
        {
            return SearchСollisionsForQuad(ref quad, out values);
        }
        /// <summary>
        /// Найдите все объекты, находящиеся в области.
        /// </summary>
        /// <param name="x">Область поиска - X начальной точки</param>
        /// <param name="y">Область поиска - Y начальной точки</param>
        /// <param name="width">Область поиска - ширина</param>
        /// <param name="height">Область поиска - высота</param>
        /// <param name="values">Объекты, находящиеся в области</param>
        public bool SearchСollisionsForQuad(float x, float y, float width, float height, out List<T> values)
        {
            var quad = new Quad(x, y, x + width, y + height);
            return SearchСollisionsForQuad(ref quad, out values);
        }

        /// <summary>
        /// Найти все элементы, перекрывающие указанную точку.
        /// </summary> 
        /// <param name="x">Позиция. X.</param>
        /// <param name="y">Позиция. Y.</param>
        /// <param name="values">Список элементов перекрывающих точку</param>
        public bool SearchСollisionsForPoint(float x, float y, out List<T> values)
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
        public bool FindCollisionsForItem(T value, out List<T> values)
        {
            values = new List<T>();
            if (_allItems.TryGetValue(value, out Item item))
            {
                item.Node.FindCollisions(item, values);
            }

            return false;
        }

        /// <summary>
        /// Очистка всего дерева
        /// </summary>
        public void Clear()
        {
            _root.Clear();
            _root = CreateNode(this, null, 0);
            _allItems.Clear();
        }
    }
}
