using GameLib.Attributes;
using GameLib.Components;
using Type = GameLib.Enums.ProductType;

namespace GameLib.Products.Lvl2
{
    /// <summary>
    /// Удобрение
    /// </summary>
    [ProductType(Type.Fertilizer)]
    public sealed class Fertilizer : Product { }
}
