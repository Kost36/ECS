using GameLib.Products.Lvl0;
using GameLib.Products.Lvl1;
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
            { ProductType.Battery, typeof(Battery) },
            { ProductType.Ore, typeof(Ore) },
            { ProductType.Iron, typeof(Iron) }
        };

        /// <summary>
        /// Привести Type к ProductType
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
