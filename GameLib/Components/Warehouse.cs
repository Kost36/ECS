using ECSCore.BaseObjects;
using GameLib.Datas;
using GameLib.Products;
using System.Collections.Generic;

namespace GameLib.Components
{
    /// <summary>
    /// Склад
    /// </summary>
    public sealed class Warehouse : ComponentBase
    {
        /// <summary>
        /// Занятый объем склада
        /// </summary>
        public int Volume;
        /// <summary>
        /// Максимальный объем склада
        /// </summary>
        public int VolumeMax;
        /// <summary>
        /// Товары
        /// </summary>
        public Dictionary<ProductType, Count> Products = new Dictionary<ProductType, Count>();
    }
}
