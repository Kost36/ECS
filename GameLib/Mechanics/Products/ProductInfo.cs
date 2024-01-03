using GameLib.Mechanics.Products.Enums;

namespace GameLib.Mechanics.Products
{
    /// <summary>
    /// Информация о еденице продукта
    /// </summary>
    public sealed class ProductInfo
    {
        /// <summary>
        /// Тип продукта
        /// </summary>
        public ProductType ProductType;
        /// <summary>
        /// Объем еденицы - м³
        /// </summary>
        public float Volume;
        /// <summary>
        /// Вес еденицы - кг
        /// </summary>
        public float Weight;
    }
}
