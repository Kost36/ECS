using GameLib.Attributes;
using GameLib.Components;
using GameLib.Mechanics.Products.Enums;
using Type = GameLib.Mechanics.Products.Enums.ProductType;

namespace GameLib.Products.Lvl0
{
    /// <summary>
    /// Вода
    /// </summary>
    [ProductType(Type.Water)]
    public sealed class Water : Product { }
}
