using GameLib.Attributes;
using GameLib.Components;
using GameLib.Mechanics.Products.Enums;
using Type = GameLib.Mechanics.Products.Enums.ProductType;

namespace GameLib.Products.Lvl3
{
    /// <summary>
    /// Кремниевая пластина
    /// </summary>
    [ProductType(Type.SiliconWafer)]
    public sealed class SiliconWafer : Product { }
}
