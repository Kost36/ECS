using GameLib.Attributes;
using GameLib.Components;
using Type = GameLib.Enums.ProductType;

namespace GameLib.Products.Lvl4
{
    /// <summary>
    /// Аккумулятор / батарея без энергией.
    /// </summary>
    [ProductType(Type.BatteryEmpty)]
    public sealed class BatteryEmpty : Product { }
}
