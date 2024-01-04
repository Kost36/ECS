using GameLib.Mechanics.Products.Enums;
using System;

namespace GameLib.Mechanics.Products.Extensions
{
    public static class ProductTypeExtensions
    {
        /// <summary>
        /// Продукт является добываемым минералом
        /// </summary>
        public static bool IsMineral(this ProductType productType)
        {
            return Enum.IsDefined(typeof(MineralType), (int)productType);
        }

        /// <summary>
        /// Преобразовать тип продукта в тип минерала
        /// </summary>
        public static bool TryToMineral(this ProductType productType, out MineralType mineralType)
        {
            if (!productType.IsMineral())
            {
                mineralType = MineralType.Unknown;
                return false;
            }

            mineralType = (MineralType)productType;
            return true;
        }

        /// <summary>
        /// Преобразовать тип продукта в тип минерала
        /// </summary>
        public static MineralType ToMineral(this ProductType productType)
        {
            if (!productType.IsMineral())
            {
                return MineralType.Unknown;
            }

            return (MineralType)productType;
        }
    }
}
