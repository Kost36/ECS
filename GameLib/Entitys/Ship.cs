using ECSCore.BaseObjects;
using GameLib.Attributes;
using System;

namespace GameLib.Entitys
{
    /// <summary>
    /// Корабль
    /// </summary>
    [Serializable]
    [LifeTimeInformation(minutes: 1)]
    public class Ship : Entity { }
}
