using GameLib.Attributes;
using GameLib.Components;
using GameLib.Mechanics.Products.Enums;
using Type = GameLib.Mechanics.Products.Enums.ProductType;

namespace GameLib.Products.Lvl2
{
    /// <summary>
    /// Каучук
    /// </summary>
    [ProductType(Type.Elastic)]
    public sealed class Elastic : Product { }
}
