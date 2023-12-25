using GameLib.Attributes;
using GameLib.Components;
using Type = GameLib.Enums.ProductType;

namespace GameLib.Products.Food
{
    /// <summary>
    /// Овощи 
    /// </summary>
    [ProductType(Type.Vegetables)]
    public sealed class Vegetables : Product { }
}
