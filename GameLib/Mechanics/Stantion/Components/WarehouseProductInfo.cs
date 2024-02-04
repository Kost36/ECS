namespace GameLib.Mechanics.Stantion.Components
{
    /// <summary>
    /// Информация о продукте на складе
    /// </summary>
    public sealed class WarehouseProductInfo
    {
        /// <summary>
        /// Продукт является потребляемым станцией
        /// </summary>
        public bool IsRaw;
        /// <summary>
        /// Продукт является производимым на станции
        /// </summary>
        public bool IsProduct;
        /// <summary>
        /// Продукт разрешено продавать
        /// </summary>
        public bool IsCanSell;
        /// <summary>
        /// Продукт разрешено покупать
        /// </summary>
        public bool IsCanBuy;
        /// <summary>
        /// Кол-во продукта на станции
        /// </summary>
        public int Count;
        /// <summary>
        /// Максимально допустимое кол-во продукта на станции
        /// </summary>
        public int MaxLimit = int.MaxValue;
        /// <summary>
        /// Минимально допустимое кол-во продукта на станции
        /// </summary>
        public int MinLimit = 0;
    }
}
