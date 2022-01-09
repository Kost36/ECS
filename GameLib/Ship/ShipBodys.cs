using ECSCore.BaseObjects;
using GameLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameLib.Components.ShipModules.ShipModule;

namespace GameLib.Components.ShipModules
{
    /// <summary>
    /// Базовый компонент корпуса корабля
    /// </summary>
    public abstract class ShipBody : ComponentBase 
    {
        /// <summary>
        /// Максимальная прочность
        /// </summary>
        public int MaxHealth;
        /// <summary>
        /// Размер корабля
        /// </summary>
        public ShipBodySize ShipBodySize;
        /// <summary>
        /// Размер (длинна в метрах)
        /// </summary>
        public int Size;
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
    }

    #region MO - дроны
    /// <summary>
    /// Корпуса кораблей класса M0
    /// </summary>
    public class ShipBodyM0 : ShipBody
    {
        /// <summary>
        /// Размер модулей корабля
        /// </summary>
        public ShipModuleSize ShipModuleSize = ShipModuleSize.S;
        /// <summary>
        /// Двигатель
        /// </summary>
        public ShipModuleEngine ShipModuleEngine;
        /// <summary>
        /// Энергетическая установка
        /// </summary>
        public ShipPowerUnit ShipPowerUnit;
        /// <summary>
        /// Щиты
        /// </summary>
        public ShipShild ShipShild;
    }

    /// <summary>
    /// Дрон - погрузчик
    /// </summary>
    public class DroneLoaderBody : ShipBodyM0
    {
        public DroneLoaderBody()
        {
            MaxHealth = 500;
            ShipBodySize = ShipBodySize.M0;
            Weight = 650; //Кг
            Size = 6; //М
            HoldVolume = 5; //м.куб
            //ShipEngine = ;
            //ShipPowerUnit=;
            //ShipShild =;
        }
    }
    /// <summary>
    /// Дрон - ремонтник
    /// </summary>
    public class DroneRepairerBody : ShipBodyM0
    {
        public DroneRepairerBody()
        {
            MaxHealth = 500;
            ShipBodySize = ShipBodySize.M0;
            Weight = 650; //Кг
            Size = 6; //М
            HoldVolume = 5; //м.куб
            //ShipEngine = ;
            //ShipPowerUnit=;
            //ShipShild =;
        }
    }
    /// <summary>
    /// Дрон - строитель
    /// </summary>
    public class DroneBuilderBody : ShipBodyM0
    {
        public DroneBuilderBody()
        {
            MaxHealth = 500;
            ShipBodySize = ShipBodySize.M0;
            Weight = 650; //Кг
            Size = 6; //М
            HoldVolume = 5; //м.куб
            //ShipEngine = ;
            //ShipPowerUnit=;
            //ShipShild =;
        }
    }
    /// <summary>
    /// Дрон - защитник
    /// </summary>
    public class DroneBattleBody : ShipBodyM0
    {
        public DroneBattleBody()
        {
            MaxHealth = 500;
            ShipBodySize = ShipBodySize.M0;
            Weight = 650; //Кг
            Size = 6; //М
            HoldVolume = 5; //м.куб
            //ShipEngine = ;
            //ShipPowerUnit=;
            //ShipShild =;
            //ShipGun =;
        }
        /// <summary>
        /// Оружие
        /// </summary>
        public ShipGun ShipGun;
    }
    #endregion
}