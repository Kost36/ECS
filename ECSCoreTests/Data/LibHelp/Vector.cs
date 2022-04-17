using System;

namespace ECSCoreTests.Lib
{
    /// <summary>
    /// Вектор
    /// </summary>
    public class Vector
    {
        #region Конструкторы
        public Vector(float dX, float dY, float dZ) { X = dX; Y = dY; Z = dZ; }
        #endregion

        #region Свойства
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        #endregion

        #region Методы
        /// <summary>
        /// Длинна вектора.
        /// </summary>
        /// <param name="dX"> Дельта по оси X </param>
        /// <param name="dY"> Дельта по оси Y </param>
        /// <param name="dZ"> Дельта по оси Z </param>
        /// <returns> длинна  </returns>
        public static float Len(float dX, float dY, float dZ)
        {
            return (float)Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2) + Math.Pow(dZ, 2));
        }

        /// <summary>
        /// Длинна вектора.
        /// </summary>
        /// <param name="vector"> Вектор направления </param>
        /// <returns> длинна </returns>
        public static float Len(Vector vector)
        {
            return (float)Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2) + Math.Pow(vector.Z, 2));
        }

        /// <summary>
        /// Нормализация вектора.
        /// </summary>
        /// <param name="dX"> Дельта по оси X </param>
        /// <param name="dY"> Дельта по оси Y </param>
        /// <param name="dZ"> Дельта по оси Z </param>
        /// <returns> Нормализованный вектор </returns>
        public static Vector Norm(float dX, float dY, float dZ)
        {
            float len = Len(dX, dY, dZ);
            return new Vector(dX / len, dY / len, dZ / len);
        }

        /// <summary>
        /// Нормализация вектора.
        /// </summary>
        /// <param name="dX"> Дельта по оси X </param>
        /// <param name="dY"> Дельта по оси Y </param>
        /// <param name="dZ"> Дельта по оси Z </param>
        /// <param name="len"> Длинна вектора </param>
        /// <returns> Нормализованный вектор </returns>
        public static Vector Norm(float dX, float dY, float dZ, float len)
        {
            return new Vector(dX / len, dY / len, dZ / len);
        }
        #endregion
    }
}
