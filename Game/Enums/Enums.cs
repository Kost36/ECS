using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Enums
{
    /// <summary>
    /// Перечисление состояний корабля
    /// </summary>
    public enum StateShip
    {
        /// <summary>
        /// Ожидание
        /// </summary>
        WAIT,
        /// <summary>
        /// Перемещение
        /// </summary>
        MOVE,
        /// <summary>
        /// Торговать
        /// </summary>
        TRADE,

        ///// <summary>
        ///// Защита
        ///// </summary>
        //DEFEND,
        ///// <summary>
        ///// Бегство
        ///// </summary>
        //ESCAPE,
    }
}