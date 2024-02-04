using ECSCore.BaseObjects;
using System;

namespace GameLib.Mechanics.Move.Components
{
    /// <summary>
    /// Компонент пути в заданную позицию
    /// </summary>
    [Serializable]
    public class Way : ComponentBase
    {
        /// <summary>
        /// Флаг - инициализация пройдена
        /// </summary>
        public bool InitOk;
        /// <summary>
        /// Длинна пути в метрах
        /// </summary>
        public float Len;

        /// <summary>
        /// Длинна пути в метрах по оси X
        /// </summary>
        public float LenX;
        /// <summary>
        /// Длинна пути в метрах по оси Y
        /// </summary>
        public float LenY;
        /// <summary>
        /// Длинна пути в метрах по оси Z
        /// </summary>
        public float LenZ;

        /// <summary>
        /// Нормализованный вектор направления по оси X
        /// </summary>
        public float NormX;
        /// <summary>
        /// Нормализованный вектор направления по оси Y
        /// </summary>
        public float NormY;
        /// <summary>
        /// Нормализованный вектор направления по оси Z
        /// </summary>
        public float NormZ;
    }
}
