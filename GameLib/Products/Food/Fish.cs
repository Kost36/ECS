using GameLib.Attributes;
using GameLib.Components;
using GameLib.Mechanics.Products.Enums;
using Type = GameLib.Mechanics.Products.Enums.ProductType;

namespace GameLib.Products.Food
{
    /// <summary>
    /// Рыба  
    /// </summary>
    [ProductType(Type.Fish)]
    public sealed class Fish : Product { }
}
