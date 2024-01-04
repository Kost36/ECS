using GameLib.Mechanics.Products.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLib.Mechanics.Products.Extensions
{
    public static class MineralTypeExtensions
    {
        /// <summary>
        /// Преобразовать тип минерала в тип продукта 
        /// </summary>
        public static ProductType ToProduct(this MineralType mineralType)
        {
            return (ProductType)mineralType;
        }

        /// <summary>
        /// Получить список всех минералов 
        /// </summary>
        public static List<MineralType> GetAllMinerals(this MineralType _)
        {
            return Enum
                .GetValues(typeof(MineralType))
                .Cast<MineralType>()
                .ToList();
        }
    }
}
