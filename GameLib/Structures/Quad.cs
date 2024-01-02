using System;

namespace GameLib.Structures
{
    public struct Quad
    {
        private readonly Point _min;
        private readonly Point _max;
        private readonly Point _center;
        private readonly long _width;
        private readonly long _height;

        public Quad(Point min, Point max)
        {
            _min = min;
            _max = max;
            _width = _max.X - _min.X;
            _height = _max.Y - _min.Y;
            _center = new Point(
                x: (long)Math.Round((_min.X + _max.X) * 0.5f),
                y: (long)Math.Round((_min.Y + _max.Y) * 0.5f));
        }

        /// <summary>
        /// Окружность пересекается с квадратом
        /// </summary>
        /// <param name="circle">Окружность</param>
        public bool CircleIsIntersects(Circle circle)
        {
            var xDistance = Math.Abs(circle.Center.X - _center.X);
            var yDistance = Math.Abs(circle.Center.Y - _center.Y);

            var halfWidth = _width * 0.5;
            var halfHeight = _height * 0.5;

            if (xDistance > halfWidth + circle.Radius) { return false; }
            if (yDistance > halfHeight + circle.Radius) { return false; }

            if (xDistance <= halfWidth) { return true; }
            if (yDistance <= halfHeight) { return true; }

            var cornerDistance = Math.Pow(xDistance - halfWidth, 2f) + Math.Pow(yDistance - halfHeight, 2f);

            return cornerDistance <= Math.Pow(circle.Radius, 2);
        }

        /// <summary>
        /// Точка находится внутри квадрата
        /// </summary>
        /// <param name="x">Центр точки. X</param>
        /// <param name="y">Центр точки. Y</param>
        public bool PointIsInside(Point point)
        {
            return point.X >= _min.X && point.Y >= _min.Y
                && point.X <= _max.X && point.Y <= _max.Y;
        }

        /// <summary>
        /// Поличить внутренние квадраты
        /// </summary>
        public Quad[] GetChildQuads()
        {
            return new Quad[]
            {
                new Quad(_min, _center),
                new Quad(
                    min: new Point(_center.X, _min.Y),
                    max: new Point(_max.X, _center.Y)),
                new Quad(_center, _max),
                new Quad(
                    min: new Point(_min.X, _center.Y),
                    max: new Point(_center.X, _max.Y)),
            };
        }

        public override string ToString()
        {
            return $"Min:[{_min}]; Max:[{_max}]; Center:[{_center}]; Width:[{_width}]; Height:[{_height}]";
        }
    }
}
