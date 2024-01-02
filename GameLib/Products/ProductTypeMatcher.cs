using GameLib.Mechanics.Products.Enums;
using GameLib.Products.Food;
using GameLib.Products.Lvl0;
using GameLib.Products.Lvl1;
using GameLib.Products.Lvl2;
using GameLib.Products.Lvl3;
using GameLib.Products.Lvl4;
using System;
using System.Collections.Generic;

namespace GameLib.Products
{
    /// <summary>
    /// Конвертер типов продуктов
    /// </summary>
    public static class ProductTypeConverter
    {
        /// <summary>
        /// Колекция типов
        /// </summary>

        public static readonly Dictionary<ProductType, Type> Collection = new Dictionary<ProductType, Type>()
        {
            //Еда
            { ProductType.CompoundFeed, typeof(CompoundFeed) },
            { ProductType.Fish, typeof(Fish) },
            { ProductType.Fruit, typeof(Fruit) },
            { ProductType.Grain, typeof(Grain) },
            { ProductType.Meat, typeof(Meat) },
            { ProductType.Vegetables, typeof(Vegetables) },

            //Восполняемые товары
            { ProductType.Enargy, typeof(Enargy) },
            { ProductType.Battery, typeof(Battery) },
            { ProductType.Water, typeof(Water) },

            //Добываемые товары,
            //{ ProductType.WaterDirty, typeof(WaterDirty) },
            //{ ProductType.Ore, typeof(Ore) },
            //{ ProductType.Sand, typeof(Sand) },
            { ProductType.Carbon, typeof(Carbon) },
            { ProductType.Nitrogen, typeof(Nitrogen) },
            { ProductType.Hydrogen, typeof(Hydrogen) },
            { ProductType.Lithium, typeof(Lithium) },
            { ProductType.Helium, typeof(Helium) }, //TODO Если не используется убрать? //Гелий используется в атомной промышленности, в космических технологиях, в производстве деталей мобильных телефонов, полупроводников, жидкокристаллических экранов, оптических волокон, вычислительной и измерительной техники, для охлаждения ядерных реакторов и в других областях.
            
            //Производимые - простые товары
            { ProductType.Iron, typeof(Iron) },
            { ProductType.Copper, typeof(Copper) },
            { ProductType.Aluminum, typeof(Aluminum) },
            { ProductType.Sulfur, typeof(Sulfur) },
            { ProductType.Silicon, typeof(Silicon) },
            { ProductType.Elastic, typeof(Elastic) },
            { ProductType.Ammonia, typeof(Ammonia) },
            { ProductType.Fertilizer, typeof(Fertilizer) },

            //Производимые - сложные товары
            { ProductType.Glass, typeof(Glass) },
            { ProductType.Metal, typeof(Metal) },
            { ProductType.Plastic, typeof(Plastic) },
            { ProductType.Rubber, typeof(Rubber) },
            { ProductType.SiliconWafer, typeof(SiliconWafer) },

            //Производимые - высокотехнологичные товары
            { ProductType.Wiring, typeof(Wiring) },
            { ProductType.BatteryEmpty, typeof(BatteryEmpty) },
            { ProductType.Electronics, typeof(Electronics) },
            { ProductType.ControlSystems, typeof(ControlSystems) },
            { ProductType.Body, typeof(Body) },
            { ProductType.Sheathing, typeof(Sheathing) },

            //Производимые - части, модули, оружие, боеприпасы

            //Производимые - станции, корабли
        };

        /// <summary>
        /// </summary>
        /// <param name="productType"> Тип продукта </param>
        public static Type ToType(ProductType productType)
        {
            return Collection[productType];
        }
    }
}

//TODO Генерировать подготовку коллекции при запуске через рефлексию.

//TODO Уйти от ручного заполнения коллекции при добавлении нового вида продукта