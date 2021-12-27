using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSCoreTests.Enums
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
        /// Стыковка
        /// </summary>
        DOCKING,
        /// <summary>
        /// Взлет
        /// </summary>
        UNDOCKING,
        /// <summary>
        /// Торговля
        /// </summary>
        TRADE,
        /// <summary>
        /// Добыча
        /// </summary>
        MINING,
        /// <summary>
        /// Защита
        /// </summary>
        DEFEND,
        /// <summary>
        /// Бегство
        /// </summary>
        ESCAPE,
        /// <summary>
        /// Сражение
        /// </summary>
        BATTLE,
        /// <summary>
        /// Патруль
        /// </summary>
        PATROL,
    }
}