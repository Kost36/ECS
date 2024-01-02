using GameLib.Structures;

namespace GameLib.Collections.QuadTree
{
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
}
