using ECSCore.BaseObjects;
using System;

namespace GameLib.Entitys.DynamicEntitys
{
    /// <summary>
    /// Корабль
    /// </summary>
    [Serializable]
    public class Ship : Entity { }

    /// <summary>
    /// Дрон;
    /// Виды: Ремонтный/Грузовой/Защитный/Штурмовой
    /// </summary>
    [Serializable]
    public class Drone : Entity { }

    /// <summary>
    /// Ракета
    /// </summary>
    [Serializable]
    public class Rocket : Entity { }

    /// <summary>
    /// Луч
    /// </summary>
    [Serializable]
    public class Laser : Entity { }

    /// <summary>
    /// Пуля
    /// </summary>
    [Serializable]
    public class Bullet : Entity { }

    /// <summary>
    /// Контейнер с товаром;
    /// При уничтожении корабля/станции - все товары раскидываются по контейнерам.
    /// Контейнеры летят в рандомном направлении из точки уничтожения с небольшой скоростью.
    /// Контейнер существует некоторое время (Далее извлекается из вселенной)
    /// </summary>
    [Serializable]
    public class Container : Entity { }
}
