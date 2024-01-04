namespace MathLib.Structures
{
    public struct Point3d
    {
        public float X;
        public float Y;
        public float Z;

        public Point3d(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return $"X:[{X}]; Y:[{Y}]; Z:[{Z}]";
        }
    }
}
