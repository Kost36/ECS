using ECSCore.BaseObjects;
using GameLib.Enums;
using GameLib.Providers;

namespace GameLib.Components
{
    /// <summary>
    /// Продукт
    /// </summary>
    public abstract class Product : ComponentBase
    {
        public Product()
        {
            var productType = ProductTypeProvider.GetProductType(typeof(Product));
            ProductType = productType;
        }

        /// <summary>
        /// Тип продукта
        /// </summary>
        public readonly ProductType ProductType;
    }
}
