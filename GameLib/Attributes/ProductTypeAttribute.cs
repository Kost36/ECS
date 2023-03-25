using GameLib.Products;
using System;

namespace GameLib.Attributes
{
    /// <summary>
    /// Атрибут типа продукта
    /// </summary>
    public sealed class ProductTypeAttribute : Attribute
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="productType"> Тип продукта </param>
        public ProductTypeAttribute(ProductType productType)
        {
            ProductType = productType;
        }

        /// <summary>
        /// Тип продукта
        /// </summary>
        public ProductType ProductType { get; set; }
    }
}
