using ECSCore.BaseObjects;
using System;

namespace GameLib.Entitys.StaticEntitys
{
    /// <summary>
    /// Система
    /// </summary>
    [Serializable]
    public class System : Entity { }

    /// <summary>
    /// Сектор
    /// </summary>
    [Serializable]
    public class Sector : Entity { }

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

    /// <summary>
    /// Станция
    /// </summary>
    [Serializable]
    public class Stantion : Entity { }

    /// <summary>
    /// Стационарная турель в космическом пространстве
    /// </summary>
    [Serializable]
    public class Turret : Entity { }
}
