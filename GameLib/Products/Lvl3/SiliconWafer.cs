using GameLib.Attributes;
using GameLib.Components;
using Type = GameLib.Enums.ProductType;

namespace GameLib.Products.Lvl3
{
    /// <summary>
    /// Кремниевая пластина
    /// </summary>
    [ProductType(Type.SiliconWafer)]
    public sealed class SiliconWafer : Product { }
}
