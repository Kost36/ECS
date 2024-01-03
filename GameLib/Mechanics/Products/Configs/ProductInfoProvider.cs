using GameLib.Mechanics.Products.Enums;
using System;
using System.Collections.Generic;

namespace GameLib.Mechanics.Products.Configs
{
    /// <summary>
    /// Поставщик информации о товарах
    /// </summary>
    public static class ProductInfoProvider
    {
        private static readonly Dictionary<ProductType, ProductInfo> _collection = new Dictionary<ProductType, ProductInfo>()
        {
            { ProductType.Unknown, null},

            //0 Уровень
            { ProductType.Enargy, new ProductInfo() { ProductType = ProductType.Enargy, Volume = 1, Weight = 1} },
            { ProductType.Battery, new ProductInfo() { ProductType = ProductType.Battery, Volume = 1, Weight = 1} },
            { ProductType.Water, new ProductInfo() { ProductType = ProductType.Water, Volume = 1, Weight = 1} },

            //1 уровень - сырьё
            { ProductType.Ice, new ProductInfo() { ProductType = ProductType.Ice, Volume = 1, Weight = 1} },
            { ProductType.Iron, new ProductInfo() { ProductType = ProductType.Iron, Volume = 1, Weight = 1} },
            { ProductType.Copper, new ProductInfo() { ProductType = ProductType.Copper, Volume = 1, Weight = 1} },
            { ProductType.Titanium, new ProductInfo() { ProductType = ProductType.Titanium, Volume = 1, Weight = 1} },
            { ProductType.Сhrome, new ProductInfo() { ProductType = ProductType.Сhrome, Volume = 1, Weight = 1} },
            //Олово
            //Свинец
            //Цинк
            //Серебро 
            //Золото
            { ProductType.Silicon, new ProductInfo() { ProductType = ProductType.Silicon, Volume = 1, Weight = 1} },
            { ProductType.Carbon, new ProductInfo() { ProductType = ProductType.Carbon, Volume = 1, Weight = 1} },
            { ProductType.Sulfur, new ProductInfo() { ProductType = ProductType.Sulfur, Volume = 1, Weight = 1} },
            { ProductType.Lithium, new ProductInfo() { ProductType = ProductType.Lithium, Volume = 1, Weight = 1} },
            { ProductType.Magnesium, new ProductInfo() { ProductType = ProductType.Magnesium, Volume = 1, Weight = 1} },
            { ProductType.Calcium, new ProductInfo() { ProductType = ProductType.Calcium, Volume = 1, Weight = 1} },
            { ProductType.Helium, new ProductInfo() { ProductType = ProductType.Helium, Volume = 1, Weight = 1} },
            { ProductType.Methane, new ProductInfo() { ProductType = ProductType.Methane, Volume = 1, Weight = 1} },
            { ProductType.Nitrogen, new ProductInfo() { ProductType = ProductType.Nitrogen, Volume = 1, Weight = 1} },
            //Аргон
            //Неон

            //2 уровень - материалы
            { ProductType.Ammonia, new ProductInfo() { ProductType = ProductType.Ammonia, Volume = 1, Weight = 1} },
            { ProductType.Elastic, new ProductInfo() { ProductType = ProductType.Elastic, Volume = 1, Weight = 1} },
            { ProductType.Fertilizer, new ProductInfo() { ProductType = ProductType.Fertilizer, Volume = 1, Weight = 1} },
            { ProductType.Hydrogen, new ProductInfo() { ProductType = ProductType.Hydrogen, Volume = 1, Weight = 1} },

            //3 уровень - еда 
            { ProductType.Grain, new ProductInfo() { ProductType = ProductType.Grain, Volume = 1, Weight = 1} },
            { ProductType.Vegetables, new ProductInfo() { ProductType = ProductType.Vegetables, Volume = 1, Weight = 1} },
            { ProductType.Fruit, new ProductInfo() { ProductType = ProductType.Fruit, Volume = 1, Weight = 1} },
            { ProductType.CompoundFeed, new ProductInfo() { ProductType = ProductType.CompoundFeed, Volume = 1, Weight = 1} },
            { ProductType.Fish, new ProductInfo() { ProductType = ProductType.Fish, Volume = 1, Weight = 1} },
            { ProductType.Meat, new ProductInfo() { ProductType = ProductType.Meat, Volume = 1, Weight = 1} },

            //4 уровень - материалы
            { ProductType.Metal, new ProductInfo() { ProductType = ProductType.Metal, Volume = 1, Weight = 1} },
            { ProductType.Rubber, new ProductInfo() { ProductType = ProductType.Rubber, Volume = 1, Weight = 1} },
            { ProductType.Plastic, new ProductInfo() { ProductType = ProductType.Plastic, Volume = 1, Weight = 1} },
            { ProductType.SiliconWafer, new ProductInfo() { ProductType = ProductType.SiliconWafer, Volume = 1, Weight = 1} },
            { ProductType.Glass, new ProductInfo() { ProductType = ProductType.Glass, Volume = 1, Weight = 1} },

            //5 уровень - товары
            { ProductType.Wiring, new ProductInfo() { ProductType = ProductType.Wiring, Volume = 1, Weight = 1} },
            { ProductType.BatteryEmpty, new ProductInfo() { ProductType = ProductType.BatteryEmpty, Volume = 1, Weight = 1} },
            { ProductType.Electronics, new ProductInfo() { ProductType = ProductType.Electronics, Volume = 1, Weight = 1} },
            { ProductType.ControlSystems, new ProductInfo() { ProductType = ProductType.ControlSystems, Volume = 1, Weight = 1} },
            { ProductType.Body, new ProductInfo() { ProductType = ProductType.Body, Volume = 1, Weight = 1} },
            { ProductType.Sheathing, new ProductInfo() { ProductType = ProductType.Sheathing, Volume = 1, Weight = 1} },

            //6 уровень - оружие, дроны, турели, модули, ракеты

            //7 уровень - cтанции, корабли
        };

        /// <summary>
        /// Получить информацию о еденице товара по типу товара
        /// </summary>
        /// <param name="productType">Перечисление типа товара</param>
        public static ProductInfo GetProductInfo(ProductType productType)
        {
            if(!_collection.TryGetValue(productType, out var productInfo))
            {
                throw new ArgumentException($"Product info not found for product type [{productType}]");
            }

            return productInfo;
        }
    }
}

//TODO Заполнить