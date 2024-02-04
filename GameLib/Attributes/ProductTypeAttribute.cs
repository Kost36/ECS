using System;

namespace GameLib.Attributes
{
    /// <summary>
    /// Атрибут тип продукта
    /// </summary>
    public sealed class ProductTypeAttribute : Attribute
    {
        public ProductTypeAttribute(Mechanics.Products.Enums.ProductType type)
        {
            Type = type;
        }

        public Mechanics.Products.Enums.ProductType Type { get; set; }
    }
}
