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
    /// Межзвездные врата;
    /// Необходимы для перемещения между звездами
    /// </summary>
    [Serializable]
    public class Gate : Entity { }

    /// <summary>
    /// Стационарная турель в космическом пространстве
    /// </summary>
    [Serializable]
    public class Turret : Entity { }
}
