using GameLib.Attributes;
using GameLib.Components;
using GameLib.Mechanics.Products.Enums;
using Type = GameLib.Mechanics.Products.Enums.ProductType;

namespace GameLib.Products.Food
{
    /// <summary>
    /// Комбикорм  
    /// </summary>
    [ProductType(Type.CompoundFeed)]
    public sealed class CompoundFeed : Product { }
}
