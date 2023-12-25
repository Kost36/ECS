using GameLib.Attributes;
using GameLib.Components;

using Type = GameLib.Enums.ProductType;

namespace GameLib.Products.Food
{
    /// <summary>
    /// Фрукты  
    /// </summary>
    [ProductType(Type.Fruit)]
    public sealed class Fruit : Product { }
}
