using GameLib.Attributes;
using GameLib.Components;
using Type = GameLib.Enums.ProductType;

namespace GameLib.Products.Lvl0
{
    /// <summary>
    /// Вода
    /// </summary>
    [ProductType(Type.Water)]
    public sealed class Water : Product { }
}
