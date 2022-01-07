using ECSCore.BaseObjects;
using GameLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Components.ShipModules
{
    /// <summary>
    /// Базовый компонент корпуса корабля
    /// </summary>
    public abstract class ShipBody : ComponentBase 
    {
        /// <summary>
        /// Размер корабля
        /// </summary>
        public ShipSize ShipSize;
        /// <summary>
        /// Вес 
        /// Кг.
        /// </summary>
        public int Weight;
        /// <summary>
        /// Объем трюма
        /// М.куб
        /// </summary>
        public int HoldVolume;
        /// <summary>
        /// Установленные модули
        /// </summary>
        public List<ShipModule> ShipModules = new List<ShipModule>();



        /// <summary>
        /// Двигатель
        /// </summary>
        public ShipEngine ShipEngine;
        /// <summary>
        /// Энергетическая установка
        /// </summary>
        public ShipPowerUnit ShipPowerUnit;
        /// <summary>
        /// Щиты
        /// </summary>
        public ShipShild ShipShild;
    }

    #region MO - дроны
    /// <summary>
    /// Дрон - погрузчик
    /// </summary>
    public class DroneLoaderBody : ShipBody
    {

        /// <summary>
        /// Дви
        /// </summary>
    }
    /// <summary>
    /// Дрон - ремонтник
    /// </summary>
    public class DroneRepairerBody : ShipBody
    {

    }
    /// <summary>
    /// Дрон - строитель
    /// </summary>
    public class DroneBuilderBody : ShipBody
    {

    }
    /// <summary>
    /// Дрон - защитник
    /// </summary>
    public class DroneDefenderBody : ShipBody
    {

    }
    #endregion
}