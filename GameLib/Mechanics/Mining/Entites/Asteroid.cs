using ECSCore.BaseObjects;
using GameLib.Attributes;
using System;

namespace GameLib.Mechanics.Mining.Entites
{
    /// <summary>
    /// Астеройд
    /// </summary>
    [Serializable]
    [LifeTimeInformation(minutes: 24)]
    public class Asteroid : Entity { }
}
