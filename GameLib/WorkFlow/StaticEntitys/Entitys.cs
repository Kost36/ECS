using ECSCore.BaseObjects;
using System;

namespace GameLib.WorkFlow.StaticEntitys
{
    /// <summary>
    /// Звездная система
    /// </summary>
    [Serializable]
    public class Star : Entity { }

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
    /// Производственный модуль
    /// </summary>
    [Serializable]
    public class ProductionModule : Entity { }

    /// <summary>
    /// Стационарная турель в космическом пространстве
    /// </summary>
    [Serializable]
    public class Turret : Entity { }
}
