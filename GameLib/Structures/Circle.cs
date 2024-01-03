﻿using System;

namespace GameLib.Structures
{
    public struct Circle
    {
        /// <summary>
        /// Точка центра
        /// </summary>
        public readonly Point Center;
        /// <summary>
        /// Радиус
        /// </summary>
        public readonly long Radius;

        public Circle(Point center, long radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// Точка находится внутри окружности
        /// </summary>
        /// <param name="point">Точка</param>
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
