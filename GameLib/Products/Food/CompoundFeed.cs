using GameLib.Attributes;
using GameLib.Components;

using Type = GameLib.Enums.ProductType;

namespace GameLib.Products.Food
{
    /// <summary>
    /// Комбикорм  
    /// </summary>
    [ProductType(Type.CompoundFeed)]
    public sealed class CompoundFeed : Product { }
}
