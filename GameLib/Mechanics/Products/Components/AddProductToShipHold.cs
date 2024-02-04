using ECSCore.BaseObjects;
using GameLib.Mechanics.Products.Enums;

namespace GameLib.Mechanics.MineralExtraction.Components
{
    /// <summary>
    /// Добавить товар в трюм корабля
    /// </summary>
    public class AddProductToShipHold : ComponentBase
    {
        /// <summary>
        /// Тип продукта
        /// </summary>
        public ProductType ProductType;
        /// <summary>
        /// Количество
        /// </summary>
        public int Count;
    }
}
