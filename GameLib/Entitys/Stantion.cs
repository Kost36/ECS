using ECSCore.BaseObjects;
using GameLib.Attributes;
using System;

namespace GameLib.Entitys
{
    /// <summary>
    /// Станция
    /// </summary>
    [Serializable]
    [LifeTimeInformation(hours: 6)]
    public class Stantion : Entity { }
}
