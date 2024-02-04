using ECSCore.BaseObjects;
using GameLib.Mechanics.Products.Enums;

namespace GameLib.Mechanics.Production.Components
{
    /// <summary>
    /// Компонент производимого товара на производственном модуле
    /// </summary>
    public sealed class Production : ComponentBase
    {
        /// <summary>
        /// Тип производимого товара
        /// </summary>
        public ProductType ProductType;
    }
}
