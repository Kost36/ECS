using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.LibHelp
{
    /// <summary>
    /// Вектор
    /// </summary>
    public class Vector
    {
        #region Конструкторы
        /// <summary>
        /// Инициализация вектора
        /// </summary>
        /// <param name="dX"> Длинна по оси X </param>
        /// <param name="dY"> Длинна по оси Y </param>
        /// <param name="dZ"> Длинна по оси Z </param>
        public Vector(float dX, float dY, float dZ) { X = dX; Y = dY; Z = dZ; }
        #endregion

        #region Свойства
        /// <summary>
        /// Длинна по оси X
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// Длинна по оси Y
        /// </summary>
        public float Y { get; set; }
        /// <summary>
        /// Длинна по оси Z
        /// </summary>
        public float Z { get; set; }
        #endregion

        #region Методы
        /// <summary>
        /// Длинна вектора.
        /// </summary>
        /// <param name="dX"> Длинна по оси X </param>
        /// <param name="dY"> Длинна по оси Y </param>
        /// <param name="dZ"> Длинна по оси Z </param>
        /// <returns> длинна  </returns>
        public static float Len(float dX, float dY, float dZ)
        {
            return (float)Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2) + Math.Pow(dZ, 2));
        }
        /// <summary>
        /// Длинна вектора.
        /// </summary>
        /// <param name="vector"> Вектор </param>
        /// <returns> Длинна </returns>
        public static float Len(Vector vector)
        {
            return (float)Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2) + Math.Pow(vector.Z, 2));
        }
        /// <summary>
        /// Длинна вектора.
        /// </summary>
        /// <returns> Длинна </returns>
        public float Len()
        {
            return (float)Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
        }

        /// <summary>
        /// Нормализация вектора.
        /// </summary>
        /// <param name="dX"> Длинна по оси X </param>
        /// <param name="dY"> Длинна по оси Y </param>
        /// <param name="dZ"> Длинна по оси Z </param>
        /// <returns> Нормализованный вектор </returns>
        public static Vector Norm(float dX, float dY, float dZ)
        {
            float len = Len(dX, dY, dZ);
            return new Vector(dX / len, dY / len, dZ / len);
        }
        /// <summary>
        /// Нормализация вектора.
        /// </summary>
        /// <param name="dX"> Длинна по оси X </param>
        /// <param name="dY"> Длинна по оси Y </param>
        /// <param name="dZ"> Длинна по оси Z </param>
        /// <param name="len"> Длинна вектора </param>
        /// <returns> Нормализованный вектор </returns>
        public static Vector Norm(float dX, float dY, float dZ, float len)
        {
            return new Vector(dX / len, dY / len, dZ / len);
        }
        /// <summary>
        /// Нормализация вектора.
        /// </summary>
        /// <returns> Нормализованный вектор </returns>
        public Vector Norm()
        {
            float len = Len(X, Y, Z);
            return new Vector(X / len, Y / len, Z / len);
        }
        #endregion
    }
}