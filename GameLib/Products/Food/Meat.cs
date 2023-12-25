using GameLib.Attributes;
using GameLib.Components;
using Type = GameLib.Enums.ProductType;

namespace GameLib.Products.Food
{
    /// <summary>
    /// Мясо  
    /// </summary>
    [ProductType(Type.Meat)]
    public sealed class Meat : Product { }
}
