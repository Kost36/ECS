using GameLib.Structures;
using System.Collections.Generic;

namespace GameLib.Collections.QuadTree
{
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
}
