using GameLib.Attributes;
using GameLib.Components;
using GameLib.Mechanics.Products.Enums;
using Type = GameLib.Mechanics.Products.Enums.ProductType;

namespace GameLib.Products.Lvl4
{
    /// <summary>
    /// Аккумулятор / батарея без энергией.
    /// </summary>
    [ProductType(Type.BatteryEmpty)]
    public sealed class BatteryEmpty : Product { }
}
