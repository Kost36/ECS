using GameLib.Attributes;
using GameLib.Components;
using GameLib.Mechanics.Products.Enums;
using Type = GameLib.Mechanics.Products.Enums.ProductType;

namespace GameLib.Products.Food
{
    /// <summary>
    /// Зерно
    /// </summary>
    [ProductType(Type.Grain)]
    public sealed class Grain : Product { }
}
