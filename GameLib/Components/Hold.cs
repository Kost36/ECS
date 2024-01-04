using ECSCore.BaseObjects;
using GameLib.Mechanics.Products.Enums;
using System;
using System.Collections.Generic;

namespace GameLib.Components
{
    /// <summary>
    /// Компонент трюма (вместимости в м³) 
    /// м³
    /// </summary>
    [Serializable]
    public class Hold : ComponentBase
    {
        /// <summary>
        /// Максимальная вместимость в м³
        /// </summary>
        public float Max;
        /// <summary>
        /// Занято емкости в м³
        /// </summary>
        public float Use;
        /// <summary>
        /// Коллекция товаров в трюме корабля
        /// Key - Тип товара
        /// Value - Количество продукта
        /// </summary>
        public Dictionary<ProductType, int> Products = new Dictionary<ProductType, int>();
    }
}
