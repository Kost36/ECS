using System;

namespace GameLib.Structures
{
    public struct Circle
    {
        public readonly Point Center;
        public readonly long Radius;

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

        public override string ToString()
        {
            return $"Center:[{Center}]; Radius:[{Radius}]";
        }
    }
}
