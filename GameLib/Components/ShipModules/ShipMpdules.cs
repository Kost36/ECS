using ECSCore.BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Components.ShipModules
{
    /// <summary>
    /// Модули корабля
    /// </summary>
    public abstract class ShipModule : ComponentBase
    {

    }

    /// <summary>
    /// Двигатели
    /// </summary>
    public abstract class ShipEngine : ShipModule
    {

    }

    /// <summary>
    /// Энергетическая установка
    /// </summary>
    public abstract class ShipPowerUnit : ShipModule
    {

    }

    /// <summary>
    /// Щиты
    /// </summary>
    public abstract class ShipShild : ShipModule
    {

    }

    /// <summary>
    /// Оружие
    /// </summary>
    public abstract class ShipGun : ShipModule
    {

    }

    /// <summary>
    /// Турели
    /// </summary>
    public abstract class ShipTurret : ShipModule
    {

    }
}