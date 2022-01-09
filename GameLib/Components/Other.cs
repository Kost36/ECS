using ECSCore.BaseObjects;
using GameLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Components.Other
{
    /// <summary>
    /// Компонент интеллекта корабля
    /// </summary>
    [Serializable]
    public class ShipAi : ComponentBase { }
    /// <summary>
    /// Компонент интеллекта корабля (Торговли)
    /// </summary>
    [Serializable]
    public class ShipAiTrade : ComponentBase { }
    /// <summary>
    /// Компонент интеллекта корабля (Боевой)
    /// </summary>
    [Serializable]
    public class ShipAiWar : ComponentBase { }
    /// <summary>
    /// Компонент состояния корабля
    /// </summary>
    [Serializable]
    public class ShipState : ComponentBase
    {
        /// <summary>
        /// Состояние корабля
        /// </summary>
        public StateShip StateShip;
    }

    /// <summary>
    /// Компонент скорости вращения
    /// (value/sec)
    /// </summary>
    [Serializable]
    public class SpeedRotation : ComponentBase
    {
        /// <summary>
        /// Cкорость вращения по оси X
        /// (m/sec)
        /// </summary>
        public float dX = 0.02f;
        /// <summary>
        /// Cкорость вращения по оси Y
        /// (m/sec)
        /// </summary>
        public float dY = 0.02f;
        /// <summary>
        /// Cкорость вращения по оси Z
        /// (m/sec)
        /// </summary>
        public float dZ = 0.02f;
    }
}