using GameLib.Attributes;
using GameLib.Components;
using GameLib.Mechanics.Products.Enums;
using Type = GameLib.Mechanics.Products.Enums.ProductType;

namespace GameLib.Products.Lvl2
{
    /// <summary>
    /// Удобрение
    /// </summary>
    [ProductType(Type.Fertilizer)]
    public sealed class Fertilizer : Product { }
}
