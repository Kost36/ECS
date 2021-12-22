using ECSCore.BaseObjects;
using Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components.ObjectStates
{
    /// <summary>
    /// Компонент позиции
    /// </summary>
    public class Pozition : ComponentBase
    {
        public float X;
        public float Y;
        public float Z;
    }
    /// <summary>
    /// Компонент направления
    /// </summary>
    public class Direction : ComponentBase
    {
        public float X;
        public float Y;
        public float Z;
    }
    /// <summary>
    /// Компонент задание корабля
    /// </summary>
    public class ShipState : ComponentBase
    {
        /// <summary>
        /// Состояние корабля
        /// </summary>
        public StateShip StateShip;
    }
    /// <summary>
    /// Компонент энерги
    /// </summary>
    public class Enargy
    {
        public float ValueMax;
        public float Value;
    }
    /// <summary>
    /// Компонент прочности/здоровья
    /// </summary>
    public class Health
    {
        public float ValueMax;
        public float Value;
    }
    /// <summary>
    /// Компонент щита
    /// </summary>
    public class Shild
    {
        public float ValueMax;
        public float Value;
    }
    /// <summary>
    /// Компонент трюма (вместимости) 
    /// М.куб
    /// </summary>
    public class Hold
    {
        public float ValueMax;
        public float Value;
    }
    /// <summary>
    /// Компонент веса
    /// Кг
    /// </summary>
    public class Weight
    {
        public float Value;
    }
}