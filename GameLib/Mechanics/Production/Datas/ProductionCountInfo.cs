using GameLib.Mechanics.Products.Enums;

namespace GameLib.Mechanics.Production.Datas
{
    /// <summary>
    /// Информация о производимом товаре
    /// </summary>
    public sealed class ProductionCountInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productType">Тип продукта</param>
        /// <param name="countInCycle">Количество продукта за цикл</param>
        public ProductionCountInfo(ProductType productType, int countInCycle)
        {
            ProductType = productType;
            CountInCycle = countInCycle;
        }

        /// <summary>
        /// Тип производимого товара
        /// </summary>
        public ProductType ProductType;
        /// <summary>
        /// Количество производимого товара за цикл
        /// </summary>
        public int CountInCycle;
    }
}
