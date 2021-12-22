using ECSCore.BaseObjects;
using Game.Enums;

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
    /// Компонент направления (Нормализованный)
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
    public class Energy : ComponentBase
    {
        public float ValueMax;
        public float Value;
    }
    /// <summary>
    /// Компонент прочности/здоровья
    /// </summary>
    public class Health : ComponentBase
    {
        public float ValueMax;
        public float Value;
    }
    /// <summary>
    /// Компонент щита
    /// </summary>
    public class Shild : ComponentBase
    {
        public float ValueMax;
        public float Value;
    }
    /// <summary>
    /// Компонент трюма (вместимости) 
    /// М.куб
    /// </summary>
    public class Hold : ComponentBase
    {
        public float ValueMax;
        public float ValueUse;
    }
    /// <summary>
    /// Компонент веса
    /// Кг
    /// </summary>
    public class Weight : ComponentBase
    {
        /// <summary>
        /// Вес корабля (Кг)
        /// </summary>
        public float Value;
    }
}