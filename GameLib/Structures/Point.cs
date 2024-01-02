namespace GameLib.Structures
{
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
}
