using ECSCore.BaseObjects;
using GameLib.Components;

namespace GameLib.Mechanics.Production.Components
{
    /// <summary>
    /// Строительство производственного модуля
    /// </summary>
    public abstract class ProductionModuleBuild : ComponentBase { }

    /// <summary>
    /// Строительство производственного моду
    /// </summary>
    /// <typeparam name="TProduct"> Тип производимого товара </typeparam>
    public sealed class ProductionModuleBuild<TProduct> : ProductionModuleBuild
        where TProduct : Product { }
}
