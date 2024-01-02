using GameLib.Attributes;
using GameLib.Components;
using GameLib.Mechanics.Products.Enums;
using Type = GameLib.Mechanics.Products.Enums.ProductType;

namespace GameLib.Products.Food
{
    /// <summary>
    /// Мясо  
    /// </summary>
    [ProductType(Type.Meat)]
    public sealed class Meat : Product { }
}
