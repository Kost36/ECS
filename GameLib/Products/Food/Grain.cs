using GameLib.Attributes;
using GameLib.Components;
using Type = GameLib.Enums.ProductType;

namespace GameLib.Products.Food
{
    /// <summary>
    /// Зерно
    /// </summary>
    [ProductType(Type.Grain)]
    public sealed class Grain : Product { }
}
