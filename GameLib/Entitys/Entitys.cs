using ECSCore.BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Entitys
{
    /// <summary>
    /// Пуля
    /// </summary>
    [Serializable]
    public class Bullet : Entity { }
    /// <summary>
    /// Луч
    /// </summary>
    [Serializable]
    public class Laser : Entity { }
    /// <summary>
    /// Ракета
    /// </summary>
    [Serializable]
    public class Rocket : Entity { }

    /// <summary>
    /// Контейнер с товаром;
    /// При уничтожении корабля/станции - все товары раскидываются по контейнерам.
    /// Контейнеры летят в рандомном направлении из точки уничтожения с небольшой скоростью.
    /// Контейнер существует некоторое время (Далее извлекается из вселенной)
    /// </summary>
    [Serializable]
    public class Container : Entity { }

    /// <summary>
    /// Дрон;
    /// Виды: Ремонтный/Грузовой/Защитный/Штурмовой
    /// </summary>
    [Serializable]
    public class Drone : Entity { }

    /// <summary>
    /// Корабль
    /// </summary>
    [Serializable]
    public class Ship : Entity { }
    
    /// <summary>
    /// Стационарная турель в космическом пространстве
    /// </summary>
    [Serializable]
    public class Turret : Entity { }

    /// <summary>
    /// Станция
    /// </summary>
    [Serializable]
    public class Stantion : Entity { }

    /// <summary>
    /// Астеройд
    /// </summary>
    [Serializable]
    public class Asteroid : Entity { }

    /// <summary>
    /// Межзвездные врата;
    /// Необходимы для перемещения между звездами
    /// </summary>
    [Serializable]
    public class Gate : Entity { }
}