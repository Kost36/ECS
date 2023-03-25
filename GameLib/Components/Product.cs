using ECSCore.BaseObjects;
using GameLib.Products;
using GameLib.Providers;

namespace GameLib.Components
{
    /// <summary>
    /// Продукт
    /// </summary>
    public abstract class Product : ComponentBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
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
