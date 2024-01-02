using GameLib.Attributes;
using GameLib.Components;
using GameLib.Mechanics.Products.Enums;
using Type = GameLib.Mechanics.Products.Enums.ProductType;

namespace GameLib.Products.Lvl0
{
    /// <summary>
    /// Аккумулятор / батарея с энергией.
    /// 3 кВт
    /// </summary>
    [ProductType(Type.Battery)]
    public sealed class Battery : Product { }
}
