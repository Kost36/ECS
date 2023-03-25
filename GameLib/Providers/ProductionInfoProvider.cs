using GameLib.Mechanics.Production.Components;
using GameLib.Products;
using GameLib.Products.Lvl0;
using GameLib.Products.Lvl1;
using System.Collections.Generic;

namespace GameLib.Mechanics.Production
{
    /// <summary>
    /// Поставщик информации о производстве продуктов
    /// </summary>
    public static class ProductionInfoProvider
    {
        /// <summary>
        /// Коллекция информации о производстве продуктов
        /// </summary>
        private static readonly Dictionary<ProductType, ProductionInfo> Collection = new Dictionary<ProductType, ProductionInfo>()
        {
            {
                ProductType.Battery, 
                new ProductionInfo<Battery>(
                    productType: ProductType.Battery, cycleTimeInSec: 60, productCountInCycle: 300)
            },
            {
                ProductType.Iron, 
                new ProductionInfo<Iron, Battery, Ore>(
                    productType: ProductType.Iron, cycleTimeInSec: 90, productCountInCycle: 10, 
                    raw1Type: ProductType.Battery, raw1CountInCycle: 100, 
                    raw2Type: ProductType.Ore, raw2CountInCycle: 20)
            },
        };

        /// <summary>
        /// Получить информацию о производимом продукте
        /// </summary>
        /// <param name="productType"> Тип производимого продукта </param>
        /// <param name="productionInfo"> Информация о производимом продукте </param>
        public static bool GetProductionInfo(ProductType productType, out ProductionInfo productionInfo)
        {
            return Collection.TryGetValue(productType , out productionInfo);
        }
    }
}