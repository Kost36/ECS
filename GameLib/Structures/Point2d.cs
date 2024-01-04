namespace GameLib.Structures
{
    public struct Point2d
    {
        public long X;
        public long Y;

        public Point2d(long x, long y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"X:[{X}]; Y:[{Y}]";
        }
    }
}
