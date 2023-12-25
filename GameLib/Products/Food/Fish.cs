using GameLib.Attributes;
using GameLib.Components;

using Type = GameLib.Enums.ProductType;

namespace GameLib.Products.Food
{
    /// <summary>
    /// Рыба  
    /// </summary>
    [ProductType(Type.Fish)]
    public sealed class Fish : Product { }
}
