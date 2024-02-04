using MathLib.Structures;
using System;

namespace MathLib
{
    public static class Distance
    {
        public static float GetDistance(Point3d point1, Point3d point2)
        {
            var xDistance = point1.X - point2.X;
            var yDistance = point1.Y - point2.Y;
            var zDistance = point1.Z - point2.Z;

            return (float)Math.Sqrt(
                (xDistance * xDistance) + 
                (yDistance * yDistance) + 
                (zDistance * zDistance));
        }
    }
}
