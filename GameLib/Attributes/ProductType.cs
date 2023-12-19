using System;

namespace GameLib.Attributes
{
    /// <summary>
    /// Атрибут типа продукта
    /// </summary>
    public sealed class ProductType : Attribute
    {
        public ProductType(Enums.ProductType type)
        {
            Type = type;
        }

        public Enums.ProductType Type { get; set; }
    }
}
