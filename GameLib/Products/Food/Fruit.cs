using GameLib.Attributes;
using GameLib.Components;
using GameLib.Mechanics.Products.Enums;
using Type = GameLib.Mechanics.Products.Enums.ProductType;

namespace GameLib.Products.Food
{
    /// <summary>
    /// Фрукты  
    /// </summary>
    [ProductType(Type.Fruit)]
    public sealed class Fruit : Product { }
}
