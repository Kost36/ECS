using GameLib.Products;
using System;

namespace GameLib.Mechanics.Production.Datas
{
    /// <summary>
    /// Информация о производимом товаре
    /// </summary>
    public abstract class ProductInfo
    {
        /// <summary>
        /// Тип производимого товара
        /// </summary>
        public Type TypeProduct;
        /// <summary>
        /// Тип производимого товара
        /// </summary>
        public ProductType ProductType;
        /// <summary>
        /// Количество производимого товара за цикл
        /// </summary>
        public int CountInCycle;
    }

    /// <summary>
    /// Информация о производимом товаре
    /// </summary>
    public sealed class ProductInfo<TProduct> : ProductInfo
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="productType"> Тип производимого товара </param>
        /// <param name="countInCycle"> Количество производимого товара за цикл </param>
        public ProductInfo(ProductType productType, int countInCycle)
        {
            TypeProduct = typeof(TProduct);
            ProductType = productType;
            CountInCycle = countInCycle;
        }
    }
}
