using ECSCore.BaseObjects;
using GameLib.Datas;
using GameLib.Mechanics.Products.Enums;
using System.Collections.Generic;

namespace GameLib.Mechanics.Stantion.Components
{
    /// <summary>
    /// Компонент склада
    /// </summary>
    public class Warehouse : ComponentBase
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
        /// Коллекция продуктов
        /// Key - Тип продукта
        /// Value - Количество продукта
        /// </summary>
        public Dictionary<ProductType, Count> Products = new Dictionary<ProductType, Count>();
    }
}
