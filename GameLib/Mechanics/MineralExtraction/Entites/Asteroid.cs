using ECSCore.BaseObjects;
using GameLib.Attributes;
using System;

namespace GameLib.Mechanics.MineralExtraction.Entites
{
    /// <summary>
    /// Астеройд
    /// </summary>
    [Serializable]
    [LifeTimeInformation(hours: 24)]
    public class Asteroid : Entity { }
}
