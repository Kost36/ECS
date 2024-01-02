using GameLib.Attributes;
using GameLib.Components;
using GameLib.Mechanics.Products.Enums;
using Type = GameLib.Mechanics.Products.Enums.ProductType;

namespace GameLib.Products.Food
{
    /// <summary>
    /// Овощи 
    /// </summary>
    [ProductType(Type.Vegetables)]
    public sealed class Vegetables : Product { }
}
