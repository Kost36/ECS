using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Enums
{
    /// <summary>
    /// Размер корпуса корабля
    /// </summary>
    public enum ShipBodySize
    {
        /// <summary>
        /// Сверх малый;
        /// Размер: 5-10м;
        /// Дроны;
        /// </summary>
        M0,
        /// <summary>
        /// Очень маленький;
        /// Размер: 10-30м;
        /// Легкие истребители, Разветчики, Транспорт
        /// </summary>
        M1,
        /// <summary>
        /// Маленький;
        /// Размер: 30-60м
        /// Истребители, Транспорт
        /// </summary>
        M2,
        /// <summary>
        /// Средний;
        /// Размер: 60-100м
        /// Тяжелые истребители, Транспорт
        /// </summary>
        M3,
        /// <summary>
        /// Болшой;
        /// Размер: 100-200м
        /// Ракетоносцы, Корветы, Транспорт
        /// </summary>
        M4,
        /// <summary>
        /// Огромный;
        /// Размер: 200-300м
        /// Фрегаты, Эсминцы
        /// </summary>
        M5,
        /// <summary>
        /// Сверхогромный;
        /// Размер: 300-500м
        /// Авианосецы
        /// </summary>
        M6,
    }
    /// <summary>
    /// Размер модуля корабля
    /// </summary>
    public enum ShipModuleSize
    {
        /// <summary>
        /// Малый;
        /// </summary>
        S,
        /// <summary>
        /// Средний;
        /// </summary>
        L,
        /// <summary>
        /// Болшой;
        /// </summary>
        M,
        /// <summary>
        /// Огромный;
        /// </summary>
        XL
    }

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