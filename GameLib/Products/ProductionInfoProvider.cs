using GameLib.Enums;
using GameLib.Mechanics.Production.Components;
using GameLib.Products.Food;
using GameLib.Products.Lvl0;
using GameLib.Products.Lvl1;
using GameLib.Products.Lvl2;
using GameLib.Products.Lvl3;
using GameLib.Products.Lvl4;
using System.Collections.Generic;
using System.Linq;

namespace GameLib.Products
{
    /// <summary>
    /// Поставщик информации о производстве продуктов
    /// </summary>
    public static class ProductionInfoProvider
    {
        /// <summary>
        /// Коллекция информации о производстве уровня 0
        /// </summary>
        private static readonly Dictionary<ProductType, ProductionInfo> _lvl0Collection = 
            new Dictionary<ProductType, ProductionInfo>()
        {
            //Восполняемые товары
            {
                ProductType.Enargy,
                new ProductionInfo<Enargy>(
                    productType: ProductType.Enargy, cycleTimeInSec: 60, productCountInCycle: 300)
            },
            {
                ProductType.Battery,
                new ProductionInfo<Battery, BatteryEmpty, Enargy>(
                    productType: ProductType.Battery, cycleTimeInSec: 60, productCountInCycle: 300,
                    raw1Type: ProductType.BatteryEmpty, raw1CountInCycle: 300,
                    raw2Type: ProductType.Enargy, raw2CountInCycle: 300)
            },
            //{
            //    ProductType.Water,
            //    new ProductionInfo<Water, WaterDirty, Enargy>(
            //        productType: ProductType.Water, cycleTimeInSec: 60, productCountInCycle: 300,
            //        raw1Type: ProductType.WaterDirty, raw1CountInCycle: 300,
            //        raw2Type: ProductType.Enargy, raw2CountInCycle: 300)
            //},
        };
        /// <summary>
        /// Коллекция информации о производстве уровня 1
        /// </summary>
        private static readonly Dictionary<ProductType, ProductionInfo> _lvl1Collection = 
            new Dictionary<ProductType, ProductionInfo>()
        {
            //Не производится - добывается на астеройдах
        };
        /// <summary>
        /// Коллекция информации о производстве уровня 2
        /// </summary>
        private static readonly Dictionary<ProductType, ProductionInfo> _lvl2Collection = 
            new Dictionary<ProductType, ProductionInfo>()
        {
        };
        /// <summary>
        /// Коллекция информации о производстве уровня 3
        /// </summary>
        private static readonly Dictionary<ProductType, ProductionInfo> _lvl3Collection =
            new Dictionary<ProductType, ProductionInfo>()
            {
                //Еда
                {
                    ProductType.Grain,
                    new ProductionInfo<Grain, Water, Enargy>(
                        productType: ProductType.Grain, cycleTimeInSec: 60, productCountInCycle: 10,
                        raw1Type: ProductType.Water, raw1CountInCycle: 20,
                        raw2Type: ProductType.Enargy, raw2CountInCycle: 20)
                },
                {
                    ProductType.Fruit,
                    new ProductionInfo<Fruit, Water, Enargy>(
                        productType: ProductType.Fruit, cycleTimeInSec: 60, productCountInCycle: 10,
                        raw1Type: ProductType.Water, raw1CountInCycle: 20,
                        raw2Type: ProductType.Enargy, raw2CountInCycle: 20)
                },
                {
                    ProductType.Vegetables,
                    new ProductionInfo<Vegetables, Water, Enargy>(
                        productType: ProductType.Vegetables, cycleTimeInSec: 60, productCountInCycle: 10,
                        raw1Type: ProductType.Water, raw1CountInCycle: 20,
                        raw2Type: ProductType.Enargy, raw2CountInCycle: 20)
                },
                {
                    ProductType.CompoundFeed,
                    new ProductionInfo<CompoundFeed, Water, Enargy, Grain, Fruit, Vegetables>(
                        productType: ProductType.CompoundFeed, cycleTimeInSec: 60, productCountInCycle: 10,
                        raw1Type: ProductType.Water, raw1CountInCycle: 20,
                        raw2Type: ProductType.Enargy, raw2CountInCycle: 20,
                        raw3Type: ProductType.Grain, raw3CountInCycle: 10,
                        raw4Type: ProductType.Fruit, raw4CountInCycle: 10,
                        raw5Type: ProductType.Vegetables, raw5CountInCycle: 10)
                },
                {
                    ProductType.Fish,
                    new ProductionInfo<Fish, Water, Enargy, CompoundFeed>(
                        productType: ProductType.Fish, cycleTimeInSec: 60, productCountInCycle: 10,
                        raw1Type: ProductType.Water, raw1CountInCycle: 20,
                        raw2Type: ProductType.Enargy, raw2CountInCycle: 20,
                        raw3Type: ProductType.CompoundFeed, raw3CountInCycle: 20)
                },
                {
                    ProductType.Meat,
                    new ProductionInfo<Meat, Water, Enargy, CompoundFeed>(
                        productType: ProductType.Meat, cycleTimeInSec: 60, productCountInCycle: 10,
                        raw1Type: ProductType.Water, raw1CountInCycle: 20,
                        raw2Type: ProductType.Enargy, raw2CountInCycle: 20,
                        raw3Type: ProductType.CompoundFeed, raw3CountInCycle: 20)
                },
            };
        /// <summary>
        /// Коллекция информации о производстве уровня 4
        /// </summary>
        private static readonly Dictionary<ProductType, ProductionInfo> _lvl4Collection =
            new Dictionary<ProductType, ProductionInfo>()
            {
            };
        /// <summary>
        /// Коллекция информации о производстве уровня 5
        /// </summary>
        private static readonly Dictionary<ProductType, ProductionInfo> _lvl5Collection =
            new Dictionary<ProductType, ProductionInfo>()
            {
            };
        /// <summary>
        /// Коллекция информации о производстве уровня 6
        /// </summary>
        private static readonly Dictionary<ProductType, ProductionInfo> _lvl6Collection =
            new Dictionary<ProductType, ProductionInfo>()
            {
            };
        /// <summary>
        /// Коллекция информации о производстве уровня 7
        /// </summary>
        private static readonly Dictionary<ProductType, ProductionInfo> _lvl7Collection =
            new Dictionary<ProductType, ProductionInfo>()
            {
            };

        private static readonly Dictionary<ProductType, ProductionInfo> _collection = 
            (Dictionary<ProductType, ProductionInfo>)_lvl0Collection
                .Concat(_lvl1Collection)
                .Concat(_lvl2Collection)
                .Concat(_lvl3Collection)
                .Concat(_lvl4Collection)
                .Concat(_lvl5Collection)
                .Concat(_lvl6Collection)
                .Concat(_lvl7Collection);

        /// <summary>
        /// Получить информацию о производимом продукте
        /// </summary>
        /// <param name="productType"> Тип производимого продукта </param>
        /// <param name="productionInfo"> Информация о производимом продукте </param>
        public static bool GetProductionInfo(ProductType productType, out ProductionInfo productionInfo)
        {
            return _collection.TryGetValue(productType, out productionInfo);
        }
    }
}

//TODO Для реализации модулей производства одного товара с разной производительностью нужно,
//что бы было не колво трат ресурса и кол во производства. а соотношение к примеру:
//Для производства 1 железа требуется 2энергии и 2руды


///// <summary>
///// Коллекция информации о производстве продуктов
///// </summary>
//private static readonly Dictionary<ProductType, ProductionInfo> Collection = new Dictionary<ProductType, ProductionInfo>()
//{
//    ////Производимые - простые товары
//    //{
//    //    ProductType.Iron,
//    //    new ProductionInfo<Iron, Enargy, Ore>(
//    //        productType: ProductType.Iron, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Ore, raw2CountInCycle: 20)
//    //},
//    //{
//    //    ProductType.Copper,
//    //    new ProductionInfo<Copper, Enargy, Ore>(
//    //        productType: ProductType.Copper, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Ore, raw2CountInCycle: 20)
//    //}, //TODO Что то еще в состав?
//    //{
//    //    ProductType.Aluminum,
//    //    new ProductionInfo<Aluminum, Enargy, Ore>(
//    //        productType: ProductType.Aluminum, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Ore, raw2CountInCycle: 20)
//    //}, //TODO Что то еще в состав?
//    //{
//    //    ProductType.Sulfur,
//    //    new ProductionInfo<Sulfur, Enargy, Ore>(
//    //        productType: ProductType.Sulfur, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Ore, raw2CountInCycle: 20)
//    //}, //TODO Что то еще в состав?
//    //{
//    //    ProductType.Silicon,
//    //    new ProductionInfo<Silicon, Enargy, Sand>(
//    //        productType: ProductType.Silicon, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Sand, raw2CountInCycle: 20)
//    //},
//    //{
//    //    ProductType.Elastic,
//    //    new ProductionInfo<Elastic, Enargy, Carbon>(
//    //        productType: ProductType.Elastic, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Carbon, raw2CountInCycle: 20)
//    //},
//    //{
//    //    ProductType.Ammonia,
//    //    new ProductionInfo<Ammonia, Enargy, Nitrogen, Hydrogen>(
//    //        productType: ProductType.Silicon, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Nitrogen, raw2CountInCycle: 10,
//    //        raw3Type: ProductType.Hydrogen, raw3CountInCycle: 30)
//    //},
//    //{
//    //    ProductType.Fertilizer,
//    //    new ProductionInfo<Fertilizer, Enargy, Nitrogen, Carbon>(
//    //        productType: ProductType.Fertilizer, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Nitrogen, raw2CountInCycle: 20,
//    //        raw3Type: ProductType.Carbon, raw3CountInCycle: 20)
//    //},

//    ////Производимые - сложные товары
//    //{
//    //    ProductType.Glass,
//    //    new ProductionInfo<Glass, Enargy, Sand, Aluminum>(
//    //        productType: ProductType.Glass, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Sand, raw2CountInCycle: 30,
//    //        raw3Type: ProductType.Aluminum, raw3CountInCycle: 10)
//    //},
//    //{
//    //    ProductType.Metal,
//    //    new ProductionInfo<Metal, Enargy, Iron, Aluminum>(
//    //        productType: ProductType.Metal, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Iron, raw2CountInCycle: 20,
//    //        raw3Type: ProductType.Aluminum, raw3CountInCycle: 10)
//    //},
//    //{
//    //    ProductType.Plastic,
//    //    new ProductionInfo<Plastic, Enargy, Elastic, Ammonia>(
//    //        productType: ProductType.Plastic, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Elastic, raw2CountInCycle: 20,
//    //        raw3Type: ProductType.Ammonia, raw3CountInCycle: 5)
//    //},
//    //{
//    //    ProductType.Rubber,
//    //    new ProductionInfo<Rubber, Enargy, Elastic, Ammonia>(
//    //        productType: ProductType.Rubber, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Elastic, raw2CountInCycle: 20,
//    //        raw3Type: ProductType.Ammonia, raw3CountInCycle: 5)
//    //},
//    //{
//    //    ProductType.SiliconWafer,
//    //    new ProductionInfo<SiliconWafer, Enargy, Silicon, Aluminum, Copper>(
//    //        productType: ProductType.SiliconWafer, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Silicon, raw2CountInCycle: 20,
//    //        raw3Type: ProductType.Aluminum, raw3CountInCycle: 5,
//    //        raw4Type: ProductType.Copper, raw4CountInCycle: 5)
//    //},

//    ////Производимые - высокотехнологичные товары
//    //{
//    //    ProductType.Wiring,
//    //    new ProductionInfo<Wiring, Enargy, Copper, Rubber>(
//    //        productType: ProductType.Wiring, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Copper, raw2CountInCycle: 20,
//    //        raw3Type: ProductType.Rubber, raw3CountInCycle: 10)
//    //},
//    //{
//    //    ProductType.BatteryEmpty,
//    //    new ProductionInfo<BatteryEmpty, Enargy, Lithium, Metal, Wiring, Plastic>(
//    //        productType: ProductType.BatteryEmpty, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Lithium, raw2CountInCycle: 30,
//    //        raw3Type: ProductType.Metal, raw3CountInCycle: 10,
//    //        raw4Type: ProductType.Wiring, raw4CountInCycle: 5,
//    //        raw5Type: ProductType.Plastic, raw5CountInCycle: 5)
//    //},
//    //{
//    //    ProductType.Electronics,
//    //    new ProductionInfo<Electronics, Enargy, Metal, Wiring, SiliconWafer, Plastic>(
//    //        productType: ProductType.Electronics, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Metal, raw2CountInCycle: 10,
//    //        raw3Type: ProductType.Wiring, raw3CountInCycle: 5,
//    //        raw4Type: ProductType.SiliconWafer, raw4CountInCycle: 5,
//    //        raw5Type: ProductType.Plastic, raw5CountInCycle: 5)
//    //},
//    //{
//    //    ProductType.ControlSystems,
//    //    new ProductionInfo<ControlSystems, Enargy, Metal, Electronics, Wiring>(
//    //        productType: ProductType.ControlSystems, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Metal, raw2CountInCycle: 10,
//    //        raw3Type: ProductType.Electronics, raw3CountInCycle: 10,
//    //        raw4Type: ProductType.Wiring, raw4CountInCycle: 5)
//    //},
//    //{
//    //    ProductType.Body,
//    //    new ProductionInfo<Body, Enargy, Metal, Electronics, Glass, Plastic>(
//    //        productType: ProductType.Body, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Metal, raw2CountInCycle: 50,
//    //        raw3Type: ProductType.Electronics, raw3CountInCycle: 10,
//    //        raw4Type: ProductType.Glass, raw4CountInCycle: 10,
//    //        raw5Type: ProductType.Plastic, raw5CountInCycle: 10)
//    //},
//    //{
//    //    ProductType.Sheathing,
//    //    new ProductionInfo<Sheathing, Enargy, Metal, Electronics, Glass, Plastic>(
//    //        productType: ProductType.Sheathing, cycleTimeInSec: 90, productCountInCycle: 10,
//    //        raw1Type: ProductType.Enargy, raw1CountInCycle: 100,
//    //        raw2Type: ProductType.Metal, raw2CountInCycle: 20,
//    //        raw3Type: ProductType.Electronics, raw3CountInCycle: 5,
//    //        raw4Type: ProductType.Glass, raw4CountInCycle: 5,
//    //        raw5Type: ProductType.Plastic, raw5CountInCycle: 5)
//    //},

//    //Производимые - части, модули, оружие, боеприпасы

//    //Производимые - станции, корабли
//};