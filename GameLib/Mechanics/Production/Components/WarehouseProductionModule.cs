using ECSCore.BaseObjects;
using GameLib.Datas;
using GameLib.Mechanics.Products.Enums;
using System.Collections.Generic;

namespace GameLib.Mechanics.Production.Components
{
    /// <summary>
    /// Склад производственного модуля
    /// </summary>
    public class WarehouseProductionModule : ComponentBase
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
        /// Заданный процент заполнения склада сырьевыми товарами
        /// </summary>
        public int PercentFillingRaws;
        /// <summary>
        /// Производимый товар
        /// </summary>
        public KeyValuePair<ProductType, Count> Product = new KeyValuePair<ProductType, Count>();
        /// <summary>
        /// Сырьевые товары
        /// </summary>
        public Dictionary<ProductType, Count> Raws = new Dictionary<ProductType, Count>();
    }
}
