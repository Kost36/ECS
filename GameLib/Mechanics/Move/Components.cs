using ECSCore.BaseObjects;
using System;

// Компоненты механники перемещения
namespace GameLib.Components.Move
{
    /// <summary>
    /// Компонент заданной позиции.
    /// Позиция, в которую нужно переместиться
    /// </summary>
    [Serializable]
    public class PozitionSV : ComponentBase
    {
        /// <summary>
        /// Позиция X
        /// </summary>
        public float X;
        /// <summary>
        /// Позиция Y
        /// </summary>
        public float Y;
        /// <summary>
        /// Позиция Z
        /// </summary>
        public float Z;
    }

    /// <summary>
    /// Компонент скорости
    /// (m/sec)
    /// </summary>
    [Serializable]
    public class Speed : ComponentBase
    {
        /// <summary>
        /// Cкорости по оси X
        /// (m/sec)
        /// </summary>
        public float dX;
        /// <summary>
        /// Cкорости по оси Y
        /// (m/sec)
        /// </summary>
        public float dY;
        /// <summary>
        /// Cкорости по оси Z
        /// (m/sec)
        /// </summary>
        public float dZ;
        /// <summary>
        /// Фактической скорости
        /// (m/sec)
        /// </summary>
        public float Fact;
        /// <summary>
        /// Максимальной скорости
        /// (m/sec)
        /// </summary>
        public float Max;
    }

    /// <summary>
    /// Компонент заданной скорости.
    /// (m/sec)
    /// </summary>
    [Serializable]
    public class SpeedSV : ComponentBase
    {
        /// <summary>
        /// Заданная скорость по оси X
        /// (m/sec)
        /// </summary>
        public float dXSV;
        /// <summary>
        /// Заданная скорость по оси Y
        /// (m/sec)
        /// </summary>
        public float dYSV;
        /// <summary>
        /// Заданная скорость по оси Z
        /// (m/sec)
        /// </summary>
        public float dZSV;
        /// <summary>
        /// Заданная скорость
        /// (m/sec)
        /// </summary>
        public float SVSpeed;
        /// <summary>
        /// Флаг - заданная скорость изменена
        /// </summary>
        public bool Update;
    }

    /// <summary>
    /// Компонент ускорения
    /// (dm/sec)
    /// </summary>
    [Serializable]
    public class Acceleration : ComponentBase
    {
        /// <summary>
        /// Ускорение 
        /// (dm/sec)
        /// </summary>
        public float Acc = 0.05f;
        /// <summary>
        /// Использование энергии, для ускорения
        /// (value/sec)
        /// </summary>
        public float EnargyUse = 5;
        /// <summary>
        /// Флаг - скорость достигнута
        /// </summary>
        public bool SpeedOk;
    }

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

    /// <summary>
    /// Компонент пути торможения
    /// </summary>
    [Serializable]
    public class WayToStop : ComponentBase
    {
        /// <summary>
        /// Длинна пути торможения в метрах
        /// </summary>
        public float Len;
        /// <summary>
        /// Флаг - наличие энергии, для полного останова
        /// </summary>
        public bool EnargyHave;
    }
}